using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher Instance { get; private set; }

    public GameObject ButtonLeft;
    public GameObject ButtonRight;

    private Scene CurrentScene;
    private int NumberOfScenes;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        NumberOfScenes = SceneManager.sceneCountInBuildSettings;
        ButtonLeft.GetComponent<Button>().onClick.AddListener(ButtonLeftOnClickFunction);
        ButtonRight.GetComponent<Button>().onClick.AddListener(ButtonRightOnClickFunction);
    }

    void Start()
    {
    
    }

    void ButtonLeftOnClickFunction()
    {
        if (CurrentScene.buildIndex == 1)
        {
            SceneManager.LoadScene(NumberOfScenes - 1, LoadSceneMode.Single);
        }
        if (CurrentScene.buildIndex > 1)
        {
            SceneManager.LoadScene(CurrentScene.buildIndex - 1, LoadSceneMode.Single);
        }
    }

    void ButtonRightOnClickFunction()
    {
        if (CurrentScene.buildIndex == NumberOfScenes - 1)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        if (CurrentScene.buildIndex < NumberOfScenes - 1)
        {
            SceneManager.LoadScene(CurrentScene.buildIndex + 1, LoadSceneMode.Single);
        }
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        CurrentScene = SceneManager.GetActiveScene();
        Debug.Log("Active Scene name is: " + CurrentScene.name + "\nActive Scene index: " + CurrentScene.buildIndex);
        //Debug.Log("Level Loaded");
        //Debug.Log(scene.name);
        //Debug.Log(mode);
        if (CurrentScene.buildIndex > 0)
        {
            PlayerPrefs.SetInt("LastLoadedScene", CurrentScene.buildIndex);
        }
    }
}