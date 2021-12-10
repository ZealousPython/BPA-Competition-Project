using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mace : Weapon
{
    // Start is called before the first frame update
    private bool swinging = false;
    private Animator anim;
    public float damagelvl1 = 1;
    public float damagelvl2 = 2;
    public float damagelvl3 = 4;
    void Start()
    {
        anim = GetComponent<Animator>();     
    }

    // Update is called once per frame
    void Update()
    {
        if (swinging) GameManager.instance.player.GetComponent<PlayerMovement>().speed = 2;
        else GameManager.instance.player.GetComponent<PlayerMovement>().speed = 3;

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
        anim.SetBool("Swinging",true);
        swinging = true;
        anim.SetBool("Startup",false);
    }
    void OnTriggerEnter2D(Collider2D col){
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
