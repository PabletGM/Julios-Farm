using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI; 

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private Image EnemyHealthBar;

    

    private void Update()
    {
        transform.LookAt(Vector3.forward);
    }

}
