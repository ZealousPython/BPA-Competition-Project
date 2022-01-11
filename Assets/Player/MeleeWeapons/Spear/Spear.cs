using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//script for the spear to use to attack with
public class Spear : Weapon
{
    //declare variables
    private Animator anim;
    private BoxCollider2D hitbox;
    public float damagelvl1;
    public float damagelvl2;
    public float damagelvl3;

    // Start is called before the first frame update
    void Start()
    {
        //grab animator and box collider components
        anim = GetComponent<Animator>();
        hitbox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //while attacking start the stabbing animation
        if (Input.GetButton("attack"))
        {
            anim.SetBool("stabbing", true);

        }
        else {
            anim.SetBool("stabbing", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //while colliding and stabbing attack enemies
        if (collision.tag == "Enemy"&&anim.GetBool("stabbing"))
        {
            if(level == 1)
                collision.gameObject.GetComponent<EnemyHealth>().hit(damagelvl1);
            if (level == 2)
                collision.gameObject.GetComponent<EnemyHealth>().hit(damagelvl2);
            if (level == 3)
                collision.gameObject.GetComponent<EnemyHealth>().hit(damagelvl3);
        }
    }
    void SwingStart()
    {
        hitbox.enabled = true;
    }
    void SwingEnd()
    {
        hitbox.enabled = false;
    }
}
