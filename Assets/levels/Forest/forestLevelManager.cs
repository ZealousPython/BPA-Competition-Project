using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forestLevelManager : MonoBehaviour
{
    private GameManager game;
    private GameObject player;
    private Vector3 playerStartPos = new Vector3(5,-13,0);

    public GameObject warrior;
    public GameObject rouge;
    public GameObject mage;

    public GameObject rougeStartWeapon;
    public GameObject warriorStartWeapon;

    public CameraMovement cam;
    public EnemySpawner spawner;
    public OgreStuff ogre;


    void Start()
    {
        game = GameManager.instance;
        getPlayer();
    }
    void Update()
    {
        
    }
    void getPlayer() {
        if (game.playerClass == 0)
        {
            player = (GameObject)Instantiate(warrior, playerStartPos, Quaternion.identity);
            GameObject playerWeapon = (GameObject)Instantiate(warriorStartWeapon, playerStartPos, Quaternion.identity);
            playerWeapon.transform.parent = player.transform;
            game.playerWeapon = warriorStartWeapon;
        }
        else if (game.playerClass == 1)
        {
            player = (GameObject)Instantiate(rouge, playerStartPos, Quaternion.identity);
            game.playerWeapon = rougeStartWeapon;
        }
        else {
            
            player = (GameObject)Instantiate(mage, playerStartPos, Quaternion.identity);
        }
        cam.player = player.transform;
        spawner.player = player;
        ogre.target = player;

    }
}
