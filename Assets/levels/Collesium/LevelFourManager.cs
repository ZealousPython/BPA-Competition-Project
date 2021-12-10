﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFourManager : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager game;
    private GameObject player;
    private Vector3 playerStartPos = new Vector3(0, -10, 0);

    public GameObject warrior;
    public GameObject rouge;
    public GameObject mage;

    public CameraMovement cam;
    public EnemySpawner spawner;
    public DragonScript bandit;
    public GameObject ItemContainer;
    private bool fightingBandit = false;
    // Start is called before the first frame update
    void Awake()
    {

    }
    void Start()
    {
        game = GameManager.instance;
        SetUpPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (bandit.isActiveAndEnabled)
        {
            fightingBandit = true;
        }
        if (fightingBandit && !bandit.isActiveAndEnabled)
            endLevel();

    }
    public void SetUpPlayer()
    {
        if (game.playerClass == 1)
        {
            player = (GameObject)Instantiate(warrior, playerStartPos, Quaternion.identity);
            GameObject playerWeapon = (GameObject)Instantiate(game.playerWeapon, playerStartPos, Quaternion.identity);
            playerWeapon.transform.parent = player.transform;
        }
        else if (game.playerClass == 2)
        {
            player = (GameObject)Instantiate(rouge, playerStartPos, Quaternion.identity);
        }
        else if (game.playerClass == 3)
        {
            player = (GameObject)Instantiate(mage, playerStartPos, Quaternion.identity);
        }

        cam.player = player.transform;
        spawner.player = player;
        game.player = player;
    }
    public void endLevel()
    {
        GameManager.instance.ChangeScene("Assets/UI/WinScreen.unity");
    }
}
