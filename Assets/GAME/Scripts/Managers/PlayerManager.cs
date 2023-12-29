using CreatorKitCodeInternal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private PlayerAbilityCharacter playerAbilityCharacter;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerAbilityCharacter = GetComponent<PlayerAbilityCharacter>();

        SetPlayerEnterPosition();
    }

    public GenericStats GetPlayerStats()
    {
        return playerAbilityCharacter.CharacterStats;
    }

    public void SetPlayerEnterPosition()
    {
        if (RoomEnterPoint.instance != null)
        {
            transform.position = RoomEnterPoint.instance.transform.position;
            transform.rotation = RoomEnterPoint.instance.transform.rotation;
            if (CameraManager.instance != null)
            {
                CameraManager.instance.ResetCamera();
            }
        }
    }

    //public void PlayerDied()
    //{
    //    UIManager.Instance.ShowGameOverCanvas();
    //}
}
