using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;

public class GameController : MonoBehaviour
{
    //Lista de enemigos que están en juego
    public List<BasicEnemyAbilityCharacter> enemyInGameList;

    [Header("Enemies per round and road")]
    [Header("Round 1")]
    public int enemiesRoundOneSmallRoad1;
    public int enemiesRoundOneSmallRoad2;
    public int enemiesRoundOneSmallRoad3;
    public int enemiesRoundOneBigRoad1;
    public int enemiesRoundOneBigRoad2;
    public int enemiesRoundOneBigRoad3;
    [Header("Round 2")]
    public int enemiesRoundTwoSmallRoad1;
    public int enemiesRoundTwoSmallRoad2;
    public int enemiesRoundTwoSmallRoad3;
    public int enemiesRoundTwoBigRoad1;
    public int enemiesRoundTwoBigRoad2;
    public int enemiesRoundTwoBigRoad3;
    [Header("Round 3")]
    public int enemiesRoundThreeSmallRoad1;
    public int enemiesRoundThreeSmallRoad2;
    public int enemiesRoundThreeSmallRoad3;
    public int enemiesRoundThreeBigRoad1;
    public int enemiesRoundThreeBigRoad2;
    public int enemiesRoundThreeBigRoad3;
    [Header("Round 4")]
    public int enemiesRoundFourSmallRoad1;
    public int enemiesRoundFourSmallRoad2;
    public int enemiesRoundFourSmallRoad3;
    public int enemiesRoundFourBigRoad1;
    public int enemiesRoundFourBigRoad2;
    public int enemiesRoundFourBigRoad3;
    [Header("Round 5")]
    public int enemiesRoundFiveSmallRoad1;
    public int enemiesRoundFiveSmallRoad2;
    public int enemiesRoundFiveSmallRoad3;
    public int enemiesRoundFiveBigRoad1;
    public int enemiesRoundFiveBigRoad2;
    public int enemiesRoundFiveBigRoad3;

    [Header("Time to spawn enemies per round")]
    [Header("Round 1")]
    public float enemiesRoundOneTimeToSpawnSmall;
    public float enemiesRoundOneTimeToSpawnBig;
    [Header("Round 2")]
    public float enemiesRoundTwoTimeToSpawnSmall;
    public float enemiesRoundTwoTimeToSpawnBig;
    [Header("Round 3")]
    public float enemiesRoundThreeTimeToSpawnSmall;
    public float enemiesRoundThreeTimeToSpawnBig;
    [Header("Round 4")]
    public float enemiesRoundFourTimeToSpawnSmall;
    public float enemiesRoundFourTimeToSpawnBig;
    [Header("Round 5")]
    public float enemiesRoundFiveTimeToSpawnSmall;
    public float enemiesRoundFiveTimeToSpawnBig;

    public static GameController Instance;

    private int actualRound = 0;

    private string mainMenu = "MainMenu";

    private bool breakTime = false;

    //ronda actual en la que nos encontramos
    public int ActualRound
    {
        get { return actualRound; }
        set { actualRound = value; }
    }

    [SerializeField] private GameObject[] roundWalls;

    [SerializeField] private EnemySpawner[] SpawnsEnemyNormal;
    [SerializeField] private EnemySpawnerBoss[] SpawnsEnemyBoss;

    //numero de rondas totales
    private int maxRounds = 5;

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
        //DontDestroyOnLoad(gameObject);

