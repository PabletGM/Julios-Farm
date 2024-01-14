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

    private void actionNewGame()
    {
        SceneLoaderManager.instance.LoadFirstScene();
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
