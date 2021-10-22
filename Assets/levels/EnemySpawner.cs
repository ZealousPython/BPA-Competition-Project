using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public float[] enemySpawnWeight;
    public float spawnInterval = 2;
    private float timer = 0;
    private float halfHeight;
    private float halfWidth;
    void Start()
    {
        Camera camera = Camera.main;
        halfHeight = camera.orthographicSize;
        halfWidth = camera.aspect * halfHeight;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            spawn();
            timer = spawnInterval;
        }
    }
    private void spawn() {
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        int offscreenDirection = Random.Range(0,4);
        if (offscreenDirection == 0)
        {
            spawnPosition.x = -halfWidth - 1;
            spawnPosition.y = Random.Range(-halfHeight, halfHeight);
        }
        else if (offscreenDirection == 1) {
            spawnPosition.x = halfWidth + 1;
            spawnPosition.y = Random.Range(-halfHeight, halfHeight);
        }
        else if (offscreenDirection == 2)
        {
            spawnPosition.y = -halfHeight - 1;
            spawnPosition.x = Random.Range(-halfWidth, halfWidth);
        }
        else if (offscreenDirection == 3)
        {
            spawnPosition.y = halfHeight + 1;
            spawnPosition.x = Random.Range(-halfWidth, halfWidth);
        }
        int rangeNum = Random.Range(0, 100);
        GameObject enemy = enemies[0];
        for (int i = 0; i < enemies.Length; i++) {
            float randomVal = rangeNum * enemySpawnWeight[i];
            if (randomVal <= (enemySpawnWeight[i] * 100)) {
                enemy = enemies[i];
                break;
            }
        }
        GameObject e = (GameObject)Instantiate(enemy, spawnPosition, Quaternion.identity);
        

    }
}
