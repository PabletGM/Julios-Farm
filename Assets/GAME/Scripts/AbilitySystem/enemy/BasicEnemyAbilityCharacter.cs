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
    protected EnemyStats enemyStats;

    protected float currentHealth;
    protected float maxHealth; 

    [Header("Destino a atacar")]
    [SerializeField]
    private GameObject destinoAtacar;

    [SerializeField] private EnemyManager enemyManager;

    public bool CanDoAbilities 
    {
        get
        {
            return canDoAbilties;
        }
        set {canDoAbilties = value;}
    }

    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }
        set { maxHealth = value; }
    }



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
            maxHealth = enemyStats.maxHealth;
            RecuperarSaludParaFuturoRespawn();

            //agent.speed = enemyStats.speed.runTimeValue;
            agent.angularSpeed = enemyStats.angularSpeed;
            agent.stoppingDistance = enemyStats.stoppingDistance;
            agent.autoBraking = enemyStats.autoBraking;
        }

        //Get player reference
        playerManager = PlayerManager.instance;
        //inicialmente que le persiga a la casa
        agent.SetDestination(destinoAtacar.transform.position);
        Animator.SetFloat("CycleOffset", Random.Range(0f, 1f));

    }

    protected override void Update()
    {
        //si el enemy esta muerto o no puede hacer habilidades no hace nada 
        if (!canDoAbilties && currentHealth <= 0)
        {
            return;
        }
        agent.speed = enemyStats.speed.runTimeValue;
        //Debug.Log(agent.speed);

        //funcionalidad del update del AbilityCharacter
        base.Update();

        //calculamos distancia entre player y enemy
        //float distanceToPlayer = Vector3.Distance(transform.position, playerManager.transform.position);

        //calculamos distancia entre enemy y edificio pero solo en ejeX
        float distanceToPlayer = Vector3.Distance(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(destinoAtacar.transform.position.x, destinoAtacar.transform.position.y, destinoAtacar.transform.position.z));
        //mira si esta a distancia cercana del player
        if (IsNearToPlayer(distanceToPlayer))
        {
            //mira si el player está vivo
            if (IsPlayerAlive())
            {
                //ponemos como objetivo del enemy la posicion del player
                agent.SetDestination(destinoAtacar.transform.position);
                //miramos si la distancia del player <= el attackRange del enemy
                if (distanceToPlayer <= slotAttackAbility.attackRange.runTimeValue)
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
        if(enemyStats!=null)
        {
            return distanceToPlayer <= enemyStats.aggroRadius && !agent.isStopped;
        }
        else
        {
            return false;
        }
       
    }

    public override void TakeDamage(float damage, DamageEmiterType emiterType)
    {
        if (emiterType == DamageEmiterType.Enemy || currentHealth < 0f)
        {
            return;
        }

    

        //si el emisor del daño es el player
        if (emiterType == DamageEmiterType.Player)
        {    
            currentHealth -= damage;
            //Debug.Log("Take Damage");
                    // EnemyHealthBar UI
            //Debug.Log(currentHealth);
            if (currentHealth <= 0f)
            {
                ResetCurrentAbility();
                canDoAbilties = false;

                //se quita enemigo de la lista de enemigos in game
                GameController.Instance.RemoveEnemyAlive(this);
                if(GameController.Instance.enemyInGameList.Count == 0)
                {
                    AbilitiesPerRound.Instance.MetodoFuncionalidadCrear3PasivasRonda();
                    GameController.Instance.BreakTime(true);
                    GameController.Instance.IsGameWon();
                }

                //destruir
                this.enabled = false;
                //vida a tope para el futuro respawn
                RecuperarSaludParaFuturoRespawn();
                this.gameObject.SetActive(false);
                //Destroy(this.gameObject);
                
            }
            else
            {
               
                enemyManager.UpdateEnemyHealthBar(currentHealth / maxHealth);
            }
        }

        
    }

    public void RecuperarSaludParaFuturoRespawn()
    {
        currentHealth = maxHealth;
        enemyManager.UpdateEnemyHealthBar(maxHealth);
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
