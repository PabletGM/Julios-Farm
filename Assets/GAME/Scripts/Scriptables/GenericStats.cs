using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GenericStats", menuName = "JuliosFarm/Stats/GenericStats")]
public class GenericStats : ScriptableObject
{
    public float health;
    public float maxHealth;
    public FloatVariable shield;
    public FloatVariable maxShield;
    [Header("Movement parameters")]
    public FloatVariable speed;
    [Range(1f, 10f)]
    public float rotationSpeed;

    [Header("Attack Parameters")]
    public FloatVariable baseDamage;

    private void Awake()
    {
        maxHealth = health;
    }

    public virtual void ResetStats()
    {
        speed.runTimeValue = speed.value;
        baseDamage.runTimeValue = baseDamage.value;
    }
}
