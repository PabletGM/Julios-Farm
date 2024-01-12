using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModifyTwoFloatStats", menuName = "JuliosFarm/Abilities/Passives/ModifyTwoFloatStats")]
public class ModifyTwoFloatStats : BaseAbility
{
    [Header("Modify Parameters")]
    public FloatVariable variableToChange;
    public float multiplier = 1f;

    [Header("Modify Parameters Second Variable")]
    public FloatVariable variableToChangeTwo;
    public float multiplierTwo = 1f;

    public override void StartAbility(AbilityCharacter character)
    {
        if (variableToChange != null)
        {
            variableToChange.runTimeValue *= multiplier;
            variableToChangeTwo.runTimeValue *= multiplierTwo;
        }
    }
}
