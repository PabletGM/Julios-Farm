using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Lista de enemigos que est�n en juego
    public List<BasicEnemyAbilityCharacter> enemyInGameList;

    public static GameController Instance;

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
        DontDestroyOnLoad(gameObject);

        // Inicializar la lista en el Awake
        enemyInGameList = new List<BasicEnemyAbilityCharacter>();
    }

    // M�todo para a�adir un enemigo a la lista
    public void AddEnemyAlive(BasicEnemyAbilityCharacter enemy)
    {
        enemyInGameList.Add(enemy);
        //Debug.Log(enemyInGameList.Count);
    }

    // M�todo para quitar un enemigo de la lista
    public void RemoveEnemyAlive(BasicEnemyAbilityCharacter enemy)
    {
        enemyInGameList.Remove(enemy);
        //Debug.Log(enemyInGameList.Count);
    }

}
