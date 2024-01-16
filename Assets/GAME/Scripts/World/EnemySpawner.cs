using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnPoints;

    [SerializeField]
    private GameObject EnemyPrefabSimple;

    [SerializeField]
    private GameObject EnemyPrefabBoss;



    [HideInInspector]
    public float timeToSpawn = 3f;
    private float timeSinceSpawn;

    private bool permisoSpawnearEnemy = false;

    public static EnemySpawner Instance;

    private int numTotalEnemiesRound;

    private int numeroEnemiesCreados = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeSpawnEachRound(int numEnemiesRound,float timetoSpawn)
    {
        //cambiar numero de enemies por ronda
        numTotalEnemiesRound = numEnemiesRound;
        //cambiar timeToSpawn enemy
        timeToSpawn = timetoSpawn;
        //reiniciamos numero enemiesCreados
        numeroEnemiesCreados = 0;
    }

        //llama a funcionalidad crear 3 enemigos de golpe
        public void InstantiateEnemyNormal()
        {
            int poolIndex = ObjectPooler.instance.SearchPool(EnemyPrefabSimple);
            if (poolIndex != -1)
            {
                for (int i = 0; i < spawnPoints.Count; i++)
                {
                    StartCoroutine(CrearEnemigoPorLugar(numTotalEnemiesRound, poolIndex, i));
                    //SceneEnemiesController.Instance.AddEnemyToScene();
                    //GameController.Instance.AddEnemyAlive(this.gameObject.GetComponent<BasicEnemyAbilityCharacter>());
                }
            }
        }

   

    private IEnumerator CrearEnemigoPorLugar(int numeroDeEnemigosTotal, int poolIndex, int spawnPointNumber)
    {
        //todo el rato
        while (true)
        {
            //Funcionalidad y si no se ha pasado con el numero de enemies creados
            if(permisoSpawnearEnemy && numeroEnemiesCreados < numeroDeEnemigosTotal)
            {
                GameObject enemyGO = ObjectPooler.instance.GetPooledObject(poolIndex);
                enemyGO.transform.position = spawnPoints[spawnPointNumber].position;
                enemyGO.transform.rotation = spawnPoints[spawnPointNumber].rotation;
                enemyGO.GetComponent<BasicEnemyAbilityCharacter>().enabled =true;
                enemyGO.GetComponent<BasicEnemyAbilityCharacter>().CanDoAbilities = true;
                enemyGO.GetComponent<BasicEnemyAbilityCharacter>().RecuperarSaludParaFuturoRespawn();
                enemyGO.SetActive(true);
                GameController.Instance.AddEnemyAlive(enemyGO.GetComponent<BasicEnemyAbilityCharacter>());
                permisoSpawnearEnemy = false;
                //aumentar numero de enemigos creados
                numeroEnemiesCreados++;
            }


            // Esperar un segundo antes de repetir
            yield return null;
        }
    }

 


    //contador para dar permiso y spawnear bicho
    void Update()
    {
        //aumentar timer
        timeSinceSpawn += Time.deltaTime;
        //comprobamos si ha llegado al maximo
        if (timeSinceSpawn >= timeToSpawn)
        {
            //damos permiso
            permisoSpawnearEnemy = true;
            //reiniciar timer
            timeSinceSpawn = 0;
        }
    }
}