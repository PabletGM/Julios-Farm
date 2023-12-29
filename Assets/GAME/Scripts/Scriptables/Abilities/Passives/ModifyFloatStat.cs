using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModifyFloatStat", menuName = "JuliosFarm/Abilities/Passives/ModifyFloatStat")]
public class ModifyFloatStat : BaseAbility
{
    [Header("Modify Parameters")]
    public FloatVariable variableToChange;
    public float multiplier = 1f;

    public override void StartAbility(AbilityCharacter character)
    {
        if (variableToChange != null)
        {
            variableToChange.runTimeValue *= multiplier;
        }
    }
}
