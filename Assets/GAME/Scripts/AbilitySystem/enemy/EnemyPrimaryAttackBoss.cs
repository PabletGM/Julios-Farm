using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "EnemyPrimaryAttackBoss", menuName = "JuliosFarm/Abilities/EnemyPrimaryAttackBoss")]
public class EnemyPrimaryAttackBoss : EnemyPrimaryAttack
{
    //Attack Particles
    public GameObject attackBossParticles;

    public int ticks;
    private float interval;

    //duracion de vfx attack particle
    private float durationAttackParticle = 0;
    private float MaxDurationAttackParticle;

    private GameObject particlesBossAttack;

    public override void StartAbility(AbilityCharacter character)
    {
        base.StartAbility(character);
        MaxDurationAttackParticle = 0.5f;
        character.Animator.SetTrigger("PrimaryAttack");

        attackEnemyBossParticle(character);
        //numero de veces que se ejecuta en una duracion especifica
        interval = duration / ticks;
    }

    public async void attackEnemyBossParticle(AbilityCharacter character)
    {
        //como no es monoBeahviour no puedo acceder a corrutinas ni a invoke
        await Task.Delay(1000);
        //Efecto Particulas
        GameObject particles = particlesBossAttack;
        if (attackBossParticles != null)
        {
            int poolIndex = ObjectPooler.instance.SearchPool(attackBossParticles);
            if (poolIndex != -1)
            {
                particles = ObjectPooler.instance.GetPooledObject(poolIndex);
                particles.transform.position = character.transform.position + new Vector3(1, 2, 0);
                particles.SetActive(true);
            }
        }
        AudioManagerPlayer.instance.RandomAttackSound();
        QuitarParticleAttack(particles);
    }

    private async void QuitarParticleAttack(GameObject particles)
    {
        await Task.Delay(250);
        particles.SetActive(false);
    }

    public override void OnReceiveAnimationEvent(AbilityCharacter character)
    {
        Vector3 rayOrigin = character.transform.position + new Vector3(0f, 0.5f, 0f);

        RaycastHit[] sphereCastHitInfo = new RaycastHit[10];
        Physics.SphereCastNonAlloc(rayOrigin, attackRadius, character.transform.forward, sphereCastHitInfo, attackRange.runTimeValue, attackLayerMask);
        if (sphereCastHitInfo.Length > 0)
        {
            for (int i = 0; i < sphereCastHitInfo.Length; i++)
            {
                if (sphereCastHitInfo[i].collider != null)
                {
                    IDamageable damageableObject = sphereCastHitInfo[i].collider.GetComponent<IDamageable>();
                    if (damageableObject != null)
                    {
                        Debug.DrawRay(sphereCastHitInfo[i].collider.transform.position, Vector3.up, Color.red, 2f);
                        damageableObject.TakeDamage(totalDamageAmount, damageEmiterType);
                    }
                }
            }
        }
    }
}

