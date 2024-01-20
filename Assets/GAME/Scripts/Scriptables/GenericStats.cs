using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GenericStats", menuName = "JuliosFarm/Stats/GenericStats")]
public class GenericStats : ScriptableObject
{
    public FloatVariable health;
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
        maxHealth = health.runTimeValue;
    }

    public virtual void ResetStats()
    {
        speed.runTimeValue = speed.value;
        baseDamage.runTimeValue = baseDamage.value;
    }
}
