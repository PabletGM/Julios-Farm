using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAbility : BaseAbility
{
    [Header("Attack Parameters")]
    public float damageAmount = 1;
    public float attackRadius;
    public float attackRange;
    public LayerMask attackLayerMask;
    public DamageEmiterType damageEmiterType;

    [Header("Attack Movement Parameters")]
    public float movementStopTime;

    protected float totalDamageAmount;

    public override void StartAbility(AbilityCharacter character)
    {
        base.StartAbility(character);
        //el movementStopTime el cronometro para bajar este es en el AbilityCharacter en el update
        if (movementStopTime > 0f)
        {
            character.StopCharacterMovement();
        }
        totalDamageAmount = damageAmount * character.CharacterStats.baseDamage.runTimeValue;
    }
}
