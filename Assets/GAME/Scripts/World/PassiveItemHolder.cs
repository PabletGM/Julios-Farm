using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItemHolder : MonoBehaviour
{
    [SerializeField]
    private BaseAbility PassiveAbility;

    private void OnTriggerEnter(Collider other)
    {
        PlayerAbilityCharacter character = other.GetComponent<PlayerAbilityCharacter>();
        if (character != null)
        {
            character.AddPassiveAbility(PassiveAbility);
            Destroy(gameObject);
        }
    }
}