        // Inicializar la lista en el Awake
        enemyInGameList = new List<BasicEnemyAbilityCharacter>();
        AddRoundToGame();
        AddRoundToGame();
        AddRoundToGame();
    }
    

    #region MurosEachRound
    //se le añade ronda al juego
    public void AddRoundToGame()
    {
        actualRound++;
        //modificar en UIManager
        UIManager.Instance.UpdateRoundNumber(actualRound);
        //enseñamos el Enemies Left Text en la UI al empezar la ronda
        UIManager.Instance.ShowEnemiesLeft();
        //cuando se le añade hacemos el comportamiento en cada ronda
        BehaviourOnEachRound();
    }

    //comportamiento de cada ronda
    private void BehaviourOnEachRound()
    {
        if (actualRound <= maxRounds)
        {
            switch (actualRound)
            {
                case 1:
                {
                    //do this
                    BehaviourRound1();
                    break;
                }
                case 2:
                {
                    //do this
                    BehaviourRound2();
                    break;
                }
                case 3:
                {
                    //do this
                    BehaviourRound3();
                    break;
                }
                case 4:
                {
                    //do this
                    BehaviourRound4();
                    break;
                }
                case 5:
                {
                    //do this
                    BehaviourRound5();
                    break;
                }

                default:
                    break;
            }
        }
    }

    private void BehaviourRound1()
    {
        //activamos muro1
        roundWalls[0].SetActive(true);
        //activamos muro3
        roundWalls[2].SetActive(true);
        //actualizamos el UI
        int enemiesTotalRound1 = enemiesRoundOneSmallRoad1 + enemiesRoundOneSmallRoad2 + enemiesRoundOneSmallRoad3 + enemiesRoundOneBigRoad1 + enemiesRoundOneBigRoad1 + enemiesRoundOneBigRoad1;
        UpdateTotalEnemiesInRound(enemiesTotalRound1);
        //desactivaos resto
        QuitWallsOfTheRound(0,2);
        //elegir tiempo de spawneo de enemigos
        ChangeSpawnTimeEachRoundSimpleEnemies(enemiesRoundOneSmallRoad1, enemiesRoundOneSmallRoad2, enemiesRoundOneSmallRoad3, enemiesRoundOneTimeToSpawnSmall);
        ChangeSpawnTimeEachRoundBoss(enemiesRoundOneBigRoad1, enemiesRoundOneBigRoad2, enemiesRoundOneBigRoad3, enemiesRoundOneTimeToSpawnBig);
    }

    

    private void BehaviourRound2()
    {
        //activamos muro2
        roundWalls[1].SetActive(true);
        //actualizamos el UI
        int enemiesTotalRound2 = enemiesRoundTwoSmallRoad1 + enemiesRoundTwoSmallRoad2 + enemiesRoundTwoSmallRoad3 + enemiesRoundTwoBigRoad1 + enemiesRoundTwoBigRoad1 + enemiesRoundTwoBigRoad1;
        UpdateTotalEnemiesInRound(enemiesTotalRound2);
        //desactivaos resto, si es -1 es que no tiene muroCorrecto2
        QuitWallsOfTheRound(1, -1);
        //elegir tiempo de spawneo de enemigos
        ChangeSpawnTimeEachRoundSimpleEnemies(enemiesRoundTwoSmallRoad1, enemiesRoundTwoSmallRoad2, enemiesRoundTwoSmallRoad3, enemiesRoundTwoTimeToSpawnSmall);
        ChangeSpawnTimeEachRoundBoss(enemiesRoundTwoBigRoad1, enemiesRoundTwoBigRoad2, enemiesRoundTwoBigRoad3, enemiesRoundTwoTimeToSpawnBig);
    }

    private void BehaviourRound3()
    {
        //activamos muro1
        //placesToPutMurosBloqueoCamino[0].SetActive(true);
        //activamos muro2
        roundWalls[1].SetActive(true);
        //actualizamos el UI
        int enemiesTotalRound3 = enemiesRoundThreeSmallRoad1 + enemiesRoundThreeSmallRoad2 + enemiesRoundThreeSmallRoad3 + enemiesRoundThreeBigRoad1 + enemiesRoundThreeBigRoad1 + enemiesRoundThreeBigRoad1;
        UpdateTotalEnemiesInRound(enemiesTotalRound3);
        //desactivaos resto
        QuitWallsOfTheRound(-1, 1);
        //elegir tiempo de spawneo de enemigos
        ChangeSpawnTimeEachRoundSimpleEnemies(enemiesRoundThreeSmallRoad1, enemiesRoundThreeSmallRoad2, enemiesRoundThreeSmallRoad3, enemiesRoundThreeTimeToSpawnSmall);
        ChangeSpawnTimeEachRoundBoss(enemiesRoundThreeBigRoad1, enemiesRoundThreeBigRoad2, enemiesRoundThreeBigRoad3, enemiesRoundThreeTimeToSpawnBig);
    }

    private void BehaviourRound4()
    {
        //activamos muro3
        roundWalls[2].SetActive(true);
        //activamos muro2
       //placesToPutMurosBloqueoCamino[1].SetActive(true);
        int enemiesTotalRound4 = enemiesRoundFourSmallRoad1 + enemiesRoundFourSmallRoad2 + enemiesRoundFourSmallRoad3 + enemiesRoundFourBigRoad1 + enemiesRoundFourBigRoad1 + enemiesRoundFourBigRoad1;
        UpdateTotalEnemiesInRound(enemiesTotalRound4);
        //desactivaos resto
        QuitWallsOfTheRound(-1,2);
        //elegir tiempo de spawneo de enemigos
        ChangeSpawnTimeEachRoundSimpleEnemies(enemiesRoundFourSmallRoad1, enemiesRoundFourSmallRoad2, enemiesRoundFourSmallRoad3, enemiesRoundFourTimeToSpawnSmall);
        ChangeSpawnTimeEachRoundBoss(enemiesRoundFourBigRoad1, enemiesRoundFourBigRoad2, enemiesRoundFourBigRoad3, enemiesRoundFourTimeToSpawnBig);
    }

    private void BehaviourRound5()
    {

        int enemiesTotalRound5 = enemiesRoundFiveSmallRoad1 + enemiesRoundFiveSmallRoad2 + enemiesRoundFiveSmallRoad3 + enemiesRoundFiveBigRoad1 + enemiesRoundFiveBigRoad1 + enemiesRoundFiveBigRoad1;
        UpdateTotalEnemiesInRound(enemiesTotalRound5);
        //desactivaos resto
        QuitWallsOfTheRound(-1,-1);
        //elegir tiempo de spawneo de enemigos
        ChangeSpawnTimeEachRoundSimpleEnemies(enemiesRoundFiveSmallRoad1, enemiesRoundFiveSmallRoad2, enemiesRoundFiveSmallRoad3, enemiesRoundFiveTimeToSpawnSmall);
        ChangeSpawnTimeEachRoundBoss(enemiesRoundFiveBigRoad1, enemiesRoundFiveBigRoad2, enemiesRoundFiveBigRoad3, enemiesRoundFiveTimeToSpawnBig);
    }

    //desactiva todos los muros excepto el del parametro que pasas
    private void QuitWallsOfTheRound(int correctWall1, int correctWall2)
    {
        for(int i = 0; i < roundWalls.Length; i++) 
        { 
            //si es muro correcto
            if(i == correctWall1 || i == correctWall2)
            {
                roundWalls[i].SetActive(true);
            }
            //sino se desactiva
            else
            {
                roundWalls[i].SetActive(false);
            }
        }
    }

    #endregion

    // Método para añadir un enemigo a la lista
    public void AddEnemyAlive(BasicEnemyAbilityCharacter enemy)
    {
        enemyInGameList.Add(enemy);
        //actualizamos en pantalla
        //UIManager.Instance.UpdateEnemiesLeft(TotalEnemiesAlive());
    }

    public void UpdateTotalEnemiesInRound(int totalEnemiesInRound)
    {
        UIManager.Instance.UpdateTotalEnemies(totalEnemiesInRound);
    }

    // Método para quitar un enemigo de la lista
    public void RemoveEnemyAlive(BasicEnemyAbilityCharacter enemy)
    {
        enemyInGameList.Remove(enemy);
        //actualizamos en pantalla
        UIManager.Instance.RemoveOneEnemy();
    }

    public int TotalEnemiesAlive()
    {
        return enemyInGameList.Count;
    }

    public void BreakTime(bool BreakTime)
    {
        breakTime = BreakTime;
    }

    public void IsGameWon()
    {
        if (actualRound == maxRounds && breakTime)
        {
            UIManager.Instance.ShowYouWinCanvas();
        }
    }

    public void EndRound()
    {
        AbilitiesPerRound.Instance.spawnRoundPassives();
        breakTime = true;
        IsGameWon();
        UIManager.Instance.HideEnemiesLeft();
    }
    public void RedirectMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void initializeEnemySpawner()
    {
        SpawnsEnemyNormal[0].InstantiateEnemyNormal();
        SpawnsEnemyNormal[1].InstantiateEnemyNormal();
        SpawnsEnemyNormal[2].InstantiateEnemyNormal();
    }

    public void EfectoPasivaIniciarFuncionalidadEnemySpawnerBossEnemy()
    {
        SpawnsEnemyBoss[0].InstantiateEnemyNormal();
        SpawnsEnemyBoss[1].InstantiateEnemyNormal();
        SpawnsEnemyBoss[2].InstantiateEnemyNormal();

    }

    //actualizar spawner
    private void ChangeSpawnTimeEachRoundSimpleEnemies(int numEnemiesRoundCamino1, int numEnemiesRoundCamino2, int numEnemiesRoundCamino3, float timeToSpawnSimple)
    {
        //cambiar valores spawnPoints enemigos normales
        SpawnsEnemyNormal[0].ChangeSpawnEachRound(numEnemiesRoundCamino1, timeToSpawnSimple);
        SpawnsEnemyNormal[1].ChangeSpawnEachRound(numEnemiesRoundCamino2, timeToSpawnSimple);
        SpawnsEnemyNormal[2].ChangeSpawnEachRound(numEnemiesRoundCamino3, timeToSpawnSimple);
    }

    private void ChangeSpawnTimeEachRoundBoss(int numEnemiesRoundCamino1, int numEnemiesRoundCamino2, int numEnemiesRoundCamino3, float timeToSpawnBoss)
    {
        //cambiar valores spawnPointsEnemigosBoss
        SpawnsEnemyBoss[0].ChangeSpawnEachRound(numEnemiesRoundCamino1, timeToSpawnBoss);
        SpawnsEnemyBoss[1].ChangeSpawnEachRound(numEnemiesRoundCamino2, timeToSpawnBoss);
        SpawnsEnemyBoss[2].ChangeSpawnEachRound(numEnemiesRoundCamino3, timeToSpawnBoss);
    }

}
