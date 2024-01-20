using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableBarrel : MonoBehaviour, IDamageable
{
    [SerializeField]
    private GameObject brokenBarrel;

    [Header("Potion Parameters")]
    [SerializeField]
    private GameObject healthPotionPrefab;
    [SerializeField]
    private Vector3 offset;
    [Header("Number between 0-10, 0 is never drop, 10 is always drop")]
    [Range(0, 10)]
    [SerializeField]
    private int dropRate;

    // VFX Barrell 
    public GameObject BarrelParticles;

    public void TakeDamage(float damage, DamageEmiterType emiterType)
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
        ChanceToDropPotion();
        brokenBarrel.transform.SetParent(null);
        brokenBarrel.gameObject.SetActive(true);
        Destroy(gameObject);
    }

    private void ChanceToDropPotion()
    {
        int randomChance = Random.Range(0, 10);
        destroyingBarrel();
        if (randomChance < dropRate)
        {
            Instantiate(healthPotionPrefab, this.transform.position + offset, this.transform.rotation);
        }
    }

    // VFX  Barrel Breaking  
    private void destroyingBarrel()
    {
        // VFX Barrel Picking 
        int poolIndex = ObjectPooler.instance.SearchPool(BarrelParticles);
        if (poolIndex != -1)
        {
            GameObject particles = ObjectPooler.instance.GetPooledObject(poolIndex);
            particles.transform.position = this.transform.position;
            particles.SetActive(true);
        }
    }
}
