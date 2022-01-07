using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tutorial : MonoBehaviour
{
    public GameObject howToPlay;
    public GameObject controls;
    public GameObject backButton;
    public GameObject forwardButton;
    public GameObject player;

    public List<GameObject> weapons;

    void Awake() {
        GameManager.instance.weaponsOwned = new List<GameObject>();
        GameManager.instance.weaponsOwned.Add(GameManager.instance.AllWeapons[3]);
        GameManager.instance.weaponsOwned.Add(GameManager.instance.AllWeapons[5]);
        GameManager.instance.playerWeapon = GameManager.instance.weaponsOwned[0];
        GameManager.instance.playerMaxMana = 100;
        GameManager.instance.playerMaxHealth = 12;
        GameManager.instance.playerHealth = 4;
        GameManager.instance.potions = 3;
        GameManager.instance.level = 0;
        GameManager.instance.gold = 1000;
        GameManager.instance.weaponLevels = new List<int>();
        GameManager.instance.weaponLevels.Add(1);
        GameManager.instance.weaponLevels.Add(1);
        GameManager.instance.weaponLevels.Add(1);
        GameManager.instance.player = player;
    }

    public void goBack() {
        howToPlay.SetActive(true);
        forwardButton.SetActive(true);
        controls.SetActive(false);
        backButton.SetActive(false);
    }
    public void goForward() {
        howToPlay.SetActive(false);
        forwardButton.SetActive(false);
        controls.SetActive(true);
        backButton.SetActive(true);
    }
    public void backToMainMenu() {
        GameManager.instance.level = .5f;
        GameManager.instance.paused = false;
        GameManager.instance.ChangeScene("Assets/UI/Main Menu.unity");
    }

}
