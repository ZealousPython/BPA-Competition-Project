using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//used for phisical attacking enemies
public class PhysicalAttack : MonoBehaviour
{
    //declare variables
    private FollowPlayer ai;
    private Animator anim;
    private BoxCollider2D hitbox;
    
    public float cooldown = 5;
    private float attackCooldown = 0;
    public float damage = 1;

    void Start()
    {
        //initilize variables
        ai = GetComponent<FollowPlayer>();
        anim = GetComponent<Animator>();
        hitbox = GetComponent<BoxCollider2D>();
        attackCooldown = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        //make sure enemy is stopped/ is not attacking / and if the cooldown is done before it attacks
        if (ai.stopped && !ai.attacking && attackCooldown <= 0) {
            ai.attacking = true;
            anim.SetTrigger("attack");
        }
        //tick down the cooldown
        attackCooldown -= Time.deltaTime;

    }
    //called during the attack animation and enables the attack hitbox
    public void attackStart() {
        
        hitbox.enabled = true;
    }
    //called at the end of the attack animation disables hitbox and resets attackcooldown while also setting attacking and stopped to false
    public void attackEnd() {
        hitbox.enabled = false;
        ai.attacking = false;
        ai.stopped = false;
        attackCooldown = cooldown;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //whenever its hitbox collides with a player hit it and disable the hitbox
        if (collision.tag == "Player") {
            collision.gameObject.GetComponent<PlayerHealth>().hit(damage) ;
            hitbox.enabled = false;
        }
    }
}
