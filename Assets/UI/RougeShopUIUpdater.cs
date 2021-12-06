using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RougeShopUIUpdater : MonoBehaviour
{
    public Button UpgradeDagger;
    public Button UpgradeJavelin;
    public Button UpgradeShurikan;
    public Text DaggerPrice;
    public Text JavelinPrice;
    public Text ShurikanPrice;
    private GameManager game;
    public Sprite[] UpgradeUI;
    public GameObject JavelinWeapon;
    public GameObject ShurikanWeapon;

    void Awake() {
        
        
    }

    void Start() {
        game = GameManager.instance;
       
    }
    void Update() {
        if (game.weaponLevels[0] != 3)
            DaggerPrice.text = ((int)game.weaponLevels[0] * 25).ToString();
        else
            DaggerPrice.text = "";
        UpgradeDagger.image.sprite = UpgradeUI[game.weaponLevels[0] - 1];
        if (game.weaponsOwned.Count <= 1)
        {
            JavelinPrice.text = ((int)JavelinWeapon.GetComponent<Weapon>().price).ToString();
            ShurikanPrice.text = ((int)ShurikanWeapon.GetComponent<Weapon>().price).ToString();
        }
        if (game.weaponsOwned.Count == 2)
        {
            if (game.weaponsOwned[1].name == "Spear") {
                if (game.weaponLevels[1] != 3)
                {
                    JavelinPrice.text = ((int)JavelinWeapon.GetComponent<Weapon>().price * game.weaponLevels[1]).ToString();
                }
                else
                    JavelinPrice.text = "";
                UpgradeJavelin.image.sprite = UpgradeUI[game.weaponLevels[1] - 1];
            }
            else if (game.weaponsOwned[1].name == "Mace")
            {
                if (game.weaponLevels[1] != 3)
                {
                    ShurikanPrice.text = ((int)ShurikanWeapon.GetComponent<Weapon>().price * game.weaponLevels[1]).ToString();
                }
                else
                    ShurikanPrice.text = "";
                UpgradeShurikan.image.sprite = UpgradeUI[game.weaponLevels[1] - 1];
            }

        }
        else if (game.weaponsOwned.Count == 3) {
            if (game.weaponsOwned[2].name == "Spear")
            {
                if (game.weaponLevels[2] != 3)
                {
                    JavelinPrice.text = ((int)JavelinWeapon.GetComponent<Weapon>().price * game.weaponLevels[2]).ToString();
                }
                else
                    JavelinPrice.text = "";
                UpgradeJavelin.image.sprite = UpgradeUI[game.weaponLevels[2] - 1];

            }
            else if (game.weaponsOwned[2].name == "Mace")
            {
                if (game.weaponLevels[2] != 3)
                {
                    ShurikanPrice.text = ((int)ShurikanWeapon.GetComponent<Weapon>().price * game.weaponLevels[2]).ToString();
                }
                else
                    ShurikanPrice.text = "";

                UpgradeShurikan.image.sprite = UpgradeUI[game.weaponLevels[2] - 1];
            }
            if (game.weaponsOwned[1].name == "Spear")
            {
                if (game.weaponLevels[1] != 3)
                {
                    JavelinPrice.text = ((int)JavelinWeapon.GetComponent<Weapon>().price * game.weaponLevels[1]).ToString();
                }
                else
                    JavelinPrice.text = "";
                UpgradeJavelin.image.sprite = UpgradeUI[game.weaponLevels[1] - 1];
            }
            else if (game.weaponsOwned[1].name == "Mace")
            {
                if (game.weaponLevels[1] != 3)
                {
                    ShurikanPrice.text = ((int)ShurikanWeapon.GetComponent<Weapon>().price * game.weaponLevels[1]).ToString();
                }
                else
                    ShurikanPrice.text = "";
                UpgradeShurikan.image.sprite = UpgradeUI[game.weaponLevels[1] - 1];
            }
        }
        
    }
}
