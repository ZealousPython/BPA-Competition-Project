using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MageShopUIUpdater : MonoBehaviour
{
    public Button UpgradeMageBasicAttack;
    public Text MageBasicAttackPrice;
    public Text RockPrice;
    public Text FirePrice;
    public Text IcePrice;
    private GameManager game;
    public Sprite[] UpgradeUI;
    private MageBasicAttack Mage;

    void Awake() {
        
        
    }

    void Start() {
        game = GameManager.instance;
        Mage = game.player.GetComponent<MageBasicAttack>();
       
    }
    void Update() {
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
