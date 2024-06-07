using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawn
{
    void Spawn(EnemyData enemyData);
}
public class SpawnEnemy : MonoBehaviour, ISpawn
{


    public EnemyData enemyData;

    public Action OnSpawn;
    public static SpawnEnemy _instance;
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Spawn(enemyData);
        }
    }

    private void Update()
    {
        if (OnSpawn != null)
        {
            Spawn(enemyData);
            OnSpawn = null;
        }
    }
    public void Spawn(EnemyData enemyData)
    {
        Instantiate(enemyData.spawnPrefab, this.transform);

    }
}
