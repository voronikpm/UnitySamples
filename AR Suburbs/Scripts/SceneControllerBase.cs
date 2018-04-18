#region Using Directives

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

namespace Assets.Scripts
{
    public abstract class SceneControllerBase : MonoBehaviour
    {
        #region Fields

        #region  Protected Fields

        protected AsyncOperation _SceneLoader;
        protected Coroutine _SceneLoadingCoroutine;

        #endregion

        #endregion

        #region Methods

        #region Virtual Methods

        public virtual void LoadScene(string scene, LoadSceneMode mode = LoadSceneMode.Single)
        {
            _SceneLoadingCoroutine = StartCoroutine(LoadSceneCoroutine(scene, mode));
        }

        protected virtual void Awake()
        {
            PostLoad();
        }

        protected virtual void CheckLoadState()
        {
            if(_SceneLoader != null)
                if(_SceneLoader.progress >= 0.9f)
                {
                    _SceneLoader.allowSceneActivation = true;
                    StopCoroutine(_SceneLoadingCoroutine);
                }
        }

        protected virtual IEnumerator LoadSceneCoroutine(string scene, LoadSceneMode mode = LoadSceneMode.Single)
        {
            _SceneLoader = SceneManager.LoadSceneAsync(scene, mode);
            _SceneLoader.allowSceneActivation = false;
            yield return _SceneLoader;
        }

        protected virtual void PostLoad()
        {
            MainSceneController.HighlightedObject = null;
        }

        protected virtual void Update()
        {
            CheckLoadState();
        }

        #endregion

        #endregion
    }
}