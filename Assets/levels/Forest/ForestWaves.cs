using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestWaves : MonoBehaviour
{
    private int wave = 1;
    private int endWave = 4;
    private int baseEnemyCount = 10;
    private float baseEnemySpeed = 2;
    public float difficultyScale = 1.5f;
    private EnemySpawner spawner;
    private int enemiesLeft = 0;
    private int inBetweenTime = 5;
    private float timer = 0;
    private void Awake()
    {
        spawner = GetComponent<EnemySpawner>();
        spawner.spawnInterval = baseEnemySpeed;
    }
    void Update()
    {
        enemiesLeft = spawner.enemyContainer.transform.childCount;
        if (enemiesLeft <= 0 && spawner.enemiesSpawned >= baseEnemyCount*wave*difficultyScale) { 
               
        }
    }
}
