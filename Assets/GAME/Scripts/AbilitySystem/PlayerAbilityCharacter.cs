using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class PlayerAbilityCharacter : AbilityCharacter
{
    private PlayerMovement playerMovement;

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
        
    }

    // M�todo para a�adir un enemigo a la lista de enemiesNearPlayer
    

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
        //si el que hace el da�o es el player se ignora
        if (emiterType == DamageEmiterType.Player)
        {
            return;
        }
        else if(emiterType == DamageEmiterType.Enemy)
        {
           
        }

       
    }

    //al detectar al enemy le a�ada a la lista
    

    private IEnumerator AtaqueAutomaticoPlayer()
    {
        //se ejecuta todo el rato mientras player exista
        while (this.gameObject!=null)
        {
            //ataca cada cierto tiempo a todos los enemigos de la lista a�adidos
            ExecutePrimaryAbility();

            // Esperar el tiempo entre ataques animacion attackAbilityCooldown
            yield return new WaitForSeconds(slotAttackAbility.tiempoAtaqueAnimacion);
        }
    }

}
