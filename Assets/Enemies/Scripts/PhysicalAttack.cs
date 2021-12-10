using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAttack : MonoBehaviour
{
    private FollowPlayer ai;
    private Animator anim;
    private BoxCollider2D hitbox;
    
    public float cooldown = 5;
    private float attackCooldown = 0;
    public float damage = 1;

    void Start()
    {
        ai = GetComponent<FollowPlayer>();
        anim = GetComponent<Animator>();
        hitbox = GetComponent<BoxCollider2D>();
        attackCooldown = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (ai.stopped && !ai.attacking && attackCooldown <= 0) {
            ai.attacking = true;
            anim.SetTrigger("attack");
        }
        attackCooldown -= Time.deltaTime;

    }
    public void attackStart() {
        hitbox.enabled = true;
    }
    public void attackEnd() {
        hitbox.enabled = false;
        ai.attacking = false;
        ai.stopped = false;
        attackCooldown = cooldown;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            collision.gameObject.GetComponent<PlayerHealth>().hit(damage) ;
            hitbox.enabled = false;
        }
    }
}
