using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private int level = 1;

    public int gold = 0;
    public int playerClass = 0; // 0 is warrior, 1 is rouge, 2 is mage
    public float playerMaxHealth = 6;
    public float playerHealth = 3;
    public float playerMaxMana = 100;
    public float playerMana = 100;
    public float potions = 2;
    public GameObject player;
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
        if (Input.GetKeyDown(KeyCode.Q) && potions > 0) {
            potions--;
            player.GetComponent<PlayerHealth>().usePotion();
        }
    }
}