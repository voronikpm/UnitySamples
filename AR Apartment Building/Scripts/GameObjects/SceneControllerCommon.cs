#region Using Directives

using System.Collections;
using Assets.Scripts.Enums;
using Assets.Scripts.EventArgs;
using Assets.Scripts.ViewModels;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        private SceneType _currentScene = SceneType.None;
        private float _framerate;
        private bool _isLoaded;
        private bool _isUnloaded = true;
        private SceneType _loadingScene = SceneType.None;
        private AsyncOperation _sceneLoader;
        private Coroutine _sceneLoadingCoroutine;

        #endregion

        #endregion

        #region Properties

        #region Regular Properties

        public SceneType CurrentScene
        {
            get { return _currentScene; }
            private set { _currentScene = value; }
        }

        public float Framerate
        {
            get { return _framerate; }
            set
            {
                _framerate = value;
                OverlayViewModel.Instance.Framerate = value;
            }
        }

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
                    if(OnSceneLoaded != null)
                        OnSceneLoaded(this, new SceneLoadedEventArgs(currentScene));
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
            return string.Format("Scene{0}", scene.ToString());
        }

        private IEnumerator LoadSceneCoroutine(string scene, LoadSceneMode mode = LoadSceneMode.Additive)
        {
            _sceneLoader = SceneManager.LoadSceneAsync(scene, mode);
            _sceneLoader.allowSceneActivation = false;
            yield return _sceneLoader;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
            SceneManager.sceneUnloaded -= SceneManager_sceneUnloaded;
        }

        private void Update()
        {
            CheckLoadState();
        }

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
            LoadScene(SceneType.Menu, true);
            OverlayViewModel.Instance.Init();
            StartCoroutine(FramerateCoroutine());
        }

        #endregion

        #endregion
    }
}