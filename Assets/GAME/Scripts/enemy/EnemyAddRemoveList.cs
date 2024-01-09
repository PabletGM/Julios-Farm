using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAddRemoveList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //se añade a la lista de enemigos totales in Game
        GameController.Instance.AddEnemyAlive(this.gameObject.GetComponent<BasicEnemyAbilityCharacter>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
