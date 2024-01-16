using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddShield", menuName = "JuliosFarm/Abilities/Passives/AddShield")]
public class AddShield : BaseAbility
{
    [Header("Modify Parameters")]
    public FloatVariable shield;
    public float amountToAdd;
    public FloatVariable maxShield;

    public override void StartAbility(AbilityCharacter character)
    {
        if (shield != null && maxShield != null)
        {
            shield.runTimeValue += amountToAdd;
            maxShield.runTimeValue = shield.runTimeValue;
        }
        UIManager.Instance.UpdateShieldBar(1f);
        FarmAbilityCharacter.Instance.InitialCurrentShield();
    }
}
