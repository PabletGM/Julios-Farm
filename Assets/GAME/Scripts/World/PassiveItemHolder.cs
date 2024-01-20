using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItemHolder : MonoBehaviour
{
    [SerializeField]
    private BaseAbility PassiveAbility;

    // VFX Passive Picking Particles
    public GameObject pickParticles;
   

    private void OnTriggerEnter(Collider other)
    {
        PlayerAbilityCharacter character = other.GetComponent<PlayerAbilityCharacter>();
        if (character != null)
        {
            gettingPassive();
            character.AddPassiveAbility(PassiveAbility);

        }
    }

    //efecto de la pasiva al cogerla
    private void gettingPassive()
    {
        // VFX Passive Picking 
        int poolIndex = ObjectPooler.instance.SearchPool(pickParticles);
        if (poolIndex != -1)
        {
            GameObject particles = ObjectPooler.instance.GetPooledObject(poolIndex);
            particles.transform.position = this.transform.position;
            particles.SetActive(true);
        }
        //destruimos resto de pasivas para que no se puedan coger
        AbilitiesPerRound.Instance.destroyOtherPassives();
        //pasar de ronda en GameController
        GameController.Instance.AddRoundToGame();
        GameController.Instance.BreakTime(false);
        //aparecer enemigos funcionalidad spawnEnemy
        GameController.Instance.initializeEnemySpawner();
        GameController.Instance.EfectoPasivaIniciarFuncionalidadEnemySpawnerBossEnemy();
        AudioManagerPlayer.instance.TakePassive();
    }
}



