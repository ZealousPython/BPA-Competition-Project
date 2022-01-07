using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//sets up and spawns player as well as handling the ending of the level
public class desertLevelManager : MonoBehaviour
{
    //declare variables
    private GameManager game;
    private GameObject player;
    private Vector3 playerStartPos = new Vector3(2.75f, -18, 0);

    public GameObject warrior;
    public GameObject rouge;
    public GameObject mage;

    public CameraMovement cam;
    public EnemySpawner spawner;
    public BossBandit bandit;
    public GameObject ItemContainer;
    private bool fightingBandit = false;
    // Start is called before the first frame update
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
    public void SetUpPlayer() {
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
    public void endLevel()
    {
        for (int i = 0; i < ItemContainer.transform.childCount; i++)
        {
            if (ItemContainer.transform.GetChild(i).name.Substring(0, 4) == "Coin")
            {
                int goldGained = Random.Range(3, 4);
                game.gold += goldGained;
            }
        }

        game.endLevel();
    }
}
