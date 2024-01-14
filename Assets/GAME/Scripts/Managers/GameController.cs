using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //Lista de enemigos que están en juego
    public List<BasicEnemyAbilityCharacter> enemyInGameList;

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
        AddRoundToGame();
    }


    #region MurosEachRound
    //se le añade ronda al juego
    public void AddRoundToGame()
    {
        actualRound++;
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
        //desactivaos resto
        QuitarRestoDeMurosExceptoElDeTuRonda(0);
    }

    private void BehaviourRound2()
    {
        //activamos muro1
        placesToPutMurosBloqueoCamino[1].SetActive(true);
        //desactivaos resto
        QuitarRestoDeMurosExceptoElDeTuRonda(1);
    }

    private void BehaviourRound3()
    {
        //activamos muro3
        placesToPutMurosBloqueoCamino[2].SetActive(true);
        //desactivaos resto
        QuitarRestoDeMurosExceptoElDeTuRonda(2);
    }

    private void BehaviourRound4()
    {
        //activamos muro4
        placesToPutMurosBloqueoCamino[3].SetActive(true);
        //desactivaos resto
        QuitarRestoDeMurosExceptoElDeTuRonda(3);
    }

    private void BehaviourRound5()
    {
        //activamos muro5
        placesToPutMurosBloqueoCamino[4].SetActive(true);
        //desactivaos resto
        QuitarRestoDeMurosExceptoElDeTuRonda(4);
    }

    //desactiva todos los muros excepto el del parametro que pasas
    private void QuitarRestoDeMurosExceptoElDeTuRonda(int muroCorrecto)
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
        //Debug.Log(enemyInGameList.Count);
    }

    // Método para quitar un enemigo de la lista
    public void RemoveEnemyAlive(BasicEnemyAbilityCharacter enemy)
    {
        enemyInGameList.Remove(enemy);
        //Debug.Log(enemyInGameList.Count);
    }

    public void RedirectMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

}
