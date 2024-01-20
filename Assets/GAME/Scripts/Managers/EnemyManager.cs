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

    //Stats management -- Health 
    private float initHealth;

    [HideInInspector]
    public string enemyName;

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

    public static EnemyManager Instance;
    

    private void Start()
    {
        currentHealth = initHealth;
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);     //EnemyHealthBar LookAt Camera 
    }

    public void UpdateEnemyHealthBar(float fillAmount)
    {
        if(this.gameObject != null)
        {
            EnemyHealthBar.fillAmount = fillAmount;
        }
       
    }

    public void UpdateEnemyName(string enemyname)
    {
        enemyName = enemyname;
    }
}
