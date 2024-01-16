using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //Lista de enemigos que están en juego
    public List<BasicEnemyAbilityCharacter> enemyInGameList;

    [Header("Enemies per round")]
    public int enemiesRoundOne;
    public int enemiesRoundTwo;
    public int enemiesRoundThree;
    public int enemiesRoundFour;
    public int enemiesRoundFive;

    [Header("Time to spawn enemies per round")]
    public float enemiesRoundOneTimeToSpawn;
    public float enemiesRoundTwoTimeToSpawn;
    public float enemiesRoundThreeTimeToSpawn;
    public float enemiesRoundFourTimeToSpawn;
    public float enemiesRoundFiveTimeToSpawn;

    public static GameController Instance;

    private int actualRound = 0;

    private string mainMenu = "MainMenu"; 

    //ronda actual en la que nos encontramos
    public int ActualRound
    {
        get { return actualRound; }
        set { actualRound = value; }
    }

    [SerializeField] private GameObject[] placesToPutMurosBloqueoCamino;

    [SerializeField] private EnemySpawner[] Spawns;

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

        //ponemos por defecto que estamos en Ronda1;
        //AddRoundToGame();
    }


    #region MurosEachRound
    //se le añade ronda al juego
    public void AddRoundToGame()
    {
        actualRound++;
        //modificar en UIManager
        UIManager.Instance.UpdateRoundNumber(actualRound);
        //cuando se le añade hacemos el comportamiento en cada ronda
        BehaviourOnEachRound();
    }

    //comportamiento de cada ronda
    private void BehaviourOnEachRound()
    {
        if (actualRound < maxRounds)
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
        placesToPutMurosBloqueoCamino[0].SetActive(true);
        //actualizamos el UI
        UpdateTotalEnemiesInRound(enemiesRoundOne);
        //desactivaos resto
        QuitWallsOfTheRound(0);
        //elegir tiempo de spawneo de enemigos
        ChangeSpawnTimeEachRound(enemiesRoundOne, enemiesRoundOneTimeToSpawn);
    }

    //actualizar spawner
    private void ChangeSpawnTimeEachRound(int numEnemiesRound, float timeToSpawn)
    {
        for(int i = 0; i < Spawns.Length; i++)
        {
            Spawns[i].ChangeSpawnEachRound(numEnemiesRound, timeToSpawn);
        }
    }

    private void BehaviourRound2()
    {
        //activamos muro1
        placesToPutMurosBloqueoCamino[1].SetActive(true);
        //actualizamos el UI
        UpdateTotalEnemiesInRound(enemiesRoundTwo);
        //desactivaos resto
        QuitWallsOfTheRound(1);
        //elegir tiempo de spawneo de enemigos
        ChangeSpawnTimeEachRound(enemiesRoundTwo, enemiesRoundTwoTimeToSpawn);
    }

    private void BehaviourRound3()
    {
        //activamos muro3
        placesToPutMurosBloqueoCamino[2].SetActive(true);
        //actualizamos el UI
        UpdateTotalEnemiesInRound(enemiesRoundThree);
        //desactivaos resto
        QuitWallsOfTheRound(2);
        //elegir tiempo de spawneo de enemigos
        ChangeSpawnTimeEachRound(enemiesRoundThree, enemiesRoundThreeTimeToSpawn);
    }

    private void BehaviourRound4()
    {
        //activamos muro4
        placesToPutMurosBloqueoCamino[3].SetActive(true);
        //actualizamos el UI
        UpdateTotalEnemiesInRound(enemiesRoundFour);
        //desactivaos resto
        QuitWallsOfTheRound(3);
        //elegir tiempo de spawneo de enemigos
        ChangeSpawnTimeEachRound(enemiesRoundFour, enemiesRoundFourTimeToSpawn);
    }

    private void BehaviourRound5()
    {
        //activamos muro5
        placesToPutMurosBloqueoCamino[4].SetActive(true);
        //actualizamos el UI
        UpdateTotalEnemiesInRound(enemiesRoundFive);
        //desactivaos resto
        QuitWallsOfTheRound(4);
        //elegir tiempo de spawneo de enemigos
        ChangeSpawnTimeEachRound(enemiesRoundFour, enemiesRoundFourTimeToSpawn);
    }

    //desactiva todos los muros excepto el del parametro que pasas
    private void QuitWallsOfTheRound(int muroCorrecto)
    {
        for(int i = 0; i < placesToPutMurosBloqueoCamino.Length; i++) 
        { 
            //si es muro correcto
            if(i == muroCorrecto)
            {
                placesToPutMurosBloqueoCamino[i].SetActive(true);
            }
            //sino se desactiva
            else
            {
                placesToPutMurosBloqueoCamino[i].SetActive(false);
            }
        }
    }

    #endregion

    // Método para añadir un enemigo a la lista
    public void AddEnemyAlive(BasicEnemyAbilityCharacter enemy)
    {
        enemyInGameList.Add(enemy);
        //actualizamos en pantalla
        //Debug.Log(enemyInGameList.Count);
    }

    public void UpdateTotalEnemiesInRound(int totalEnemiesInRound)
    {
        UIManager.Instance.UpdateEnemiesLeft(totalEnemiesInRound);
    }

    // Método para quitar un enemigo de la lista
    public void RemoveEnemyAlive(BasicEnemyAbilityCharacter enemy)
    {
        enemyInGameList.Remove(enemy);
        //actualizamos en pantalla
        UIManager.Instance.UpdateEnemiesLeft(TotalEnemiesAlive());
        //Debug.Log(enemyInGameList.Count);
    }

    public int TotalEnemiesAlive()
    {
        return enemyInGameList.Count;
    }

    public void RedirectMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void EfectoPasivaIniciarFuncionalidadEnemySpawner()
    {
        for (int i = 0; i < Spawns.Length; i++)
        {
            Spawns[i].InstantiateEnemyNormal();
        }
    }

}
