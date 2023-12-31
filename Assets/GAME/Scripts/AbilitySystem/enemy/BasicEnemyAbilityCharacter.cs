using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyAbilityCharacter : AbilityCharacter
{
    //[Header("Enemy Parameters")]
    //[Header("Destroy Particles")]
    //public GameObject despawnParticles;
    //public GameObject particlesPivot;

    protected NavMeshAgent agent;

    private PlayerManager playerManager;
    private EnemyStats enemyStats;

    protected float currentHealth;

    [Header("Destino a atacar")]
    [SerializeField]
    private GameObject destinoAtacar;

    //IEnumerator DestroyEnemyCoroutine;

    //iniciamos abilityCharacter, enemyStats y variables del navmesh
    protected override void InitAbilityCharacter()
    {
        base.InitAbilityCharacter();

        //Init agent paramenters 
        agent = GetComponent<NavMeshAgent>();
        enemyStats = (EnemyStats)characterStats;
        if (enemyStats != null)
        {
            currentHealth = enemyStats.health;

            agent.speed = enemyStats.speed.runTimeValue;
            agent.angularSpeed = enemyStats.angularSpeed;
            agent.stoppingDistance = enemyStats.stoppingDistance;
            agent.autoBraking = enemyStats.autoBraking;
        }

        //Get player reference
        playerManager = PlayerManager.instance;
        //inicialmente que le persiga a la casa
        agent.SetDestination(destinoAtacar.transform.position);
    }

    protected override void Update()
    {
        //si el enemy esta muerto o no puede hacer habilidades no hace nada 
        if (!canDoAbilties && currentHealth <= 0)
        {
            return;
        }

        //funcionalidad del update del AbilityCharacter
        base.Update();

        //calculamos distancia entre player y enemy
        //float distanceToPlayer = Vector3.Distance(transform.position, playerManager.transform.position);

        //calculamos distancia entre enemy y edificio pero solo en ejeX
        float distanceToPlayer = Vector3.Distance(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(destinoAtacar.transform.position.x, destinoAtacar.transform.position.y, destinoAtacar.transform.position.z));
        //mira si esta a distancia cercana del player
        if (IsNearToPlayer(distanceToPlayer))
        {
            //mira si el player est� vivo
            if (IsPlayerAlive())
            {
                //ponemos como objetivo del enemy la posicion del player
                agent.SetDestination(destinoAtacar.transform.position);
                //miramos si la distancia del player <= el attackRange del enemy
                if (distanceToPlayer <= slotAttackAbility.attackRange)
                {
                    //hacemos ataque del enemy
                    ExecutePrimaryAbility();
                }
            }
            //si el jugador no esta vivo paramos el characterMovement
            else
            {
                StopCharacterMovement();
                //Animator.SetTrigger("IsLaughing");
            }
        }

        Animator.SetBool("IsMoving", IsNearToPlayer(distanceToPlayer) && IsPlayerAlive());
    }

    public override void EnableCharacterMovement()
    {
        base.EnableCharacterMovement();
        agent.isStopped = false;
    }

    public override void StopCharacterMovement()
    {
        base.StopCharacterMovement();
        agent.isStopped = true;
    }

    protected virtual bool IsPlayerAlive()
    {
        return playerManager.CurrentHealth > 0;
    }

    //comprueba si la distancia entre player y enemy es < a el limite establecido y si tiene permiso para moverse(agent.isStopped = false)
    protected bool IsNearToPlayer(float distanceToPlayer)
    {
        return distanceToPlayer <= enemyStats.aggroRadius && !agent.isStopped;
    }

    public override void TakeDamage(float damage, DamageEmiterType emiterType)
    {
        if (emiterType == DamageEmiterType.Enemy || currentHealth < 0f)
        {
            return;
        }

        //si el emisor del da�o es el player
        else if(emiterType == DamageEmiterType.Player)
        {    
            currentHealth -= damage;
            //Debug.Log(currentHealth);
            if (currentHealth <= 0f)
            {
                ResetCurrentAbility();
                canDoAbilties = false;
                Destroy(this.gameObject);
                //se quita enemigo de la lista de enemigos in game
                GameController.Instance.RemoveEnemyAlive(this);
            }
        }

        
    }

    //private IEnumerator DestroyEnemy()
    //{
    //    float timeToWait = 2.5f;
    //    while (timeToWait >= 0)
    //    {
    //        yield return null;
    //        timeToWait -= Time.deltaTime;
    //    }

    //    DestroyEnemyCoroutine = null;
    //    int poolIndex = ObjectPooler.instance.SearchPool(despawnParticles);
    //    if (poolIndex != -1)
    //    {
    //        GameObject particles = ObjectPooler.instance.GetPooledObject(poolIndex);
    //        particles.transform.position = particlesPivot.transform.position;
    //        particles.SetActive(true);
    //    }
    //    //SceneEnemiesController.Instance.RemoveEnemyFromScene();
    //    currentHealth = characterStats.health;
    //    Animator.Rebind();
    //    agent.isStopped = false;
    //    canDoAbilties = true;
    //    gameObject.SetActive(false);
    //}
}
