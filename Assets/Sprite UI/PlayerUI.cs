using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image ManaBar;
    public Image HeartBarOneE;
    public Image HeartBarOneF;
    public Image HeartBarTwoE;
    public Image HeartBarTwoF;
    public Image SpellBar;
    public GameManager game;
    // Start is called before the first frame update
    void Start()
    {
        game = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        ManaBar.fillAmount = game.playerMana / game.playerMaxMana;
        HeartBarOneE.fillAmount = game.playerMaxHealth/10;
        HeartBarOneF.fillAmount = game.playerMaxHealth / 10 * (game.playerHealth / game.playerMaxHealth);
        HeartBarTwoE.fillAmount = (game.playerMaxHealth-10) / 10;
        HeartBarTwoF.fillAmount = (game.playerMaxHealth-10) / 10 * ((game.playerHealth-10) / (game.playerMaxHealth-10));
    }
}
