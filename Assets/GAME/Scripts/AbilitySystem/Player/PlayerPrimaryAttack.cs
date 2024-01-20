using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.TextCore.Text;


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
        AudioManagerPlayer.instance.RandomAttackSound();
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
                    //se ejecuta y hace da�o x veces por segundo
                    MakeDamage(character.transform);
                    MakeDamageBarrel(character.transform);
                }
                
            }
        }



        ////cronometro particulas ataque
        //durationAttackParticle+= Time.deltaTime;
        //// Verificar si ha pasado 1 segundo
        //if (durationAttackParticle >=MaxDurationAttackParticle)
        //{
           
        //    // Reiniciar el cron�metro
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
        //attackPlayerParticle(character);
    }

    private void MakeDamage(Transform character)
    {
        //cuando haga da�o que ya busque la lista de enemigos y vea cuales est�n cerca seg�n la posicion
        //coges la lista de enemigos en el juego
        List<BasicEnemyAbilityCharacter> enemyInGame= GameController.Instance.enemyInGameList;
        //compruebas en cada enemigo de la lista su posicion
        for(int i = 0; i < enemyInGame.Count; i++)
        {
            //si su posicion del enemy - posicion del player <= attackRange ---> est� cerca
            float distancePlayerEnemy = Vector3.Distance(enemyInGame[i].transform.position, character.transform.position);
            if (distancePlayerEnemy <= attackRange.runTimeValue)
            {
                if (enemyInGame[i] != null)
                {
                    //est� cerca de player
                    enemyInGame[i].TakeDamage(totalDamageAmount, damageEmiterType);

                    //cuando hace da�o a un enemy, una posibilidad de 1/5 de que haga un grito de guerra
                    WarShoutMaybe();
                }
                
            }
        }  
    }

    private void MakeDamageBarrel(Transform character)
    {
        //cuando haga da�o que ya busque la lista de enemigos y vea cuales est�n cerca seg�n la posicion
        //coges la lista de enemigos en el juego
        List<DamageableBarrel> barrelInGame = GameController.Instance.barrelInGameList;
        //compruebas en cada enemigo de la lista su posicion
        for (int i = 0; i < barrelInGame.Count; i++)
        {
            //si su posicion del enemy - posicion del player <= attackRange ---> est� cerca
            float distancePlayerBarrel = Vector3.Distance(barrelInGame[i].transform.position, character.transform.position);
            //Debug.Log(distancePlayerBarrel);
            if (distancePlayerBarrel <= attackRange.runTimeValue)
            {
                if (barrelInGame[i] != null)
                {
                    //est� cerca de player
                    barrelInGame[i].TakeDamage(totalDamageAmount, damageEmiterType);
                    barrelInGame.Remove(barrelInGame[i]);

                    //cuando hace da�o a un enemy, una posibilidad de 1/5 de que haga un grito de guerra
                    //WarShoutMaybe();
                }

            }
        }
    }

    private void WarShoutMaybe()
    {
        int numPosibilidades = 8;
        int numElegido = 2;
        int randomValue = UnityEngine.Random.Range(0,numPosibilidades);
        if(randomValue == numElegido)
        {
            AudioManagerPlayer.instance.RandomAttackVoice();
        }

    }



   
}
