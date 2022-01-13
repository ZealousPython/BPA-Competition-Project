using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    //variable declarations
    private GameManager game;


    public List<GameObject> weaponsAvailable = new List<GameObject>();
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
    public MageBasicAttack Mage;
    public WarriorWeapons Warrior;
    public RogueAttack Rogue;

    void Start()
    {
        //get global variables and call avalilable weapons
        game = GameManager.instance;
        playerClass = game.playerClass;
        gold = game.gold;
        playerWeapons = game.weaponsOwned;
        playerSpells = game.mageSpells;
        getAvailableWeapons();
        Time.timeScale = 1;
    }

    void updateShopValues()
    {
        //updates the shop values based on Gamemanager
        gold = game.gold;
        playerWeapons = game.weaponsOwned;
        playerSpells = game.mageSpells;
    }

    void updateGameValues()
    {
        //updates the gameManager values based on the current shop values
        game.gold = gold;
        game.weaponsOwned = playerWeapons;
        game.mageSpells = playerSpells;
        updateShopValues();
    }

    public void getAvailableWeapons()
    {
        //get weapons and spells that can be bought based on each player type
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
        //if the player has enough gold add a heart in exchange for gold and heals player too full
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
        //if player has enough gold adds a potion and removes gold
        if (gold >= potionPrice)
        {
            gold -= potionPrice;
            game.potions++;
        }
        updateGameValues();
    }

    public void selectItem(GameObject item)
    {
        //When the player selects and item handle the item weather it needs to be bought or upgraded
        bool itemToBuy = false;
        for (int i = 0; i < weaponsAvailable.Count; i++)
        {
            //loops through available weapons and see if the weapon can be bought if so buy it and add it to the players inventory
            if (item == weaponsAvailable[i])
            {
                itemToBuy = true;
                Weapon weaponToBuy = weaponsAvailable[i].GetComponent<Weapon>();
                if (gold >= weaponToBuy.price)
                {
                    gold -= weaponToBuy.price;
                    playerWeapons.Add(weaponsAvailable[i]);
                    weaponsAvailable.RemoveAt(i);

                    break;
                }
            }
        }
        //if the item cannot be bought check if it can be upgraded for each player type
        if (!itemToBuy)
        {
            if (playerClass == 3)
            {
                // since the mage only has one upgradedble item check to see if it is one and if so increase the weapon level if there is enough gold
                if (item.gameObject.name == Mage.basicAttack.name)
                {
                    if (game.MageBasicAttackLevel != 3)
                    {
                        int upgradePrice = 25 * game.MageBasicAttackLevel;
                        if (upgradePrice <= gold)
                        {
                            gold -= upgradePrice;
                            game.MageBasicAttackLevel++;
                        }
                    }
                }
            }
            //check through all weapons and see if the weapon can be upgraded and if so upgrad it
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
        //if the item selected is a spell check which one it is and buy it and update the spell UI
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
    {//get the price of an item through
        return item.GetComponent<Weapon>().price;
    }

    public int getItemPrice(string spell)
    {
        //get which spell is veing passed through and return the price
        Spell spellChosen = Mage.fireCast;
        if (spell == "Fire")
            spellChosen = Mage.fireCast;
        else if (spell == "Ice")
            spellChosen = Mage.iceCast;
        else if (spell == "Rock")
            spellChosen = Mage.rockCast;
        return spellChosen.price;
    }
    //methods for each button to run methods from Gamemanager
    public void NextLevel() {
        GameManager.instance.endLevel();
    }
    public void Save()
    {
        GameManager.instance.saveFile();
    }
    public void SaveAndQuit() {
        GameManager.instance.saveFile();
        GameManager.instance.ChangeScene("Assets/UI/Main Menu.unity");
    }
}
