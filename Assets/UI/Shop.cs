using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private GameManager game;


    private List<GameObject> weaponsAvailable = new List<GameObject>();
    private List<string> spellsAvailable = new List<string>();
    public List<string> allSpells;
    public List<GameObject> WarriorWeapons;
    public List<GameObject> RougeWeapons;

    private List<GameObject> playerWeapons;
    private List<string> playerSpells;
    private int playerClass;
    private int gold;

    public int heartPrice = 10;
    public int potionPrice = 5;
    MageBasicAttack Mage;

    void Start()
    {
        game = GameManager.instance;
        playerClass = game.playerClass;
        print(playerClass);
        gold = game.gold;
        playerWeapons = game.weaponsOwned;
        playerSpells = game.mageSpells;
        getAvailableWeapons();
        if (playerClass == 3)
        {
            Mage = game.player.GetComponent<MageBasicAttack>();
        }
    }

    void updateShopValues()
    {
        gold = game.gold;
        playerWeapons = game.weaponsOwned;
        playerSpells = game.mageSpells;
    }

    void updateGameValues()
    {
        game.gold = gold;
        game.weaponsOwned = playerWeapons;
        game.mageSpells = playerSpells;
        updateShopValues();
    }

    public void getAvailableWeapons()
    {
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
        else if (playerClass == 3)
        {
            print("Hi");
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

    public void buyHeart()
    {
        if (gold >= heartPrice && game.playerMaxHealth < 20)
        {
            gold -= heartPrice;
            game.playerMaxHealth++;
            game.playerHealth = game.playerMaxHealth;
        }
        updateGameValues();
    }

    public void buyPotion()
    {
        if (gold >= potionPrice)
        {
            gold -= potionPrice;
            game.potions++;
        }
        updateGameValues();
    }

    public void selectItem(GameObject item)
    {
        bool itemToBuy = false;
        for (int i = 0; i < weaponsAvailable.Count; i++)
        {
            if (item == weaponsAvailable[i])
            {
                itemToBuy = true;
                Weapon weaponToBuy = weaponsAvailable[i].GetComponent<Weapon>();
                if (gold >= weaponToBuy.price)
                {
                    gold -= weaponToBuy.price;
                    playerWeapons.Add(weaponsAvailable[i]);
                    game.weaponLevels.Add(1);
                    weaponsAvailable.RemoveAt(i);

                    break;
                }
            }
        }
        if (!itemToBuy)
        {
            if (playerClass == 3)
            {
                if (item.gameObject.name == Mage.basicAttack.name)
                {
                    if (game.MageBasicAttackLevel != 3)
                    {
                        print("level");
                        int upgradePrice = 25 * game.MageBasicAttackLevel;
                        if (upgradePrice <= gold)
                        {
                            gold -= upgradePrice;
                            game.MageBasicAttackLevel++;
                        }
                    }
                }
            }
            for (int i = 0; i < playerWeapons.Count; i++)
            {
                if (item == playerWeapons[i])
                {
                    for (int e = 0; e < game.weaponsOwned.Count; e++)
                    {
                        if (game.weaponLevels[i] < 3)
                        {
                            if (item.GetComponent<Weapon>().price != 0)
                            {
                                if (game.weaponsOwned[i].name == item.GetComponent<Weapon>().name)
                                {
                                    int upgradePrice = (int)(playerWeapons[i].GetComponent<Weapon>().price * game.weaponLevels[i]);
                                    if (upgradePrice <= gold)
                                    {
                                        gold -= upgradePrice;
                                        game.weaponLevels[i]++;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                int upgradePrice = 25 * game.weaponLevels[i];
                                if (upgradePrice <= gold)
                                {
                                    gold -= upgradePrice;
                                    game.weaponLevels[i]++;
                                    break;
                                }
                            }
                        }
                    }
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
                Spell spellToBuy = Mage.fireCast;
                if (spell == "Fire")
                    spellToBuy = Mage.fireCast;
                else if (spell == "Ice")
                    spellToBuy = Mage.iceCast;
                else if (spell == "Rock")
                    spellToBuy = Mage.rockCast;
                int spellPrice = spellToBuy.price;
                if (gold >= spellPrice)
                {
                    gold -= spellPrice;
                    playerSpells.Add(spellsAvailable[i]);
                    spellsAvailable.RemoveAt(i);
                    Mage.updateSpellUI();
                    break;
                }
            }
        }
        updateGameValues();
    }

    public int getItemPrice(GameObject item)
    {
        return item.GetComponent<Weapon>().price;
    }

    public int getItemPrice(string spell)
    {
        Spell spellChosen = Mage.fireCast;
        if (spell == "Fire")
            spellChosen = Mage.fireCast;
        else if (spell == "Ice")
            spellChosen = Mage.iceCast;
        else if (spell == "Rock")
            spellChosen = Mage.rockCast;
        return spellChosen.price;
    }
}
