using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CharacterSelect : MonoBehaviour
{
    private GameManager game;
    
    void Start() {
        game = GameManager.instance;
    }
    void Update() {
    }
    public void ChooseWarrior() {
        game.playerClass = 0;
        game.playerMaxHealth = 8;
        game.playerHealth = 8;
        game.playerMaxMana = 0;
        game.playerMana = 0;
        game.ChangeScene(game.firstLevelScenePath);
    }
    public void ChooseRouge()
    {
        game.playerClass = 1;
        game.playerMaxHealth = 7;
        game.playerHealth = 7;
        game.playerMaxMana = 0;
        game.playerMana = 0;
        game.ChangeScene(game.firstLevelScenePath);
    }
    public void ChooseMage()
    {
        game.playerClass = 2;
        game.playerMaxHealth = 6;
        game.playerHealth = 6;
        game.playerMaxMana = 100;
        game.playerMana = 100;
        game.ChangeScene(game.firstLevelScenePath);
    }
}
