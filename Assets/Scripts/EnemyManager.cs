using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public enum PatrolType
{
    Patrol,
    Detect,
    Chase,
    Attack,
    Die
}
public class EnemyManager : Singleton<EnemyManager>
{
    public Transform[] patrolPoints;
    public List<GameObject> enemies;

    /// <summary>
    /// Kills a specific enemy
    /// </summary>
    /// <param name="_enemy"> The enemy we want to kill</param>
    public void KillEnemy(GameObject _enemy)
    {
        enemies.Remove(_enemy);
        ShowEnemyCount();
        Destroy(_enemy);
    }

    /// <summary>
    /// Get a random patrol point
    /// </summary>
    /// <returns>A random patrol point</returns>
    public Transform GetRandomSpawnPoint()
    {
        return patrolPoints[Random.Range(0, patrolPoints.Length)];
    }

    /// <summary>
    /// Updates enemy count in UI
    /// </summary>
    void ShowEnemyCount()
    {
        _UI.UpdateEnemyCount(enemies.Count);
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDie += KillEnemy;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDie -= KillEnemy;
    }
}
