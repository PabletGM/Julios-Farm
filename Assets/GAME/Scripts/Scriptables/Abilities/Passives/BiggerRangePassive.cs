using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BiggerRangePassive", menuName = "JuliosFarm/Abilities/Passives/BiggerRangePassive")]
public class BiggerRangePassive : BaseAbility
{
    [Header("Modify Parameters")]
    public FloatVariable Range;
    public float multiplier = 1f;

    public override void StartAbility(AbilityCharacter character)
    {
        if (Range != null)
        {
            Range.runTimeValue *= multiplier;
            PlayerAbilityCharacter.Instance.rangeParticles.transform.localScale *= multiplier;
            PlayerAbilityCharacter.Instance.rangeParticles.transform.localScale += new Vector3(multiplier , multiplier, multiplier);
        }
    }
}
