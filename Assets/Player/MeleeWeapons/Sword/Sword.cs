using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script for sword attack
public class Sword : Weapon
{
    //declare variables
    private Animator anim;
    private BoxCollider2D hitbox;
    public float damagelvl1 = 1;
    public float damagelvl2 = 2;
    public float damagelvl3 = 3;


    void Start()
    {
        anim = GetComponent<Animator>();
        hitbox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()

    {
        //while attacking play the swing animation
        if (Input.GetButton("attack") && !hitbox.enabled)
        {
            anim.SetTrigger("Swing");
            
        }    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //on trigger collision attack the enemy
        if (collision.tag == "Enemy") {
            if (level == 1) 
                collision.gameObject.GetComponent<EnemyHealth>().hit(damagelvl1);
            else if (level == 2)
                collision.gameObject.GetComponent<EnemyHealth>().hit(damagelvl2);
            else if (level == 3)
                collision.gameObject.GetComponent<EnemyHealth>().hit(damagelvl3);
        }
    }
    void SwingStart() {
        hitbox.enabled = true;
    }
    void SwingEnd() {
        hitbox.enabled = false;
    }
}
