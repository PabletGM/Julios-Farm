using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrimaryAttack : BasePrimaryAttack
{
  
    public override void StartAbility(AbilityCharacter character)
    {
        //conectamos con StartAbility de BasePrimaryAttack
        base.StartAbility(character);

    }

    public override void OnReceiveAnimationEvent(AbilityCharacter character)
    {
        
    }
}
