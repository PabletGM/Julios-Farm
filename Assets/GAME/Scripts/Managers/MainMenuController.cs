using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void NewGame()
    {
        Invoke("actionNewGame", 1f);
    }

    public void Survival()
    {
        Invoke("actionSurvival", 1f);
    }

    private void actionNewGame()
    {
        SceneLoaderManager.instance.LoadFirstScene();
    }

    private void actionSurvival()
    {
        SceneLoaderManager.instance.LoadSurvivalScene();
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
