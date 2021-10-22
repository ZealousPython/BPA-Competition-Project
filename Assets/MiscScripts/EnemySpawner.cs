using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public float[] enemySpawnWeight;
    public float spawnInterval = 2;
    private float timer = 0;
    private float halfHeight;
    private float halfWidth;
    private bool needSpawn = false;
    public GameObject player;
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
        if (timer <= 0)
        {
            spawn();
            timer = spawnInterval;
        }
        if (needSpawn)
        {
            spawn();
        }
    }
    private void spawn()
    {
        Vector3 spawnPosition = new Vector3(player.transform.position.x, player.transform.position.y, 0);
        int offscreenDirection = Random.Range(0, 4);
        if (offscreenDirection == 0)
        {
            spawnPosition.x = -halfWidth - 1;
            spawnPosition.y = Random.Range(-halfHeight, halfHeight);
        }
        else if (offscreenDirection == 1)
        {
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
        bool enemyChosen = false;
        GameObject enemy = enemies[0];
        float randomVal = enemySpawnWeight[Random.Range(0, enemies.Length)] * 100;
        for (int i = 0; i < enemies.Length; i++)
        {

            if (randomVal <= (enemySpawnWeight[i] * 100))
            {
                print(randomVal);
                enemy = enemies[i];
                enemyChosen = true;
                break;
            }
            if (enemyChosen) break;
        }
        GameObject e = (GameObject)Instantiate(enemy, spawnPosition, Quaternion.identity);
        NavMeshAgent agent = e.GetComponent<NavMeshAgent>();
        if (!agent.isOnNavMesh)
        {
            needSpawn = true;
            Destroy(e);
        }
        else
        {
            needSpawn = false;
            e.GetComponent<FollowPlayer>().target = player;
        }

    }
}
