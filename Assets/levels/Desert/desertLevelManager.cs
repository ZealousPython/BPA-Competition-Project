using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desertLevelManager : MonoBehaviour
{
    private GameManager game;
    private GameObject player;
    private Vector3 playerStartPos = new Vector3(2.75f, -18, 0);

    public GameObject warrior;
    public GameObject rouge;
    public GameObject mage;

    public CameraMovement cam;
    public EnemySpawner spawner;
    public BossBandit bandit;
    // Start is called before the first frame update
    void Awake() {
        
    }
    void Start()
    {
        game = GameManager.instance;
        SetUpPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetUpPlayer() {
        print("Set");
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
        bandit.target = player;
        game.player = player;
    }
}
