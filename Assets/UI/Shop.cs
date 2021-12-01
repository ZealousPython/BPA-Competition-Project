using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private GameManager game;


    private List<GameObject> weaponsAvailable;
    private List<string> spellsAvailable;
    public List<string> allSpells;
    public List<GameObject> WarriorWeapons;
    public List<GameObject> RougeWeapons;

    private List<GameObject> playerWeapons;
    private List<string> playerSpells;
    private int playerClass;
    private int gold;

    public int heartPrice = 15;
    public int potionPrice = 5;

    void Start()
    {
        game = GameManager.instance;
        playerClass = game.playerClass;
        gold = game.gold;
        playerWeapons = game.weaponsOwned;
        playerSpells = game.mageSpells;
    }

    void updateShopValues() {
        gold = game.gold;
        playerWeapons = game.weaponsOwned;
        playerSpells = game.mageSpells;
    }

    void updateGameValues() {
        game.gold = gold;
        game.weaponsOwned = playerWeapons;
        game.mageSpells = playerSpells;
        updateShopValues();
    }

    public void getAvailableWeapons() {
        if (playerClass == 1)
        {
            for (int i = 0; i < WarriorWeapons.Count; i++)
            {
                bool weaponAvailable = true;
                for (int e = 0; e < playerWeapons.Count; e++)
                {
                    if (WarriorWeapons[i] == playerWeapons[e])
                        weaponAvailable = false;
                }
                if (weaponAvailable)
                    weaponsAvailable.Add(WarriorWeapons[i]);
            }
        }
        else if (playerClass == 2)
        {
            for (int i = 0; i < RougeWeapons.Count; i++)
            {
                bool weaponAvailable = true;
                for (int e = 0; e < playerWeapons.Count; e++)
                {
                    if (RougeWeapons[i] == playerWeapons[e])
                        weaponAvailable = false;
                }
                if (weaponAvailable)
                    weaponsAvailable.Add(RougeWeapons[i]);
            }
        }
        else if (playerClass == 3) {
            for (int i = 0; i < allSpells.Count; i++)
            {
                bool spellAvailable = true;
                for (int e = 0; e < playerSpells.Count; e++)
                {
                    if (allSpells[i] == playerSpells[e])
                        spellAvailable = false;
                }
                if (spellAvailable)
                    spellsAvailable.Add(allSpells[i]);
            }
        }
    }

    public void buyHeart() {
        if (gold >= heartPrice && game.playerMaxHealth < 20) {
            gold -= heartPrice;
            game.playerMaxHealth++;
        }
        updateGameValues();
    }

    public void buyPotion() {
        if (gold >= potionPrice)
        {
            gold -= potionPrice;
            game.potions++;
        }
        updateGameValues();
    }

    public void selectItem(GameObject item) {
        bool itemToBuy = false;
        for (int i = 0; i < weaponsAvailable.Count; i++) {
            if (item == weaponsAvailable[i]) {
                itemToBuy = true;
                Weapon weaponToBuy = weaponsAvailable[i].GetComponent<Weapon>();
                if (gold >= weaponToBuy.price) {
                    gold -= weaponToBuy.price;
                    playerWeapons.Add(weaponsAvailable[i]);
                    weaponsAvailable.RemoveAt(i);

                    break;
                }
            }
        }
        if (!itemToBuy) {
            for (int i = 0; i < playerWeapons.Count; i++) {
                if (item == playerWeapons[i]) {
                    if (item.GetComponent<Weapon>().price != 0)
                    {
                        int upgradePrice = (int)(0.5f * playerWeapons[i].GetComponent<Weapon>().price * playerWeapons[i].GetComponent<Weapon>().level);
                        if (upgradePrice <= gold)
                        {
                            gold -= upgradePrice;
                            playerWeapons[i].GetComponent<Weapon>().level++;
                        }
                    }
                    else {
                        int upgradePrice = 10 * playerWeapons[i].GetComponent<Weapon>().level;
                        if (upgradePrice <= gold) {
                            gold -= upgradePrice;
                            playerWeapons[i].GetComponent<Weapon>().level++;
                        }
                    }
                    break;
                }
            }
        }
        updateGameValues();
    }

    public void selectItem(string spell)
    {
        for (int i = 0; i < spellsAvailable.Count; i++)
        {
            if (spell == spellsAvailable[i])
            {
                Spell spellToBuy = new FireCast();
                if (spell == "Fire")
                    spellToBuy = new FireCast();
                else if (spell == "Ice")
                    spellToBuy = new CastIceSpike();
                else if (spell == "Rock")
                    spellToBuy = new RockCast();
                int spellPrice = spellToBuy.price;
                if (gold >= spellPrice)
                {
                    gold -= spellPrice;
                    playerSpells.Add(spellsAvailable[i]);
                    spellsAvailable.RemoveAt(i);
                    break;
                }
            }
        }
        updateGameValues();
    }

    public int getItemPrice(GameObject item) {
        return item.GetComponent<Weapon>().price;
    }

    public int getItemPrice(string spell) {
        Spell spellChosen = new FireCast();
        if (spell == "Fire")
            spellChosen = new FireCast();
        else if (spell == "Ice")
            spellChosen = new CastIceSpike();
        else if (spell == "Rock")
            spellChosen = new RockCast();
        return spellChosen.price;
    }
}
