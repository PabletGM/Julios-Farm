using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddFloatStat", menuName = "JuliosFarm/Abilities/Passives/AddFloatStat")]
public class AddFloatStat : BaseAbility
{
    [Header("Modify Parameters")]
    public FloatVariable variableToChange;
    public float amountToAdd = 1f;

    public override void StartAbility(AbilityCharacter character)
    {
        if (variableToChange != null)
        {
            variableToChange.runTimeValue += amountToAdd;
        }
    }
}
