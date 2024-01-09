using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class PlayerAbilityCharacter : AbilityCharacter
{
    private PlayerMovement playerMovement;

    private int layerEnemy;

    //Lista de enemigos que estan cerca del player
    private List<BasicEnemyAbilityCharacter> enemyNearPlayerList;


    public bool CanMovePlayer
    {
        get
        {
            return canMove;
        }
        set
        {
            canMove = value;
        }
    }
    
    private void Awake()
    {
        //iniciar layer de player
        layerEnemy= LayerMask.NameToLayer("Enemy");
        // Inicializar la lista en el Awake
        enemyNearPlayerList = new List<BasicEnemyAbilityCharacter>();
    }

    // Método para añadir un enemigo a la lista de enemiesNearPlayer
    public void AddEnemyListNearPlayer(BasicEnemyAbilityCharacter enemy)
    {
        enemyNearPlayerList.Add(enemy);
        Debug.Log(enemyNearPlayerList.Count);
    }

    // Método para quitar un enemigo de la lista de enemiesNearPlayer
    public void RemoveEnemyListNearPlayer(BasicEnemyAbilityCharacter enemy)
    {
        enemyNearPlayerList.Remove(enemy);
        Debug.Log(enemyNearPlayerList.Count);
    }

    protected override void InitAbilityCharacter()
    {
        base.InitAbilityCharacter();
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.CanMovement = true;
        //empezar corroutine player atack
        StartCoroutine(AtaqueAutomaticoPlayer());
    }


    protected override void ResetCurrentAbility()
    {
        if (!playerMovement.CanMovement)
        {
            playerMovement.CanMovement = true;
        }

        base.ResetCurrentAbility();
    }

    public override void EnableCharacterMovement()
    {
        base.EnableCharacterMovement();
        playerMovement.CanMovement = true;
    }

    public override void StopCharacterMovement()
    {
        base.StopCharacterMovement();
        playerMovement.CanMovement = false;
    }

    public override void TakeDamage(float damage, DamageEmiterType emiterType)
    {
        //si el que hace el daño es el player se ignora
        if (emiterType == DamageEmiterType.Player)
        {
            return;
        }
        else if(emiterType == DamageEmiterType.Enemy)
        {
            //PlayerManager.instance.CurrentHealth -= 1;
            //Debug.Log(PlayerManager.instance.CurrentHealth);
            //if (PlayerManager.instance.CurrentHealth <= 0)
            //{
            //    Debug.Log("Player Dead");
            //}
        }

       
    }

    //al detectar al enemy le añada a la lista
    private void OnTriggerEnter(Collider other)
    {
        //distancia entre player y enemy
        Vector3 distanceFromPlayerToEnemy = this.transform.position - other.transform.position;
        float sqrtDist = distanceFromPlayerToEnemy.sqrMagnitude;

        //cogemos el attackRange que es el radius del sphereCollider del attackArea del PlayerCharacter
        float attackRange = this.gameObject.GetComponentInChildren<SphereCollider>().radius;
        //si la distancia entre player y enemy supera un minimo le mete a la lista
        if(sqrtDist <= attackRange * attackRange)
        {
            //añadir en lista
            AddEnemyListNearPlayer(other.gameObject.GetComponent<BasicEnemyAbilityCharacter>());
        }
    }

    //al no detectar el enemy le saca de la lista
    private void OnTriggerExit(Collider other)
    {
        //distancia entre player y enemy
        Vector3 distanceFromPlayerToEnemy = this.transform.position - other.transform.position;
        float sqrtDist = distanceFromPlayerToEnemy.sqrMagnitude;

        //cogemos el attackRange que es el radius del sphereCollider del attackArea del PlayerCharacter
        float attackRange = this.gameObject.GetComponentInChildren<SphereCollider>().radius;
        //si la distancia entre player y enemy supera un minimo le mete a la lista
        if (sqrtDist > attackRange * attackRange)
        {
            //añadir en lista
            RemoveEnemyListNearPlayer(other.gameObject.GetComponent<BasicEnemyAbilityCharacter>());
        }
    }

    private IEnumerator AtaqueAutomaticoPlayer()
    {
        //se ejecuta todo el rato mientras player exista
        while (this.gameObject!=null)
        {
            //ataca cada cierto tiempo a todos los enemigos de la lista añadidos
            ExecutePrimaryAbility();

            // Esperar el tiempo entre ataques attackAbilityCooldown
            yield return new WaitForSeconds(attackAbilityCooldown);
        }
    }

    //private void PlayerDied()
    //{
    //    ResetCurrentAbility();
    //    canDoAbilties = false;
    //    //playerMovement.CanMove = false;

    //    Animator.SetTrigger("IsDied");

    //    StartCoroutine(ShowPlayerDied());
    //}

    //IEnumerator ShowPlayerDied()
    //{
    //    yield return new WaitForSeconds(1f);
    //    CharacterStats.ResetStats();
    //    PlayerManager.instance.PlayerDied();
    //}
}
