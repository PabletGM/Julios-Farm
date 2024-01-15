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

    [SerializeField]
    private float timeToSpawn = 5f;
    private float timeSinceSpawn; 

    // Start is called before the first frame update
    void Start()
    {
        InstantiateEnemyNormal();
    }

    public void InstantiateEnemyNormal()
    {
        int poolIndex = ObjectPooler.instance.SearchPool(EnemyPrefabSimple);
        if (poolIndex != -1)
        {
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                GameObject enemyGO = ObjectPooler.instance.GetPooledObject(poolIndex);
                enemyGO.transform.position = spawnPoints[i].position;
                enemyGO.transform.rotation = spawnPoints[i].rotation;
                enemyGO.SetActive(true);
                GameController.Instance.AddEnemyAlive(enemyGO.GetComponent<BasicEnemyAbilityCharacter>());
                //SceneEnemiesController.Instance.AddEnemyToScene();
                //GameController.Instance.AddEnemyAlive(this.gameObject.GetComponent<BasicEnemyAbilityCharacter>());
            }
        }
    }


    //void Update()
    //{
    //  timeSinceSpawn += Time.deltaTime;
    //  if (timeSinceSpawn >= timeToSpawn)
    //   {
    //   GameObject enemyGO = ObjectPooler.instance.GetPooledObject(poolIndex);
    //   enemyGO.transform.position = this.transform.position;
    //   timeSinceSpawn = 0;
    //   }
    //}
}