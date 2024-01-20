using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(fileName = "SceneList", menuName = "JuliosFarm/SceneList")]
public class SceneList : ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField]
    private SceneAsset firstScene;

    [SerializeField]
    private List<SceneAsset> sceneAssets;
#endif

    [SerializeField]
    public List<string> sceneNames;

    [SerializeField]
    public string firstSceneName;
    [SerializeField]
    public string secondSceneName;

    private void OnValidate()
    {
#if UNITY_EDITOR
        sceneNames.Clear();
        if (sceneAssets.Count > 0)
        {
            foreach (SceneAsset asset in sceneAssets)
            {
                if (asset != null)
                {
                    sceneNames.Add(asset.name);
                }
            }
        }

        if (firstScene != null)
        {
            firstSceneName = firstScene.name;
        }
#endif
    }
}
