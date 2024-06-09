using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawn
{
    void spawn(EnemyData enemyData);
}
public class SpawnEnemy : MonoBehaviour, ISpawn
{
    public int enemyCount { get; set; }
    private int enemyMaxCount = 5;

    public Transform _transform;

    public EnemyData enemyData;

    private void Start()
    {
        _transform = this.transform;
        enemyCount = 0;
    }

    private void Update()
    {
        if (enemyCount <= enemyMaxCount)
        {
            spawn(enemyData);
            enemyCount++;
        }


    }
    public void spawn(EnemyData enemyData)
    {
        Instantiate(enemyData.spawnPrefab, _transform.transform);

    }

    public void CountSub()
    {
        enemyCount--;
    }
}
