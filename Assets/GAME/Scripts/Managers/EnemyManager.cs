using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI; 

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private Image EnemyHealthBar;

    public Transform cam; 

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }

}
