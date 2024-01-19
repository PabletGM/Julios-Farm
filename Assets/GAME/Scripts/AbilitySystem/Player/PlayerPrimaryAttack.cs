using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


[CreateAssetMenu(fileName = "PlayerPrimaryAttack", menuName = "JuliosFarm/Abilities/PlayerPrimaryAttack")]
public class PlayerPrimaryAttack : BasePrimaryAttack
{
    public int ticks;
    private float interval;

    
    


    //duracion de vfx attack particle
    private float durationAttackParticle =0 ;
    private float MaxDurationAttackParticle;

    public override void StartAbility(AbilityCharacter character)
    {
        //conectamos con StartAbility de BasePrimaryAttack
        base.StartAbility(character);
        MaxDurationAttackParticle = 0.5f;
        character.Animator.SetTrigger("AttackTrigger");


        attackPlayerParticle(character);
        //numero de veces que se ejecuta en una duracion especifica
        interval = duration / ticks;
    }

    public  async void attackPlayerParticle(AbilityCharacter character)
    {
        //como no es monoBeahviour no puedo acceder a corrutinas ni a invoke
        await Task.Delay(250);
        PlayerAbilityCharacter.Instance.attackParticles.SetActive(true);   
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



        ////cronometro particulas ataque
        //durationAttackParticle+= Time.deltaTime;
        //// Verificar si ha pasado 1 segundo
        //if (durationAttackParticle >=MaxDurationAttackParticle)
        //{
           
        //    // Reiniciar el cronómetro
        //    durationAttackParticle = 0f;
        ////}

    }

    public override void EndAbility(AbilityCharacter character)
    {
        QuitarParticleAttack();
    }

    private  async void QuitarParticleAttack()
    {
        await Task.Delay(1000);
        PlayerAbilityCharacter.Instance.attackParticles.SetActive(false);
    }

    



    public override void OnReceiveAnimationEvent(AbilityCharacter character)
    {
        //particula de ataque
        attackPlayerParticle(character);
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
