using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAbility : ScriptableObject
{
    [Header("BaseAbility Parameters")]
    public bool isPassive = false;
    public string abilityDescription = "";

    // 0 means instant ability
    public float duration = 0f;
    public float cooldown = 0f;

    public virtual void StartAbility(AbilityCharacter character)
    {

    }

    public virtual void UpdateAbility(AbilityCharacter character, float deltaTime, float elapsedTime)
    {

    }

    public virtual void FixedUpdateAbility(AbilityCharacter character, float fixedDeltaTime, float elapsedTime)
    {

    }

    public virtual void OnReceiveAnimationEvent(AbilityCharacter character)
    {

    }

    public virtual void EndAbility(AbilityCharacter character)
    {

    }
}
