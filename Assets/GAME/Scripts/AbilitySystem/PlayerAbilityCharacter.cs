using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class PlayerAbilityCharacter : AbilityCharacter
{
    //private PlayerMovement playerMovement;

    protected override void InitAbilityCharacter()
    {
        base.InitAbilityCharacter();
        //playerMovement = GetComponent<PlayerMovement>();
        //playerMovement.CanMove = true;
    }


    protected override void ResetCurrentAbility()
    {
        //if (!playerMovement.CanMove)
        //{
        //    playerMovement.CanMove = true;
        //}

        base.ResetCurrentAbility();
    }

    public override void EnableCharacterMovement()
    {
        base.EnableCharacterMovement();
        //playerMovement.CanMove = true;
    }

    public override void StopCharacterMovement()
    {
        base.StopCharacterMovement();
        //playerMovement.CanMove = false;
    }

    public override void TakeDamage(float damage, DamageEmiterType emiterType)
    {
        if (emiterType == DamageEmiterType.Player)
        {
            return;
        }

        Debug.Log("daño a player");
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
