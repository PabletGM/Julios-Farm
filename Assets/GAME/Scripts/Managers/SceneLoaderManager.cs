using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    public static SceneLoaderManager instance;

    public SceneList SceneList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void LoadFirstScene()
    {
        LoadScene(SceneList.firstSceneName);
    }

    public void LoadSurvivalScene()
    {
        LoadScene(SceneList.secondSceneName);
    }

    public void ReturnToMainMenu()
    {
        LoadScene("MainMenu");
    }

    public void LoadRandomScene()
    {
        if (SceneList != null)
        {
            int sceneIndex = Random.Range(0, SceneList.sceneNames.Count);
            Debug.Log("Scene To Load -> " + SceneList.sceneNames[sceneIndex]);
            LoadScene(SceneList.sceneNames[sceneIndex]);
        }
    }

    private void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}
