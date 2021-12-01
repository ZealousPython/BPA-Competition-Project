using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desertLevelManager : MonoBehaviour
{
    private GameManager game;
    private GameObject player;
    private Vector3 playerStartPos = new Vector3(2.75f, -18, 0);

    public CameraMovement cam;
    public EnemySpawner spawner;
    public BossBandit bandit;
    // Start is called before the first frame update
    void Start()
    {
        game = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        player = game.player;
        player = (GameObject)Instantiate(player, playerStartPos, Quaternion.identity);
        if (game.playerClass == 1)
        {
            GameObject playerWeapon = (GameObject)Instantiate(game.playerWeapon, playerStartPos, Quaternion.identity);
            playerWeapon.transform.parent = player.transform;
        }

        cam.player = player.transform;
        spawner.player = player;
        bandit.target = player;
        game.player = player;
    }
}
