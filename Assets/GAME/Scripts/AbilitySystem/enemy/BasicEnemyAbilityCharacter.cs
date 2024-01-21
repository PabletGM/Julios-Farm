using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class BasicEnemyAbilityCharacter : AbilityCharacter
{
    protected NavMeshAgent agent;

    private PlayerManager playerManager;
    [HideInInspector]
    public EnemyStats enemyStats;

    protected float currentHealth;
    protected float maxHealth;

    [Header("Destino a atacar")]
    [SerializeField]
    private GameObject destinoAtacar;

    [SerializeField] private EnemyManager enemyManager;

    private float tiempoAnimacionMuerteBasicEnemy = 1f;
    private float tiempoAnimacionMuerteBossEnemy = 1.85f;

    // Death Particles
    public GameObject despawnParticles;
    public GameObject particlesPivot;
    //Movement Particles
    public GameObject movementParticles;

    [HideInInspector]
    public bool isDead = false;
    

    //public static BasicEnemyAbilityCharacter Instance;


    [HideInInspector]
    public IEnumerator hitEfect;

    public bool CanDoAbilities
    {
        get
        {
            return canDoAbilties;
        }
        set { canDoAbilties = value; }
    }

    public bool CanMove
    {
        get
        {
            return canMove;
        }
        set { canMove = value; }
    }

    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }
        set { maxHealth = value; }
    }

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //    else if (Instance != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //}



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
            resetHealthRespawn();

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

        //funcionalidad del update del AbilityCharacter
        base.Update();



        //calculamos distancia entre enemy y edificio pero solo en ejeX
        float distanceToPlayer = Vector3.Distance(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(destinoAtacar.transform.position.x, destinoAtacar.transform.position.y, destinoAtacar.transform.position.z));
        //mira si esta a distancia cercana del player
        if (IsNearToPlayer(distanceToPlayer))
        {
            //mira si el player está vivo
            if (IsPlayerAlive())
            {
                VeryCloseToPlayer(distanceToPlayer);
                //ponemos como objetivo del enemy la posicion del player
                agent.SetDestination(destinoAtacar.transform.position);              
               //miramos si la distancia del player <= el attackRange del enemy
                if (distanceToPlayer <= slotAttackAbility.attackRange.runTimeValue)
                {
                    //hacemos ataque del enemy
                    ExecutePrimaryAbility();
                    //sound
                    AttackEnemy(distanceToPlayer);
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
    //Movement Particle
    public IEnumerator movementEnemyParticle()
    {
        //Efecto Particulas
        if (movementParticles != null)
        {
            int poolIndex = ObjectPooler.instance.SearchPool(movementParticles);
            if (poolIndex != -1)
            {
                GameObject particles = ObjectPooler.instance.GetPooledObject(poolIndex);
                particles.transform.position = this.transform.position;
                particles.SetActive(true);
            }
        }
        yield return null;
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

    public void AssociateEnemyStats()
    {
        //Init agent paramenters 
        agent = GetComponent<NavMeshAgent>();
        enemyStats = (EnemyStats)characterStats;
        if (enemyStats != null)
        {
            maxHealth = enemyStats.maxHealth;
            resetHealthRespawn();

            //agent.speed = enemyStats.speed.runTimeValue;
            agent.angularSpeed = enemyStats.angularSpeed;
            agent.stoppingDistance = enemyStats.stoppingDistance;
            agent.autoBraking = enemyStats.autoBraking;
        }
    }

    public void ResetEnemyStats()
    {
        enemyStats = (EnemyStats)characterStats;
        if (this.gameObject.GetComponentInChildren<EnemyManager>().enemyName == "basicEnemy")
        {
            enemyStats.health.runTimeValue = 6f;
            enemyStats.maxHealth = 6f;
            enemyStats.speed.runTimeValue = 4f;
        }
        else if (this.gameObject.GetComponentInChildren<EnemyManager>().enemyName == "bossEnemy")
        {
            enemyStats.health.runTimeValue = 20f;
            enemyStats.maxHealth = 20f;
            enemyStats.speed.runTimeValue = 2f;
        }
    }

    protected virtual bool IsPlayerAlive()
    {
        return playerManager.CurrentHealth > 0;
    }

    //comprueba si la distancia entre player y enemy es < a el limite establecido y si tiene permiso para moverse(agent.isStopped = false)
    protected bool IsNearToPlayer(float distanceToPlayer)
    {
        if (enemyStats != null)
        {
            return distanceToPlayer <= enemyStats.aggroRadius && !agent.isStopped;
        }
        else
        {
            return false;
        }

    }

    //random sounds
    private void VeryCloseToPlayer(float distancePlayer)
    {
        int numPosibilidades = 80;
        int numElegido = 2;
        int randomValue = UnityEngine.Random.Range(0, numPosibilidades);

        if (randomValue == numElegido)
        {
            if (enemyStats != null)
            {
                if (distancePlayer <= 10)
                {
                    this.gameObject.GetComponentInChildren<AudioManagerEnemy>().RandomWalkSound();
                }
            }
        }
        
    }


    private void AttackEnemy(float distancePlayer)
    {
        int numPosibilidades = 5;
        int numElegido = 2;
        int randomValue = UnityEngine.Random.Range(0, numPosibilidades);

        if (randomValue == numElegido)
        {
            if (enemyStats != null)
            {
                if (distancePlayer <= 10)
                {
                    this.gameObject.GetComponentInChildren<AudioManagerEnemy>().RandomAttackSound();
                }
            }
        }

    }

    private void AttackHitEnemy()
    {
        int numPosibilidades = 2;
        int numElegido = 1;
        int randomValue = UnityEngine.Random.Range(0, numPosibilidades);

        if (randomValue == numElegido)
        {
            if (enemyStats != null)
            {
                
                    this.gameObject.GetComponentInChildren<AudioManagerEnemy>().RandomAttackVoice();
                
            }
        }

    }

    private void AttackDeathEnemy()
    {
        int numPosibilidades = 2;
        int numElegido = 1;
        int randomValue = UnityEngine.Random.Range(0, numPosibilidades);

        if (randomValue == numElegido)
        {
            if (enemyStats != null)
            {

                this.gameObject.GetComponentInChildren<AudioManagerEnemy>().RandomDeath();

            }
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
            //asocio la corutina
            AsociarHitEffectCoroutine();
            //vemos que no es nula
            if (hitEfect != null)
            {
                //la ejecutamos
                StartCoroutine(hitEfect);
            }
            currentHealth -= damage;
            //sonido hit
            AttackHitEnemy();

            //ponemos animacion Hit
            Animator.SetTrigger("HitTrigger");


            if (currentHealth <= 0f && !isDead)
            {
                //activamos animacion muerte
                Animator.SetTrigger("DeathTrigger");
                AttackDeathEnemy();
                isDead = true;

                    //que paren de moverse
                    Animator.SetBool("IsMoving", false);                
                    enemyManager.UpdateEnemyHealthBar(currentHealth / maxHealth);
                    //segun que enemigo sea una pausa u otra
                    agent.isStopped = true;
                    if (this.gameObject.GetComponentInChildren<EnemyManager>().enemyName == "basicEnemy")
                    {
                        Invoke("StopExistingEnemy", tiempoAnimacionMuerteBasicEnemy);
                    }
                    else if (this.gameObject.GetComponentInChildren<EnemyManager>().enemyName == "bossEnemy")
                    {
                        Invoke("StopExistingEnemy", tiempoAnimacionMuerteBossEnemy);
                    }


            }
                else
                {

                    enemyManager.UpdateEnemyHealthBar(currentHealth / maxHealth);
                }
        }


    }
    

        private void StopExistingEnemy()
        {
            if (hitEfect != null)
            {
                StopCoroutine(hitEfect);
                hitEfect = null;
            }


            ResetCurrentAbility();
            canDoAbilties = false;

            //se quita enemigo de la lista de enemigos in game
            GameController.Instance.RemoveEnemyAlive(this);
            if (GameController.Instance.enemyInGameList.Count == 0)
            {
                GameController.Instance.EndRound();
            }

            //Death Particles 
            int poolIndex = ObjectPooler.instance.SearchPool(despawnParticles);
            if (poolIndex != -1)
            {
                GameObject particles = ObjectPooler.instance.GetPooledObject(poolIndex);
                particles.transform.position = particlesPivot.transform.position;
                particles.SetActive(true);
            }
            //destruir
            this.enabled = false;
            //vida a tope para el futuro respawn
            resetHealthRespawn();
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }

        public void resetHealthRespawn()
        {
            currentHealth = maxHealth;
            enemyManager.UpdateEnemyHealthBar(maxHealth);
        }

        public void AsociarHitEffectCoroutine()
        {
            hitEfect = this.gameObject.GetComponent<hitEffect>().HitEffect();
        }
    
}
