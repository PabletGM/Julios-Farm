using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddShield", menuName = "JuliosFarm/Abilities/Passives/AddShield")]
public class AddShield : BaseAbility
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
        UIManager.Instance.UpdateShieldBar(1f);
        FarmAbilityCharacter.Instance.InitialCurrentShield();
    }
}
