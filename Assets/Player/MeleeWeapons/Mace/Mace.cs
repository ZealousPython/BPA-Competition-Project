using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//manages mace swinging
public class Mace : Weapon
{
    //declare variables
    private bool swinging = false;
    private Animator anim;
    public float damagelvl1 = 1;
    public float damagelvl2 = 2;
    public float damagelvl3 = 4;
    void Start()
    {
        //get the animator component
        anim = GetComponent<Animator>();     
    }

    // Update is called once per frame
    void Update()
    {
        //change the speed of the warrior while swinging unless the player is using their ability
        if (!GameManager.instance.player.GetComponent<WarriorWeapons>().Speed)
        {
            if (swinging) GameManager.instance.player.GetComponent<PlayerMovement>().speed = 2;
            else GameManager.instance.player.GetComponent<PlayerMovement>().speed = 3;
        }
        //start the swing animation if the buuton is held or stop the swinging animation
        if (!swinging && Input.GetButtonDown("attack"))
        {
            anim.SetTrigger("Swing");
            anim.SetBool("Startup",true);
        }        
        else if (!Input.GetButton("attack"))
        {
            anim.SetBool("Swinging",false);
            swinging = false;
        }
    }
    public void startUpFinished(){
        //after the start up animation is done start the swinging animation
        anim.SetBool("Swinging",true);
        swinging = true;
        anim.SetBool("Startup",false);
    }
    void OnTriggerEnter2D(Collider2D col){
        //while colliding and swinging attack the enemy depending on the damage level
        if (col.tag == "Enemy" && swinging){
            if (level == 1)
                col.gameObject.GetComponent<EnemyHealth>().hit(damagelvl1);
            if (level == 2)
                col.gameObject.GetComponent<EnemyHealth>().hit(damagelvl2);
            if (level == 3)
                col.gameObject.GetComponent<EnemyHealth>().hit(damagelvl3);
        }
    }
}
