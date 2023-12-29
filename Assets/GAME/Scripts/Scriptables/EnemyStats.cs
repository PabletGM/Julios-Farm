using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "JuliosFarm/Stats/EnemyStats")]
public class EnemyStats : GenericStats
{
    [Header("Enemy Stats")]
    public float angularSpeed = 120f;
    public float stoppingDistance = 2f;
    public bool autoBraking = false;
}
