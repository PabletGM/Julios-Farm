using CreatorKitCodeInternal;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    PlayerAbilityCharacter character;
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private PlayerStats stats;

    private Animator anim;

    [Header("NavMesh parameters")]
    private NavMeshAgent agent;

    private Vector3 positionHitPoint;

    private float positionHitPointOffset = 5;

    private bool leftButtonPressed = false;

    public bool CanMovement
    {
        get
        {
            return character.CanMovePlayer;
        }
        set
        {
            character.CanMovePlayer = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = stats.speed.runTimeValue;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //si se pulsa y se puede mover
        if (CanMovement && leftButtonPressed)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitPoint;

            if (Physics.Raycast(ray, out hitPoint))
            {
                positionHitPoint = hitPoint.point;
                agent.SetDestination(hitPoint.point);
            }
        }
        else if (!leftButtonPressed)
        {
            agent.SetDestination(this.transform.position);
        }


        if (agent.velocity != Vector3.zero)
        {
            anim.SetBool("IsMoving", true);
        }
        //para quitar animacion correr que no tenga velocidad y que distancia entre punto al que va y player sea 0
        else if (agent.velocity == Vector3.zero || Vector3.Distance(this.transform.position, positionHitPoint) <= positionHitPointOffset)
        {
            anim.SetBool("IsMoving", false);
        }
    }

    private void OnHoldMovement(InputValue value)
    {
        leftButtonPressed = !leftButtonPressed;
    }
}
