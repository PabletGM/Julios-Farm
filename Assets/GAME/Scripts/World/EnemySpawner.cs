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


    private int enemiesPerSpawner = 3;

    [HideInInspector]
    public float timeToSpawn = 3f;
    private float timeSinceSpawn;

    private bool permisoSpawnearEnemy = false;

    public static EnemySpawner Instance;

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

        //llama a funcionalidad crear 3 enemigos de golpe
        public void InstantiateEnemyNormal()
        {
            int poolIndex = ObjectPooler.instance.SearchPool(EnemyPrefabSimple);
            if (poolIndex != -1)
            {
                for (int i = 0; i < spawnPoints.Count; i++)
                {
                    StartCoroutine(CrearEnemigoPorLugar(enemiesPerSpawner, poolIndex, i));
                    //SceneEnemiesController.Instance.AddEnemyToScene();
                    //GameController.Instance.AddEnemyAlive(this.gameObject.GetComponent<BasicEnemyAbilityCharacter>());
                }
            }
        }

    //funcionalidad crear enemigos
    //private void CrearEnemigoPorLugar(int numeroDeEnemigos, int poolIndex, int spawnPointNumber)
    //{
    //    //crea "numeroEnemigos" enemies
    //    for(int i = 0; i < numeroDeEnemigos; i++)
    //    {
    //        GameObject enemyGO = ObjectPooler.instance.GetPooledObject(poolIndex);
    //        enemyGO.transform.position = spawnPoints[spawnPointNumber].position;
    //        enemyGO.transform.rotation = spawnPoints[spawnPointNumber].rotation;
    //        enemyGO.SetActive(true);
    //        GameController.Instance.AddEnemyAlive(enemyGO.GetComponent<BasicEnemyAbilityCharacter>());
    //        permisoSpawnearEnemy = false;
    //    }
        
    //}

    private IEnumerator CrearEnemigoPorLugar(int numeroDeEnemigos, int poolIndex, int spawnPointNumber)
    {
        //todo el rato
        while (true)
        {
            //Funcionalidad
            if(permisoSpawnearEnemy)
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