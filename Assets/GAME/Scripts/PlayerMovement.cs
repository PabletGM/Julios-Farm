using CreatorKitCodeInternal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{

    private NavMeshAgent agent;

    private int levelLayer;

    [SerializeField]
    private float speed = 10.0f;
    private Vector3 lastRaycastResult;

    [SerializeField]
    private Transform lookAtTarget;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.angularSpeed = 360.0f;
        lastRaycastResult = transform.position;

        levelLayer = 1 << LayerMask.NameToLayer("Level");

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        Ray screenRay = CameraController.Instance.GameplayCamera.ScreenPointToRay(Input.mousePosition);

        CharacterLookAt();
        agent.SetDestination(lookAtTarget.position);
    }

    private void CharacterLookAt()
    {
        transform.LookAt(lookAtTarget, Vector3.up);
    }
}
