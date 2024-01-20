using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "JuliosFarm/Abilities/HealthPotion")]
public class HealthPotion : BaseAbility
{
    
    public float amountToHeal = 1f;

    public override void StartAbility(AbilityCharacter character)
    {
        FarmAbilityCharacter.Instance.Heal(amountToHeal);
    }
}
