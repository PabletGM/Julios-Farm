using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasePrimaryAttack", menuName = "JuliosFarm/Abilities/BasePrimaryAttack")]
public class BasePrimaryAttack : AttackAbility
{
    // Attack Particles
    public GameObject attackParticles;

    public override void StartAbility(AbilityCharacter character)
    {
        //conectamos con StartAbility de AttackAbility
        base.StartAbility(character);
        
    }

    public override void OnReceiveAnimationEvent(AbilityCharacter character)
    {
        

       
    }
}
