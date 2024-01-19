using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPrimaryAttack", menuName = "JuliosFarm/Abilities/PlayerPrimaryAttack")]
public class PlayerPrimaryAttack : BasePrimaryAttack
{
    public int ticks;
    private float interval;

    public override void StartAbility(AbilityCharacter character)
    {
        //conectamos con StartAbility de BasePrimaryAttack
        base.StartAbility(character);
        character.Animator.SetTrigger("AttackTrigger");
        attackPlayerParticle(character);
        //numero de veces que se ejecuta en una duracion especifica
        interval = duration / ticks;
    }

    public void attackPlayerParticle(AbilityCharacter character)
    {
        //Efecto Particulas
        if (attackParticles != null)
        {
            int poolIndex = ObjectPooler.instance.SearchPool(attackParticles);
            if (poolIndex != -1)
            {
                GameObject particles = ObjectPooler.instance.GetPooledObject(poolIndex);
                particles.transform.position = character.transform.position;
                particles.SetActive(true);
            }
        }
    }

    public override void UpdateAbility(AbilityCharacter character, float deltaTime, float elapsedTime)
    {
        base.UpdateAbility(character, deltaTime, elapsedTime);

        if (elapsedTime % interval <= deltaTime)
        {
            if ((elapsedTime / interval) < ticks)
            {
                if(character!=null)
                {
                    //se ejecuta y hace daño 3 veces por segundo
                    MakeDamage(character.transform);
                }
                
            }
        }
    }

    public override void OnReceiveAnimationEvent(AbilityCharacter character)
    {
        //MakeDamage(character.transform);
    }

    private void MakeDamage(Transform character)
    {
        //cuando haga daño que ya busque la lista de enemigos y vea cuales están cerca según la posicion

        //coges la lista de enemigos en el juego
        List<BasicEnemyAbilityCharacter> enemyInGame= GameController.Instance.enemyInGameList;
        //compruebas en cada enemigo de la lista su posicion
        for(int i = 0; i < enemyInGame.Count; i++)
        {
            //si su posicion del enemy - posicion del player <= attackRange ---> está cerca
            float distancePlayerEnemy = Vector3.Distance(enemyInGame[i].transform.position, character.transform.position);
            if (distancePlayerEnemy <= attackRange.runTimeValue)
            {
                if (enemyInGame[i] != null)
                {
                    //está cerca de player
                    enemyInGame[i].TakeDamage(totalDamageAmount, damageEmiterType);
                }
                
            }
        }  
    }



   
}
