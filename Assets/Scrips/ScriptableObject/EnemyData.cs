using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
    Monster,
    Human,
    Beast
}

[CreateAssetMenu(fileName = "Enemy", menuName = "New Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public EnemyType type;
    public GameObject spawnPrefab;

    public Transform transform;
}