using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [SerializeField]
    private float followSpeed;
    [SerializeField]
    private Vector3 offset;

    private GameObject player;

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
    }
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.gameObject;
        ResetCamera();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = Vector3.Lerp(transform.position, (player.transform.position + offset), Time.deltaTime * followSpeed);
    }

    public void ResetCamera()
    {
        if (player != null)
        {
            this.transform.position = player.transform.position + offset;
        }
    }
}
