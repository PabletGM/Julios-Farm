using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FarmAbilityCharacter : AbilityCharacter
{

    private FarmStats farmStats;

    protected float currentHealth;

    //iniciamos abilityCharacter y farmStats
    protected override void InitAbilityCharacter()
    {
        base.InitAbilityCharacter();

        //Init agent paramenters 
        if (farmStats != null)
        {
            currentHealth = farmStats.health;
        }

    }

    protected override void Update()
    {
        //si el farm esta muerto o no puede hacer habilidades no hace nada 
        if (!canDoAbilties && currentHealth <= 0)
        {
            return;
        }

        //funcionalidad del update del AbilityCharacter
        base.Update();
    }


    public override void TakeDamage(float damage, DamageEmiterType emiterType)
    {
        if (emiterType == DamageEmiterType.Player || currentHealth < 0f)
        {
            return;
        }

        //si el emisor del daño es el player
        else if (emiterType == DamageEmiterType.Enemy)
        {
            currentHealth -= damage;
            //Debug.Log(currentHealth);
            //if (currentHealth <= 0f)
            //{
            //    ResetCurrentAbility();
            //    canDoAbilties = false;
            //    Destroy(this.gameObject);
            //}
        }


    }
}
