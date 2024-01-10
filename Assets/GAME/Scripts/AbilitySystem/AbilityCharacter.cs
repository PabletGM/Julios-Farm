using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class AbilityCharacter : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected GenericStats characterStats;
    public GenericStats CharacterStats
    {
        get
        {
            return characterStats;
        }
    }
    [SerializeField]
    protected AttackAbility slotAttackAbility;
    [SerializeField]
    protected List<BaseAbility> passiveAbilities;

    private Rigidbody rb;
    public Rigidbody Rigidbody
    {
        get
        {
            return rb;
        }
    }
    private Animator anim;
    public Animator Animator
    {
        get
        {
            return anim;
        }
    }

    //permiso para moverse y hacer habilidades
    public bool canMove = true;
    protected bool canDoAbilties = true;

    //tiempo de parada entre ataque y ataque
    protected float stoppedByExecuteAbilityTime = 0f;

    protected BaseAbility currentAbility;
    //tiempo que lleva hecho de habilidad
    private float currentAbilityElapsedTime = 0;
    //coolDown de ataque
    protected float attackAbilityCooldown = 0;

    private void Start()
    {
        InitAbilityCharacter();
    }

    // Method to init variables for all ability characters
    protected virtual void InitAbilityCharacter()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        if (passiveAbilities.Count > 0)
        {
            foreach (BaseAbility passiveAbility in passiveAbilities)
            {
                passiveAbility.StartAbility(this);
            }
        }
    }

    public void AddPassiveAbility(BaseAbility passiveAbility)
    {
        passiveAbilities.Add(passiveAbility);
        passiveAbility.StartAbility(this);
        UIManager.Instance.listPassiveAbilitiesImages.Add(passiveAbility.abilityIcon);
    }

    //si es el enemy ejecuta primary ability de ataque si esta en distancia de ataque
    protected void ExecutePrimaryAbility()
    {
        if (!canDoAbilties)
        {
            return;
        }

        //si hay habilidad de ataque y no hay cooldown
        if (slotAttackAbility != null && attackAbilityCooldown <= 0f)
        {
            slotAttackAbility.StartAbility(this);
            stoppedByExecuteAbilityTime = slotAttackAbility.movementStopTime;
            currentAbility = slotAttackAbility;
            attackAbilityCooldown = slotAttackAbility.cooldown;
        }
    }



    protected virtual void ResetCurrentAbility()
    {
        if (currentAbility != null)
        {
            currentAbility.EndAbility(this);
            currentAbility = null;
        }
        currentAbilityElapsedTime = 0f;
    }

    protected void SendAbilityAnimationEvent()
    {
        if (currentAbility == null)
        {
            return;
        }

        currentAbility.OnReceiveAnimationEvent(this);
    }

    protected virtual void Update()
    {
        if (currentAbility != null && currentAbility.duration != 0f)
        {
            if (currentAbilityElapsedTime < currentAbility.duration)
            {
                currentAbilityElapsedTime += Time.deltaTime;
                currentAbility.UpdateAbility(this, Time.deltaTime, currentAbilityElapsedTime);
            }
            else
            {
                ResetCurrentAbility();
            }
        }

        if (stoppedByExecuteAbilityTime > 0f)
        {

            stoppedByExecuteAbilityTime -= Time.deltaTime;
            if (stoppedByExecuteAbilityTime <= 0f)
            {
                EnableCharacterMovement();
            }
        }

        if (attackAbilityCooldown > 0f)
        {
            attackAbilityCooldown -= Time.deltaTime;
        }
    }

    public virtual void EnableCharacterMovement() { }
    public virtual void StopCharacterMovement() { }

    private void FixedUpdate()
    {
        if (currentAbility != null && currentAbility.duration != 0f)
        {
            currentAbility.FixedUpdateAbility(this, Time.fixedDeltaTime, currentAbilityElapsedTime);
        }
    }

    public virtual void TakeDamage(float damage, DamageEmiterType emiterType)
    {
        
    }
}
