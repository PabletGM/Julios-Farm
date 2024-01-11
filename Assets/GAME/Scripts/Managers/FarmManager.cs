using CreatorKitCodeInternal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FarmManager : MonoBehaviour
{
    public static FarmManager instance;

    //Stats management
    private float initHealth;

    private float currentHealth;
    public float CurrentHealth
    {
        set
        {
            currentHealth = value;
        }
        get
        {
            return currentHealth;
        }
    }

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
        currentHealth = initHealth;
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
