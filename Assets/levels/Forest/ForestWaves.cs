using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ForestWaves : MonoBehaviour
{
    private int wave = 1;
    private int endWave = 4;
    private int baseEnemyCount = 10;
    private float baseEnemySpeed = 2;
    public float difficultyScale = .75f;
    private EnemySpawner spawner;
    private int enemiesLeft = 0;
    private int inBetweenTime = 5;
    public float timer = 0;
    public Text waves;
    public int waveEnemyCount;
    public bool waveEnded = true;
    private bool bossWave = false;
    public GameObject boss;
    public GameObject bossBar;
    private void Awake()
    {
        waveEnemyCount = (int)(baseEnemyCount);
        spawner = GetComponent<EnemySpawner>();
        spawner.spawnInterval = baseEnemySpeed;
    }
    void Update()
    {
        enemiesLeft = spawner.enemyContainer.transform.childCount;
        if (enemiesLeft <= 0 && spawner.enemiesSpawned >= waveEnemyCount && timer <= 0)
        {
            spawner.spawning = false;
            if (wave != endWave)
            {
                spawner.enemiesSpawned = 0;
                wave++;
                waveEnemyCount = (int)(baseEnemyCount * wave * difficultyScale);
                enemiesLeft = waveEnemyCount;
                timer = inBetweenTime;
                spawner.spawnInterval = baseEnemySpeed / (wave * difficultyScale);
                waveEnded = true;
            }
            else {
                timer = inBetweenTime;
                waveEnded = true;
                bossWave = true;
            }
        }
        else if (spawner.enemiesSpawned >= waveEnemyCount && !waveEnded) {
            spawner.spawning = false;
        }
        if (timer > 0 && waveEnded)
            timer -= Time.deltaTime;
        if (!spawner.spawning && waveEnded && timer <= 0 && wave <= endWave)
        {

            spawner.spawning = true;
            waveEnded = false;
        }
        if (timer <= 0 && wave == endWave &&bossWave) {
            wave++;
            boss.SetActive(true);
            bossBar.SetActive(true);
        }
        waves.text = "Wave: " + wave.ToString();
    }
}
