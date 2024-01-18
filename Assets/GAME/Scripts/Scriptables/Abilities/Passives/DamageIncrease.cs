using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageIncrease", menuName = "JuliosFarm/Abilities/Passives/DamageIncrease")]
public class DamageIncrease : BaseAbility
{
    [Header("Modify Parameters")]
    public FloatVariable variableToChange;
    public float amountToAdd = 1f;

    public override void StartAbility(AbilityCharacter character)
    {
        if (variableToChange != null)
        {
            variableToChange.runTimeValue += amountToAdd;
            PlayerManager.instance.ChangeRakes();
        }
    }
}
