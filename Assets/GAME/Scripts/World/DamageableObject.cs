//using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour, IDamageable
{
    //[SerializeField]
    //protected DOTweenAnimation hitAnimation;

    [SerializeField]
    private GameObject destroyParticles;
    [SerializeField]
    private Vector3 particlesOffsetPosition;


    public virtual void TakeDamage(float damage, DamageEmiterType emiterType)
    {
        if (destroyParticles != null)
        {
            int poolIndex = ObjectPooler.instance.SearchPool(destroyParticles);
            if (poolIndex != -1)
            {
                GameObject particles = ObjectPooler.instance.GetPooledObject(poolIndex);
                particles.transform.position = this.transform.position + particlesOffsetPosition;
                particles.SetActive(true);
            }

        }
        Destroy(gameObject);
    }
}
