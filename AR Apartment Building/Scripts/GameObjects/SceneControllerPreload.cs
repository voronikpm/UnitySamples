using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllerPreload : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        LoadScene("SceneHouse");
	}

    public void LoadScene(string scene)
    {
        StartCoroutine(LoadSceneCoroutine(scene));
    }

    private IEnumerator LoadSceneCoroutine(string scene)
    {
        yield return new WaitForEndOfFrame();
#if UNITY_IPHONE
            Handheld.SetActivityIndicatorStyle(iOS.ActivityIndicatorStyle.Gray);
#elif UNITY_ANDROID
            Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Large);
#elif UNITY_TIZEN
            Handheld.SetActivityIndicatorStyle(TizenActivityIndicatorStyle.Small);
#endif
        Handheld.StartActivityIndicator();
        SceneManager.LoadScene(scene);
    }
}
