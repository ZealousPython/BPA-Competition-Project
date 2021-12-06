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
    private float debugTimer = 5;


    void Start()
    {
        game = GameManager.instance;
        getPlayer();
    }
    void Update()
    {
        debugTimer -= Time.deltaTime;
        if (debugTimer < 0)
        {
            game.endLevel(1);
        }
    }
    void getPlayer() {
        print(game.playerClass);
        if (game.playerClass == 1)
        {
            if (game.weaponsOwned.Count > 0)
                game.weaponsOwned[0] = warriorStartWeapon;
            else {
                game.weaponsOwned.Add(warriorStartWeapon);
            }
            game.playerWeapon = game.weaponsOwned[0];
            player = (GameObject)Instantiate(warrior, playerStartPos, Quaternion.identity);
            
        }
        else if (game.playerClass == 2)
        {
            if (game.weaponsOwned.Count > 0)
                game.weaponsOwned[0] = rougeStartWeapon;
            else
            {
                game.weaponsOwned.Add(rougeStartWeapon);
            }
            game.playerWeapon = game.weaponsOwned[0];
            player = (GameObject)Instantiate(rouge, playerStartPos, Quaternion.identity);
            
        }
        else if(game.playerClass == 3){
            
            player = (GameObject)Instantiate(mage, playerStartPos, Quaternion.identity);
        }
        cam.player = player.transform;
        spawner.player = player;
        ogre.target = player;
        game.player = player;

    }
}
