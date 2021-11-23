using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Weapon
{
    // Start is called before the first frame update
    private Animator anim;
    private BoxCollider2D hitbox;
    public float damagelvl1;
    public float damagelvl2;
    public float damagelvl3;

    void Start()
    {
        anim = GetComponent<Animator>();
        hitbox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            anim.SetBool("stabbing", true);

        }
        else {
            anim.SetBool("stabbing", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
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
