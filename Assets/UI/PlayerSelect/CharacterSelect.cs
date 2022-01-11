using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
//initilize variables for each player class depending on what is selected
public class CharacterSelect : MonoBehaviour
{
    private GameManager game;
    
    void Start() {
        Time.timeScale = 1;
        game = GameManager.instance;
    }
    void Update() {

    }
    public void ChooseWarrior() {
        //initilize nessasry variables for warrior
        game.gold = 0;
        game.potions = 1;
        game.playerClass = 1;
        game.playerMaxHealth = 8;
        game.playerHealth = 8;
        game.playerMaxMana = 100;
        game.playerMana = 100;
        game.ChangeScene(game.firstLevelScenePath);
        game.level = 1;
        game.weaponLevels = new List<int>();
        game.weaponLevels.Add(1);
        game.weaponLevels.Add(1);
        game.weaponLevels.Add(1);
        game.weaponsOwned = new List<GameObject>();
        game.mageSpells = new List<string>();
    }
    public void ChooseRouge()
    {
        //initilize nessasry variables for Rouge
        game.gold = 0;
        game.potions = 1;
        game.playerClass = 2;
        game.playerMaxHealth = 7;
        game.playerHealth = 7;
        game.playerMaxMana = 100;
        game.playerMana = 100;
        game.ChangeScene(game.firstLevelScenePath);
        game.level = 1;
        game.weaponLevels = new List<int>();
        game.weaponLevels.Add(1);
        game.weaponLevels.Add(1);
        game.weaponLevels.Add(1);
        game.weaponsOwned = new List<GameObject>();
        game.mageSpells = new List<string>();

    }
    public void ChooseMage()
    {
        //initilize nessasry variables for Mage
        game.gold = 0;
        game.potions = 1;
        game.playerClass = 3;
        game.playerMaxHealth = 6;
        game.playerHealth = 6;
        game.playerMaxMana = 100;
        game.playerMana = 100;
        game.ChangeScene(game.firstLevelScenePath);
        game.level = 1;
        game.weaponLevels = new List<int>();
        game.weaponLevels.Add(1);
        game.weaponLevels.Add(1);
        game.weaponLevels.Add(1);
        game.weaponsOwned = new List<GameObject>();
        game.mageSpells = new List<string>();
    }
}
