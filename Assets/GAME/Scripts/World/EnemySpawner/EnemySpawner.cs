using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnPoints;

    [SerializeField]
    private GameObject EnemyPrefabSimple;




    [HideInInspector]
    public float timeToSpawn = 3f;
    private float timeSinceSpawn;

    private bool permisoSpawnearEnemy = false;

    public static EnemySpawner Instance;

    private int numTotalEnemiesRound;

    private int numeroEnemiesCreadosSpawn1 = 0;

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

    public void ChangeSpawnEachRound(int numEnemiesRound,float timetoSpawn)
    {
        //cambiar numero de enemies por ronda
        numTotalEnemiesRound = numEnemiesRound;
        //cambiar timeToSpawn enemy
        timeToSpawn = timetoSpawn;
        //reiniciamos numero enemiesCreados
        numeroEnemiesCreadosSpawn1 = 0;
    }

        //llama a funcionalidad crear 3 enemigos de golpe
        public void InstantiateEnemyNormal()
        {
            int poolIndex = ObjectPooler.instance.SearchPool(EnemyPrefabSimple);

            if (poolIndex != -1)
            {
                //Iniciar Spawners

                    //para cada spawner hará el numeroEnemigosCreados = numTotalEnemiesRound
                    StartCoroutine(CrearEnemigoPorLugar(numTotalEnemiesRound, poolIndex, 0, numeroEnemiesCreadosSpawn1));
            }
        }

   

    private IEnumerator CrearEnemigoPorLugar(int numeroDeEnemigosTotal, int poolIndex, int spawnPointNumber, int numeroEnemiesCreadosSpawnX)
    {
        //todo el rato
        while (true)
        {
            //Funcionalidad y si no se ha pasado con el numero de enemies creados
            if(permisoSpawnearEnemy && numeroEnemiesCreadosSpawnX < numeroDeEnemigosTotal)
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
                numeroEnemiesCreadosSpawnX++;
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