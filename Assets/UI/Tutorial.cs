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

    void Start() {
        GameManager.instance.weaponsOwned = weapons;
        GameManager.instance.playerWeapon = weapons[0];
        GameManager.instance.level = 0;
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
