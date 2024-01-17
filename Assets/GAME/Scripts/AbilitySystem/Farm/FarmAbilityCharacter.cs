using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FarmAbilityCharacter : AbilityCharacter
{

    protected float currentHealth;
    protected float maxShield;
    protected float maxHealth;
    protected float currentShield;

    [SerializeField]
    private GameObject farmShield;

    public static FarmAbilityCharacter Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    //iniciamos abilityCharacter y farmStats
    protected override void InitAbilityCharacter()
    {
        base.InitAbilityCharacter();

        //Init agent paramenters 
        maxHealth = characterStats.maxHealth;
        currentHealth = characterStats.health;
        UpdateCurrentShield(0f);
        maxShield = characterStats.maxShield.runTimeValue;
        UIManager.Instance.UpdateHealthBar(1f);
        UIManager.Instance.UpdateShieldBar(0f);

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
        else if (emiterType == DamageEmiterType.Enemy && currentHealth > 0)
        {
            if(currentShield > 0)
            {
                currentShield -= damage;
                UIManager.Instance.UpdateShieldBar(currentShield / maxShield);
                if(currentShield <= 0f)
                {
                    maxShield = 0f;
                    farmShield.SetActive(false);
                }
            }
            else
            {
                currentHealth -= damage;
                UIManager.Instance.UpdateHealthBar(currentHealth / maxHealth);
            }

           
        }
    }

    public void UpdateCurrentShield(float currentshield)
    {
        currentShield = currentshield;
    }

    public void InitialCurrentShield()
    {
        currentShield = characterStats.shield.runTimeValue;
        maxShield = characterStats.maxShield.runTimeValue;
        farmShield.SetActive(true);
    }

}
