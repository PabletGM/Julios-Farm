//using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDamageableObject : DamageableObject
{
    public float health = 3f;

    private float currentHealth;

    private void Start()    {
        currentHealth = health;
    }

    public override void TakeDamage(float damage, DamageEmiterType emiterType)
    {
        //hitAnimation.DORestart();
        if (health == -1f)
        {
            return;
        }

        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            base.TakeDamage(damage, emiterType);
        }
    }
}
