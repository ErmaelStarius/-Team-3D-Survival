using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class SpawnEnemy : MonoBehaviour
{

    public EnemyData enemyData;

    // public Action OnSpawn;
    // public static SpawnEnemy _instance;
    private void Awake()
    {
        //_instance = this;
    }

    private void Start()
    {
        enemyData.transform = this.transform;

        for (int i = 0; i < 5; i++)
        {
            StartCoroutine(Spawn(enemyData));
        }
    }

    public void SpawningPool(EnemyData enemyData)
    {
        StartCoroutine(Spawn(enemyData));
    }

    IEnumerator Spawn(EnemyData enemyData)
    {
        Instantiate(enemyData.spawnPrefab, enemyData.transform.position, Quaternion.identity, transform);

        yield return new WaitForSeconds(0.1f);
    }

}
