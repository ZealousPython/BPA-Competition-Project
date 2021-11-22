using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private int level = 1;

    public int gold = 0;
    public int playerClass = 0; // 0 is warrior, 1 is rouge, 2 is mage
    public float playerMaxHealth = 0;
    public float playerHealth = 0;
    public float playerMaxMana = 100;
    public float playerMana = 100;
    public float potions = 0;
    public GameObject playerWeapon;
    public GameObject player;
    public Spell[] mageSpells = { };


    public float bossHealth = 0;
    

    public bool playing = false;

    public string firstLevelScenePath = "Assets/levels/Forest/Forest.unity";
    public string secondLevelScenePath = "Assets/levels/Desert/Desert.unity";
    public string thirdLevelScenePath = "Assets/levels/Cave/Cave.unity";
    public string shopScenePath = "";
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if (playing)
        {
            if (Input.GetKeyDown(KeyCode.Q) && potions > 0)
            {
                potions--;
                player.GetComponent<PlayerHealth>().usePotion();
            }
        }
    }
    public void ChangeScene(string scenePath) {
        SceneManager.LoadScene(scenePath, LoadSceneMode.Single);
    }
}