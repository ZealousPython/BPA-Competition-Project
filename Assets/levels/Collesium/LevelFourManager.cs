using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//manages spawning the player and ending the level for the fourth level
public class LevelFourManager : MonoBehaviour
{
    //declare variables
    private GameManager game;
    private GameObject player;
    private Vector3 playerStartPos = new Vector3(0, -10, 0);

    public GameObject warrior;
    public GameObject rouge;
    public GameObject mage;

    public CameraMovement cam;
    public EnemySpawner spawner;
    public DragonScript dragon;
    public GameObject ItemContainer;
    private bool fightingDragon = false;
    
    // Start is called before the first frame update
    void Start()
    {
        game = GameManager.instance;
        SetUpPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        //checks if you are fighting the dragon and if it is dead end the level
        if (dragon.isActiveAndEnabled)
        {
            fightingDragon = true;
        }
        if (fightingDragon && !dragon.isActiveAndEnabled)
            endLevel();

    }
    public void SetUpPlayer()
    {
        //spawns and sets up each player into the scene
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
        //assign the player variable to gameobjects that need it
        cam.player = player.transform;
        spawner.player = player;
        game.player = player;
    }
    public void endLevel()
    {
        //upon ending the level transistion to the win screen as this is the final level
        GameManager.instance.nextLevel();
        GameManager.instance.ChangeScene("Assets/UI/WinScreen.unity");
    }
}
