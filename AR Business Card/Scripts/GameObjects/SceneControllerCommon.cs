#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts.Entities;
using Assets.Scripts.Enums;
using Assets.Scripts.EventArgs;
using Assets.Scripts.ExtensionMethods;
using Assets.Scripts.ViewModels;
using Noesis;
using UnityEngine;
using UnityEngine.SceneManagement;
using GUI = Noesis.GUI;
using Path = System.IO.Path;
using Rect = UnityEngine.Rect;

#endregion

namespace Assets.Scripts.GameObjects
{
    public sealed class SceneControllerCommon : SceneControllerBase
    {
        #region Public Delegates

        public delegate void SceneLoadedEventHandler(object sender, SceneLoadedEventArgs args);

        #endregion

        #region Events

        public event SceneLoadedEventHandler OnSceneLoaded;

        #endregion

        #region Fields

        #region  Private Fields

        private float _framerate;

        [SerializeField]
        private SceneType _initialScene;

        private bool _isLoaded;
        private bool _isUnloaded = true;
        private SceneType _loadingScene = SceneType.None;
        private AsyncOperation _sceneLoader;
        private Coroutine _sceneLoadingCoroutine;
        private Language _selectedLanguage;

        #endregion

        #endregion

        #region Properties

        #region Regular Properties

        public SceneType CurrentScene { get; private set; } = SceneType.None;

        public float Framerate
        {
            get { return _framerate; }
            set
            {
                _framerate = value;
                OverlayViewModel.Instance.Framerate = value;
            }
        }

        public List<Language> Languages { get; set; }

