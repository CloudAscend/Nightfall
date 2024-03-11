using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPortal : EnemyBase
{
    [SerializeField] private GameObject[] enemyPrefab; //Temp
    [SerializeField] private int spawnCount;
    [SerializeField] private float spawnTime;

    private Vector3 spawnVec;
    private float timeRate;

    protected override void Update()
    {
        if (timeRate >= spawnTime)
        {
            timeRate -= spawnTime;
            Spawn();
        }
        else
        {
            timeRate += Time.deltaTime;
        }
    }

    private void Spawn()
    {
        for (int i = 0; i < 360; i += 360 / spawnCount)
        {
            //Temp
            float degree = i * Mathf.Deg2Rad;
            spawnVec.x = Mathf.Cos(degree);
            spawnVec.y = Mathf.Sin(degree);
            Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], transform.position + spawnVec, Quaternion.identity);
        }
    }
}
