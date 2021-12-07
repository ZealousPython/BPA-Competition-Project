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
    public GameObject ItemContainer;
    public float debugTimer = 5;
    private bool fightingOgre = false;


    void Start()
    {
        game = GameManager.instance;
        getPlayer();
    }
    void Update()
    {
        if (ogre.isActiveAndEnabled) {
            fightingOgre = true;
        }
        if (fightingOgre && !ogre.isActiveAndEnabled)
            endLevel();
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
    public void endLevel() {
        for (int i = 0; i < ItemContainer.transform.childCount; i++)
        {
            if (ItemContainer.transform.GetChild(i).name.Substring(0, 4) == "Coin")
            {
                int goldGained = Random.Range(1,7);
                game.gold += goldGained;
            }
        }

        game.endLevel();
    }
}
