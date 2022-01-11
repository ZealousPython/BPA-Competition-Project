using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//manage the hearts and mana bar for the player
public class PlayerUI : MonoBehaviour
{
    //grab UI elements
    public Image ManaBar;
    public Image HeartBarOneE;
    public Image HeartBarOneF;
    public Image HeartBarTwoE;
    public Image HeartBarTwoF;
    public Image SpellBar;
    public Text Coins;
    public Text Potions;
    public GameManager game;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        game = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        //update the players health and mana
        ManaBar.fillAmount = game.playerMana / game.playerMaxMana;
        HeartBarOneE.fillAmount = game.playerMaxHealth/10;
        HeartBarOneF.fillAmount = game.playerMaxHealth / 10 * (game.playerHealth / game.playerMaxHealth);
        HeartBarTwoE.fillAmount = (game.playerMaxHealth-10) / 10;
        HeartBarTwoF.fillAmount = (game.playerMaxHealth-10) / 10 * ((game.playerHealth-10) / (game.playerMaxHealth-10));
        Coins.text = game.gold.ToString();
        Potions.text = game.potions.ToString();
    }
}
