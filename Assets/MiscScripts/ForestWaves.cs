using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ForestWaves : MonoBehaviour
{
    //delcare variables
    private int wave = 1;
    public int endWave = 4;
    public int baseEnemyCount = 10;
    public float baseEnemySpeed = 2;
    public float difficultyScale = .75f;
    private EnemySpawner spawner;
    private int enemiesLeft = 0;
    private int inBetweenTime = 2;
    private float timer = 0;
    public Text waves;
    private int waveEnemyCount;
    private bool waveEnded = true;
    private bool bossWave = false;
    public GameObject boss;
    public GameObject bossBar;
    public Image waveProgressBar;
    private bool bossActive = false;
    private void Awake()
    {
        //initialize variables
        waveEnemyCount = (int)(baseEnemyCount);
        spawner = GetComponent<EnemySpawner>();
        spawner.spawnInterval = baseEnemySpeed;
    }
    void Update()
    {
        //gets the amount of enemies left and updates the wave progress bar
        enemiesLeft = spawner.enemyContainer.transform.childCount;
        waveProgressBar.fillAmount = (spawner.enemiesSpawned-enemiesLeft * 1.0f) / (waveEnemyCount*1.0f);
        //checks if their are no enemies left after a round has already started
        if (enemiesLeft <= 0 && spawner.enemiesSpawned >= waveEnemyCount && timer <= 0)
        {
            //if all enemies are gone stop spawning and start a timer until the next wave
            spawner.spawning = false;
            if (wave != endWave)
            {
                //if all waves are not completed increase wave difficulty and move onto the next wave
                spawner.enemiesSpawned = 0;
                wave++;
                waveEnemyCount = (int)(baseEnemyCount * wave * difficultyScale);
                enemiesLeft = waveEnemyCount;
                timer = inBetweenTime;
                spawner.spawnInterval = baseEnemySpeed / (wave * difficultyScale);
                waveEnded = true;
            }
            else {
                // if the end wave has been reached spawn the boss
                timer = inBetweenTime;
                waveEnded = true;
                bossWave = true;
            }
        }
        //set enemies spawning to fales if all enemies have been spawned for that round
        else if (spawner.enemiesSpawned >= waveEnemyCount && !waveEnded) {
            spawner.spawning = false;
        }
        //stick down the wave timer
        if (timer > 0 && waveEnded)
            timer -= Time.deltaTime;
        //start spawning again after the wave timer is over
        if (!spawner.spawning && waveEnded && timer <= 0 && wave <= endWave)
        {
            waveProgressBar.fillAmount = 0;
            spawner.spawning = true;
            waveEnded = false;
        }
        //show the boss and boss health bar and start boss fight
        if (timer <= 0 && wave == endWave &&bossWave&&!bossActive) {
            boss.SetActive(true);
            bossBar.SetActive(true);
            bossActive = true;
        }
        //display the current wave
        waves.text =  wave.ToString();
    }
}
