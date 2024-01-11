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
        //numero de veces que se ejecuta en una duracion especifica
        interval = duration / ticks;
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
        //Debug.Log("Deal Damage");
        //cuando haga daño que ya busque la lista de enemigos y vea cuales están cerca según la posicion

        //coges la lista de enemigos en el juego
        List<BasicEnemyAbilityCharacter> enemyInGame= GameController.Instance.enemyInGameList;
        //compruebas en cada enemigo de la lista su posicion
        for(int i = 0; i < enemyInGame.Count; i++)
        {
            //si su posicion del enemy - posicion del player <= attackRange ---> está cerca
            float distancePlayerEnemy = Vector3.Distance(enemyInGame[i].transform.position, character.transform.position);
            if (distancePlayerEnemy <= attackRange)
            {
                //está cerca de player
                enemyInGame[i].TakeDamage(totalDamageAmount, damageEmiterType);
            }
        }

        //Vector3 rayOrigin = character.transform.position + new Vector3(0f, 0.5f, 0f);

        //RaycastHit[] sphereCastHitInfo = new RaycastHit[10];
        //Physics.SphereCastNonAlloc(rayOrigin, attackRadius, character.transform.forward, sphereCastHitInfo, attackRange, attackLayerMask);
        //if (sphereCastHitInfo.Length > 0)
        //{
        //    for (int i = 0; i < sphereCastHitInfo.Length; i++)
        //    {
        //        if (sphereCastHitInfo[i].collider != null)
        //        {
        //            IDamageable damageableObject = sphereCastHitInfo[i].collider.GetComponent<IDamageable>();
        //            if (damageableObject != null)
        //            {
        //                Debug.DrawRay(sphereCastHitInfo[i].collider.transform.position, Vector3.up, Color.red, 2f);
        //                damageableObject.TakeDamage(totalDamageAmount, damageEmiterType);
        //            }
        //        }
        //    }
        //}
    }



    //hace daño solo cada cierto tiempo pero el update ability se ejecuta siempre y cuando haga daño que ya busque la lista

    //en el playerAbility preguntas si tienes pasiva si es loop, hacer daño cada x ticks como en el dash por ticks, en el momento del tick recorro la lista 
}
