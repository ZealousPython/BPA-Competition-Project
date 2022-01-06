using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//is used to set the player up after starting the level and change to the next scene when the boss is killed
public class CaveLevelManager : MonoBehaviour
{
    //delcare variables
    private GameManager game;
    private GameObject player;
    private Vector3 playerStartPos = new Vector3(-0.46f, -19.27f, 0);

    public GameObject warrior;
    public GameObject rouge;
    public GameObject mage;

    public CameraMovement cam;
    public EnemySpawner spawner;
    public SpiderBoss Spider;
    public GameObject ItemContainer;
    private bool fightingSpider = false;
    // Start is called before the first frame update
    void Start()
    {
        game = GameManager.instance;
        SetUpPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        //checks if the spider is spawned and if is dead if is dead end the level
        if (Spider.isActiveAndEnabled)
        {
            fightingSpider = true;
        }
        if (fightingSpider && !Spider.isActiveAndEnabled)
            endLevel();
    }
    public void SetUpPlayer()
    {
        //sets up the player gameobject based on playerclass
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
        //assigns the objects the player variable so they can operate
        cam.player = player.transform;
        spawner.player = player;
        Spider.target = player;
        game.player = player;
    }
    public void endLevel()
    {
        //get all the coins in the item container and automatically get them for the player
        for (int i = 0; i < ItemContainer.transform.childCount; i++)
        {
            if (ItemContainer.transform.GetChild(i).name.Substring(0, 4) == "Coin")
            {
                int goldGained = Random.Range(2, 4);
                game.gold += goldGained;
            }
        }
        //call end level from the gamemanager in order to switch levels
        game.endLevel();
    }
}
