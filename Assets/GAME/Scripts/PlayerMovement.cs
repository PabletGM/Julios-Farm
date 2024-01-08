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
    private float speed = 10;

    private Vector3 positionHitPoint;

    private float positionHitPointOffset = 5;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
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
                positionHitPoint = hitPoint.point;
                agent.SetDestination(hitPoint.point);
            }
        }

        if(agent.velocity != Vector3.zero)
        {
            anim.SetBool("IsMoving", true);
        }
        //para quitar animacion correr que no tenga velocidad y que distancia entre punto al que va y player sea 0
        else if (agent.velocity == Vector3.zero || Vector3.Distance(this.transform.position, positionHitPoint) <= positionHitPointOffset)
        {
            anim.SetBool("IsMoving", false);
        }
    }

}
