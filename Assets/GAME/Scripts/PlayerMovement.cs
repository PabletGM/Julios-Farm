using CreatorKitCodeInternal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private Camera cam;
    
    private Animator anim;

    [Header("NavMesh parameters")]
    private NavMeshAgent agent;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        speed = agent.speed;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitPoint;

            if(Physics.Raycast(ray, out hitPoint))
            {
                agent.SetDestination(hitPoint.point);
            }
        }

        if(agent.velocity != Vector3.zero)
        {
            anim.SetBool("IsMoving", true);
        }
        else if (agent.velocity == Vector3.zero)
        {
            anim.SetBool("IsMoving", false);
        }
    }

}
