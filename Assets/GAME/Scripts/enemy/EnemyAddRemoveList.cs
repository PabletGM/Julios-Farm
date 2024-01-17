using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAddRemoveList : MonoBehaviour
{
  

    public void AddEnemyToList()
    {
        //se añade a la lista de enemigos totales in Game
        GameController.Instance.AddEnemyAlive(this.gameObject.GetComponent<BasicEnemyAbilityCharacter>());    /*< --EnemySpawner*/
    }

  
}
