using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BarrelAbilityCharacter : AbilityCharacter
{

    [SerializeField]
    private GameObject brokenBarrel;

    public override void TakeDamage(float damage, DamageEmiterType emiterType)
    {
        //if (destroyParticles != null)
        //{
        //    int poolIndex = ObjectPooler.instance.SearchPool(destroyParticles);
        //    if (poolIndex != -1)
        //    {
        //        GameObject particles = ObjectPooler.instance.GetPooledObject(poolIndex);
        //        particles.transform.position = this.transform.position + particlesOffsetPosition;
        //        particles.SetActive(true);
        //    }

        //}
        Debug.Log("Damage Barrel");
        brokenBarrel.transform.SetParent(null);
        brokenBarrel.gameObject.SetActive(true);
        Destroy(gameObject);
    }
}
