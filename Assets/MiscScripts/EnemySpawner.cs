﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    //declare variables
    public GameObject[] enemies;
    public float[] enemySpawnWeight;
    public float spawnInterval = 2;
    private float timer = 0;
    private float halfHeight;
    private float halfWidth;
    private bool needSpawn = false;
    public GameObject player;
    public GameObject enemyContainer;
    public GameObject ItemContainer;
    public bool spawning = false;
    public int enemiesSpawned = 0;
    void Start()
    {
        //get camera dimensions for enemy spawning
        Camera camera = Camera.main;
        halfHeight = camera.orthographicSize;
        halfWidth = camera.aspect * halfHeight;
    }

    // Update is called once per frame
    void Update()
    {
        //manage the spawning of enemies and the spawner timer
        if(spawning)
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
        //get the spawn position and offset it by the screens length based on spawn direction
        Vector3 spawnPosition = new Vector3(player.transform.position.x, player.transform.position.y, 0);
        int offscreenDirection = Random.Range(0, 4);
        if (offscreenDirection == 0)
        {
            spawnPosition.x += -halfWidth - 1;
            spawnPosition.y += Random.Range(-halfHeight, halfHeight);
        }
        else if (offscreenDirection == 1)
        {
            spawnPosition.x += halfWidth + 1;
            spawnPosition.y += Random.Range(-halfHeight, halfHeight);
        }
        else if (offscreenDirection == 2)
        {
            spawnPosition.y += -halfHeight - 1;
            spawnPosition.x += Random.Range(-halfWidth, halfWidth);
        }
        else if (offscreenDirection == 3)
        {
            spawnPosition.y += halfHeight + 1;
            spawnPosition.x += Random.Range(-halfWidth, halfWidth);
        }
        //pick which enemy will be spawned based off if enemy spawn weight and a random number from 0 - 99
        int rangeNum = Random.Range(0, 100);
        GameObject enemy = enemies[0];
        float randomVal = Random.Range(0,100);

        for (int i = 0; i < enemies.Length; i++)
        {
            if (randomVal <= ((enemySpawnWeight[i] * 100)-1))
            {
                enemy = enemies[i];
                break;
            }
        }
        //create the enemy object and get the agent component
        GameObject e = (GameObject)Instantiate(enemy, spawnPosition, Quaternion.identity);
        NavMeshAgent agent = e.GetComponent<NavMeshAgent>();
        if (!agent.isOnNavMesh)
        {
            // spawn another enemy if the current enemy is not on the spawn mesh
            needSpawn = true;
            Destroy(e);
        }
        else
        {
            //if enemy is on the mesh spawn the enemy and increment the amount of enemies spawned
            needSpawn = false;
            e.GetComponent<FollowPlayer>().target = player;
            e.transform.parent = enemyContainer.transform;
            e.GetComponent<EnemyHealth>().ItemContainer = ItemContainer;
            enemiesSpawned++;
        }

    }
}
