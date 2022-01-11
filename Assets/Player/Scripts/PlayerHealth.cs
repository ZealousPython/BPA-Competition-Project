using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//manages the players health
public class PlayerHealth : MonoBehaviour
{
    //declare varaiables
    private GameManager game;
    private float maxHealth;
    private float health;
    void Start()
    {
        game = GameManager.instance;
        maxHealth = game.playerMaxHealth;
        health = game.playerHealth;
    }
    public void hit(float damage)
    {
        //when the player is hit take damage
        health -= damage;
        game.playerHealth = health;
    }
    public void usePotion() {
        //upon using a potion heal five health
        health += 5;
        if (health > maxHealth) {
            health = maxHealth;
        }
        game.playerHealth = health;
    }
}