        public Language SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                _selectedLanguage = value;
                OverlayViewModel.Instance.RefreshLanguages();
            }
        }

        //TODO save settings
        public Dictionary<SceneType, Settings> SettingsDictionary { get; private set; } = new Dictionary<SceneType, Settings>
                                                                                          {
                                                                                              {SceneType.Turbine, new Settings()},
                                                                                              {SceneType.House, new Settings()},
                                                                                              {SceneType.Game, new Settings()}
                                                                                          };

        #endregion

        #region Static Properties

        public static SceneControllerCommon Instance { get; private set; }

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        public void LoadScene(SceneType scene, bool isFirst = false, LoadSceneMode mode = LoadSceneMode.Additive)
        {
            _loadingScene = scene;
            _sceneLoadingCoroutine = StartCoroutine(LoadSceneCoroutine(GetSceneName(scene), mode));
            OverlayViewModel.Instance.IsEnabled = false;
            if(!isFirst)
                SceneManager.UnloadSceneAsync(GetSceneName(CurrentScene));
        }

        public void ShareImage()
        {
            StartCoroutine(ShareImageCoroutine());
        }

        private void CheckLoadState()
        {
            if(_sceneLoader != null && _sceneLoader.progress >= 0.9f)
            {
                _sceneLoader.allowSceneActivation = true;
                StopCoroutine(_sceneLoadingCoroutine);
                _sceneLoader = null;
                CurrentScene = _loadingScene;
                _loadingScene = SceneType.None;
            }
        }

        private void FinishLoading(Scene scene)
        {
            if(scene.buildIndex != 0)
                if(_isLoaded && _isUnloaded)
                {
                    _isLoaded = false;
                    _isUnloaded = false;
                    var currentScene = _loadingScene == SceneType.None ? CurrentScene : _loadingScene;
                    OnSceneLoaded?.Invoke(this, new SceneLoadedEventArgs(currentScene));
                }
        }

        private IEnumerator FramerateCoroutine()
        {
            while(true)
            {
                int lastFrameCount = Time.frameCount;
                float lastTime = Time.realtimeSinceStartup;
                yield return new WaitForSecondsRealtime(0.5f);
                float timeSpan = Time.realtimeSinceStartup - lastTime;
                int frameCount = Time.frameCount - lastFrameCount;
                Framerate = frameCount / timeSpan;
            }
        }

        private string GetSceneName(SceneType scene)
        {
            return $"Scene{scene.ToString()}";
        }

        private IEnumerator LoadSceneCoroutine(string scene, LoadSceneMode mode = LoadSceneMode.Additive)
        {
            _sceneLoader = SceneManager.LoadSceneAsync(scene, mode);
            _sceneLoader.allowSceneActivation = false;
            yield return _sceneLoader;
        }

        private void LoadSettings()
        {
            if(!File.Exists($"{Application.persistentDataPath}/settings.dat"))
                return;
            try
            {
                using(var file = File.OpenRead($"{Application.persistentDataPath}/settings.dat"))
                {
                    var data = new BinaryFormatter().Deserialize(file) as Dictionary<SceneType, Settings>;
                    if(data == null)
                        return;
                    SettingsDictionary = data;
                }
            }
            catch(Exception e)
            {
                Debug.Log($"{e.Message}\r\n{e.StackTrace}");
            }
        }

        private void SaveSettings()
        {
            try
            {
                FindObjectsOfType<SceneControllerBase>().Where(x => !(x is SceneControllerCommon)).InvokeAction(x => x.UnhookEventListeners());
                using(var file = File.Create($"{Application.persistentDataPath}/settings.dat"))
                {
                    new BinaryFormatter().Serialize(file, SettingsDictionary);
                    file.Flush();
                    file.Close();
                }
            }
            catch(Exception e)
            {
                Debug.Log($"{e.Message}\r\n{e.StackTrace}");
            }
        }

        private IEnumerator ShareImageCoroutine()
        {
            yield return new WaitForEndOfFrame();
            var ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            ss.Apply();
            string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
            File.WriteAllBytes(filePath, ss.EncodeToPNG());
            NativeShare.Share(filePath, true, "ru.indalo.Turbine");
        }

        private void Update()
        {
            CheckLoadState();
            Input.simulateMouseWithTouches = false;
            var isTouch = false;
            var touchPos = Vector2.zero;
            if(Application.isMobilePlatform)
            {
                if(Input.touches.Any())
                {
                    isTouch = Input.GetTouch(0).phase == TouchPhase.Began;
                    touchPos = Input.touches[0].position;
                    IsMultiTouch = Input.touchCount == 2;
                }
            }
            else
            {
                isTouch = Input.GetMouseButtonDown(0);
                touchPos = Input.mousePosition;
            }
            if(isTouch && !IsMultiTouch)
            {
                var ray = Camera.main.ScreenPointToRay(touchPos);
                RaycastHit raycastHit;
                if(Physics.Raycast(ray, out raycastHit))
                {
                    var hit = raycastHit.transform.GetComponent<TouchableObject>();
                    if(hit)
                        hit.TouchAction();
                }
            }
            else if(IsMultiTouch)
            {
                SceneControllerTurbine.Instance.Zoom(Input.touches[0], Input.touches[1]);
            }
        }

        public bool IsMultiTouch { get; private set; }

        #endregion

        #region Event Handlers

        private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _isLoaded = true;
            FinishLoading(scene);
        }

        private void SceneManager_sceneUnloaded(Scene scene)
        {
            _isUnloaded = true;
            FinishLoading(scene);
        }

        #endregion

        #region Overriding Methods

        protected override void Awake()
        {
            Instance = FindObjectOfType<SceneControllerCommon>();
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
            LoadSettings();
            LoadScene(_initialScene, true);
            OverlayViewModel.Instance.Init();
            StartCoroutine(FramerateCoroutine());
            Languages = new List<Language>
                        {
                            new Language {Name = "English", Resources = (ResourceDictionary) GUI.LoadXaml("Assets/Scripts/UI/TextsEn.xaml")},
                            new Language {Name = "Russian", Resources = (ResourceDictionary) GUI.LoadXaml("Assets/Scripts/UI/TextsRu.xaml")}
                        };
            SelectedLanguage = Languages[0];
        }

        protected override void OnDestroy()
        {
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
            SceneManager.sceneUnloaded -= SceneManager_sceneUnloaded;
            SaveSettings();
        }

        #endregion

        #endregion
    }
}