using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class PlayerAbilityCharacter : AbilityCharacter
{
    private PlayerMovement playerMovement;

    private int layerEnemy;

    

    public bool CanMovePlayer
    {
        get
        {
            return canMove;
        }
        set
        {
            canMove = value;
        }
    }
    
    private void Awake()
    {
        //iniciar layer de player
         layerEnemy= LayerMask.NameToLayer("Enemy");

       


    }

    protected override void InitAbilityCharacter()
    {
        base.InitAbilityCharacter();
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.CanMovement = true;
        //empezar corroutine player atack
        StartCoroutine(AtaqueAutomaticoPlayer());
    }


    protected override void ResetCurrentAbility()
    {
        if (!playerMovement.CanMovement)
        {
            playerMovement.CanMovement = true;
        }

        base.ResetCurrentAbility();
    }

    public override void EnableCharacterMovement()
    {
        base.EnableCharacterMovement();
        playerMovement.CanMovement = true;
    }

    public override void StopCharacterMovement()
    {
        base.StopCharacterMovement();
        playerMovement.CanMovement = false;
    }

    public override void TakeDamage(float damage, DamageEmiterType emiterType)
    {
        //si el que hace el daño es el player se ignora
        if (emiterType == DamageEmiterType.Player)
        {
            return;
        }
        else if(emiterType == DamageEmiterType.Enemy)
        {
            Debug.Log("daño a player");
        }

       
    }

    //al detectar al enemy le ataque
    //private void OnTriggerEnter(Collider other)
    //{
    //    //si detecta a enemy que lo haga
    //    if(other.gameObject.layer == layerEnemy)
    //    {
    //        //Debug.Log("Attack");
    //        /*ExecutePrimaryAbility();*/
    //    }
        
    //}

    private IEnumerator AtaqueAutomaticoPlayer()
    {
        //se ejecuta todo el rato mientras player exista
        while (this.gameObject!=null)
        {
            // Lógica de ataque aquí
            ExecutePrimaryAbility();

            // Esperar el tiempo entre ataques attackAbilityCooldown
            yield return new WaitForSeconds(attackAbilityCooldown);
        }
    }

    //private void PlayerDied()
    //{
    //    ResetCurrentAbility();
    //    canDoAbilties = false;
    //    //playerMovement.CanMove = false;

    //    Animator.SetTrigger("IsDied");

    //    StartCoroutine(ShowPlayerDied());
    //}

    //IEnumerator ShowPlayerDied()
    //{
    //    yield return new WaitForSeconds(1f);
    //    CharacterStats.ResetStats();
    //    PlayerManager.instance.PlayerDied();
    //}
}
