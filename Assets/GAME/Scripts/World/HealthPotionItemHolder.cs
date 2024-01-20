using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionItemHolder : MonoBehaviour
{
    [SerializeField]
    private BaseAbility PassiveAbility;

    // VFX Passive Picking Particles
    public GameObject pickParticles;


    private void OnTriggerEnter(Collider other)
    {
        PlayerAbilityCharacter character = other.GetComponent<PlayerAbilityCharacter>();
        if (character != null)
        {
            PickUp();
            character.AddPassiveAbility(PassiveAbility);

        }
    }

    //efecto de la pasiva al cogerla
    private void PickUp()
    {
        // VFX Passive Picking 
        int poolIndex = ObjectPooler.instance.SearchPool(pickParticles);
        if (poolIndex != -1)
        {
            GameObject particles = ObjectPooler.instance.GetPooledObject(poolIndex);
            particles.transform.position = this.transform.position;
            particles.SetActive(true);
        }
        AudioManagerPlayer.instance.TakePassive();
        Destroy(this.gameObject);
    }
}



