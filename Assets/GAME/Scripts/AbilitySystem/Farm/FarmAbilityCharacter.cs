using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FarmAbilityCharacter : AbilityCharacter
{

    protected float currentHealth;
    protected float maxHealth;

    //iniciamos abilityCharacter y farmStats
    protected override void InitAbilityCharacter()
    {
        base.InitAbilityCharacter();

        //Init agent paramenters 
        maxHealth = characterStats.maxHealth;
        currentHealth = characterStats.health;
        UIManager.Instance.UpdateHealthBar(1f);

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

        //si el emisor del da�o es el player
        else if (emiterType == DamageEmiterType.Enemy)
        {
            currentHealth -= damage;

            UIManager.Instance.UpdateHealthBar(currentHealth/maxHealth);
            Debug.Log("Max health = " + maxHealth + ", current health = " + currentHealth);

            //if (currentHealth <= 0f)
            //{
            //    ResetCurrentAbility();
            //    canDoAbilties = false;
            //    Destroy(this.gameObject);
            //}
        }


    }
}
