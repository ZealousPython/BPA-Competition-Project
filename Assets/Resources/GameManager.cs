using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Data.SQLite;
using System.IO;
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private int level = 1;

    public int gold = 0;
    public int playerClass = 0; // 0 is warrior, 1 is rouge, 2 is mage
    public float playerMaxHealth = 5;
    public float playerHealth = 0;
    public float playerMaxMana = 100;
    public float playerMana = 100;
    public float potions = 0;
    public GameObject playerWeapon;
    public GameObject player;
    public Spell[] mageSpells = new Spell[3]{null,null,null};
    public GameObject[] weaponsOwned = new GameObject[3]{null,null,null };
    public Image spellBar;
    public Image[] spriteUIImages;
    SQLiteConnection connection;


    public float bossHealth = 0;


    public bool playing = false;

    public string firstLevelScenePath = "Assets/levels/Forest/Forest.unity";
    public string secondLevelScenePath = "Assets/levels/Desert/Desert.unity";
    public string thirdLevelScenePath = "Assets/levels/Cave/Cave.unity";
    public string shopScenePath = "";
    void Awake()
    {
        if (instance == null) {
            instance = this;
            InitDataBase();
        }

        else if (instance != this)
        {
            instance.spellBar = spellBar;
            instance.spriteUIImages = spriteUIImages;
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && potions > 0)
        {
            potions--;
            player.GetComponent<PlayerHealth>().usePotion();
        }
        if (playing)
        {

        }
    }
    public void ChangeScene(string scenePath) {
        SceneManager.LoadScene(scenePath, LoadSceneMode.Single);
    }
    public void InitDataBase() {
        string databasePath = Application.streamingAssetsPath + "/Saves/saves.db";
        
        if (!File.Exists(databasePath))
        {
            File.Create(databasePath);
            print("File Created");
        }
        else {
            print("File Found");
        }
        databasePath = "URI=file:" + databasePath+";";
        print(databasePath);
        connection = new SQLiteConnection(databasePath);
        connection.Open();
        SQLiteCommand command = connection.CreateCommand();
        command.CommandText = "PRAGMA foreign_key = ON;";
        command.ExecuteNonQuery();
        command = connection.CreateCommand();
        command.CommandText = "CREATE TABLE IF NOT EXISTS playerSaves (File INTEGER PRIMARY KEY, " +
            "Level DECIMAL," +
            "PlayerHealth DECIMAL," +
            "Coins INTEGER," +
            "Potions INTEGER," +
            "Class INTEGER)";
        command.ExecuteNonQuery();
        command.CommandText = "CREATE TABLE IF NOT EXISTS weaponSaves (File INTEGER PRIMARY KEY, "+
            "WeaponOne TEXT," +
            "WeaponOneLevel INTEGER," +
            "WeaponTwo TEXT," +
            "WeaponTwoLevel INTEGER" +
            "WeaponThree TEXT" +
            "WeaponThreeLevel INTEGER)";
        command.ExecuteNonQuery();
        command.CommandText = "CREATE TABLE IF NOT EXISTS spellSaves (File INTEGER PRIMARY KEY, " +
            "SpellOne TEXT," +
            "SpellTwo TEXT," +
            "SpellThree TEXT)";
        command.ExecuteNonQuery();
        connection.Close();

    }
    public void saveFile() {
        connection.Open();
        SQLiteCommand command = connection.CreateCommand();
        command.CommandText = string.Format("INSERT or REPLACE INTO playerSaves(File, Level, PlayerHealth, Coins, Potions,Class) "+
            "VALUES({0},{1},{2},{3},{4},{5})", 0 , level , playerMaxHealth , gold , potions,playerClass);
        command.ExecuteNonQuery();
        command.CommandText = string.Format("INSERT or REPLACE INTO weaponSaves(File, Level, PlayerHealth, Coins, Potions,Class) " +
            "VALUES({0},{1},{2},{3},{4},{5},{6})", 0, level, playerMaxHealth, gold, potions, playerClass);
        command.ExecuteNonQuery();
        command.CommandText = string.Format("INSERT or REPLACE INTO spellSaves(File, Level, PlayerHealth, Coins, Potions,Class) " +
            "VALUES({0},{1},{2},{3})", 0, level, playerMaxHealth, gold, potions, playerClass);
        command.ExecuteNonQuery();
        connection.Close();

    }
    public void loadFile() {
        connection.Open();
        SQLiteCommand readCommand = connection.CreateCommand();
        SQLiteDataReader reader;
        string query = "SELECT * FROM saves";
        readCommand.CommandText = query;
        reader = readCommand.ExecuteReader();
        while (reader.Read())
        {
            Debug.Log("File: " + reader[0].ToString());
            Debug.Log("Level: " + reader[1].ToString());
            Debug.Log("Player Health: " + reader[2].ToString());
            Debug.Log("Coins: " + reader[3].ToString());
            Debug.Log("Potions: " + reader[4].ToString());
        }
        connection.Close();
    }
}