using CreatorKitCodeInternal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private PlayerAbilityCharacter playerAbilityCharacter;

    //Stats management
    private float initHealth;

    private float currentHealth;
    public float CurrentHealth
    {
        set
        {
            currentHealth = value;
        }
        get
        {
            return currentHealth;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerAbilityCharacter = GetComponent<PlayerAbilityCharacter>();
        initHealth = playerAbilityCharacter.CharacterStats.health.runTimeValue;
        currentHealth = initHealth;
    }

    public GenericStats GetPlayerStats()
    {
        return playerAbilityCharacter.CharacterStats;
    }

    public void ChangeRakes()
    {
        playerAbilityCharacter.normalRake.SetActive(false);
        playerAbilityCharacter.damageRake.SetActive(true);
    }
}
