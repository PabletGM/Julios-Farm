using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItemHolder : MonoBehaviour
{
    [SerializeField]
    private BaseAbility PassiveAbility;

    private void OnTriggerEnter(Collider other)
    {
        PlayerAbilityCharacter character = other.GetComponent<PlayerAbilityCharacter>();
        if (character != null)
        {
            EfectoCogerPassive();
            character.AddPassiveAbility(PassiveAbility);
            //Destroy(gameObject);
        }
    }

    //efecto de la pasiva al cogerla
    private void EfectoCogerPassive()
    {
        //destruimos resto de pasivas para que no se puedan coger
        AbilitiesPerRound.Instance.PasivaCogidaParaLaRondaDestruirResto();
        //pasar de ronda en GameController
        GameController.Instance.AddRoundToGame();
        GameController.Instance.BreakTime(false);
        //aparecer enemigos funcionalidad spawnEnemy
        GameController.Instance.EfectoPasivaIniciarFuncionalidadEnemySpawnerSimpleEnemy();
        GameController.Instance.EfectoPasivaIniciarFuncionalidadEnemySpawnerBossEnemy();
    }
}
