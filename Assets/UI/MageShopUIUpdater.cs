using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//manage the buy and upgrade buttons for the spell UI
public class MageShopUIUpdater : MonoBehaviour
{
    //Get UI Buttons
    public Button UpgradeMageBasicAttack;
    public Text MageBasicAttackPrice;
    public Text RockPrice;
    public Text FirePrice;
    public Text IcePrice;
    private GameManager game;
    public Sprite[] UpgradeUI;
    public GameObject player;
    private MageBasicAttack Mage;

    void Start() {
        game = GameManager.instance;
        GameManager.instance.player = player;
        Mage = player.GetComponent<MageBasicAttack>();

        Time.timeScale = 1;
    }
    void Update() {
        //update the prices of the spells and the leveling  for the basic attack
        RockPrice.text = Mage.rockCast.price.ToString();
        FirePrice.text = Mage.fireCast.price.ToString();
        IcePrice.text = Mage.iceCast.price.ToString();
        if (game.MageBasicAttackLevel != 3)
            MageBasicAttackPrice.text = ((int)game.MageBasicAttackLevel * 25).ToString();
        else
            MageBasicAttackPrice.text = "";
        UpgradeMageBasicAttack.image.sprite = UpgradeUI[game.MageBasicAttackLevel - 1];
    }
}
