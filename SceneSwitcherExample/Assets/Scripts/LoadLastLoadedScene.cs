using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLastLoadedScene : MonoBehaviour
{
    private int LastLoadedScene;

    void Start()
    {
        // Fetch last loaded scene from the PlayerPrefs (set these Playerprefs in another script)
        LastLoadedScene = PlayerPrefs.GetInt("LastLoadedScene");
        print(LastLoadedScene);

//#if (!UNITY_ANDROID && !UNITY_IOS)
        LoadSceneFunction();
//#endif
    }

    void LoadSceneFunction()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex == 0)
        {
            //Debug.Log("Load scene 1");
            //SceneManager.LoadScene(1);

            Debug.Log("Load last loaded scene");
            if (LastLoadedScene == 0)
            {
                SceneManager.LoadScene(1);
            }
            if (LastLoadedScene > 0)
            {
                SceneManager.LoadScene(LastLoadedScene);
            }
        }
    }
}