using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager game;
    private float maxHealth;
    private float health;
    void Start()
    {
        game = GameManager.instance;
        maxHealth = game.playerMaxHealth;
        health = game.playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void hit(float damage)
    {
        health -= damage;
        if (health < 0)
            print("dead");
        game.playerHealth = health;
    }
    public void usePotion() {
        health += 5;
        if (health > maxHealth) {
            health = maxHealth;
        }
        game.playerHealth = health;
    }
}
