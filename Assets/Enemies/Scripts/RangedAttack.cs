using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//script for ranged attacking enemies
public class RangedAttack : MonoBehaviour
{
    //declare variables
    public GameObject weapon;
    private FollowPlayer ai;
    private Animator anim;
    private BoxCollider2D hitbox;

    public float cooldown = 5;
    private float attackCooldown = 5;
    public float rotation_offset = -90;
    void Start()
    {
        //initilize needed variables
        ai = GetComponent<FollowPlayer>();
        anim = GetComponent<Animator>();
        hitbox = GetComponent<BoxCollider2D>();
        attackCooldown = cooldown;
    }
    void Update()
    {
        //start attacking if cooldown is done is stopped and not attacking
        if (ai.stopped && !ai.attacking && attackCooldown <= 0)
        {
            ai.attacking = true;
            anim.SetTrigger("attack");
        }
        //tick down the cooldown
        attackCooldown -= Time.deltaTime;

    }
    public void shoot()
    {
        //get the rotation of the enemy and create a new rotation with a offset
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation = new Vector3(rotation.x, rotation.y, rotation.z +rotation_offset);
        // get the distance between the enemy position and the target and get a normalized direction between the two
        Vector3 direction = (ai.target.transform.position - transform.position);
        direction.z = 0.0f;
        Vector3 directionNormalized = direction.normalized;
        //create a projectile at the current position rotated towards the player
        GameObject p = (GameObject)Instantiate(weapon, transform.position, Quaternion.Euler(rotation));
        // get the projectiles movement script and set the direction of the projectile
        EnemyProjectile pscript = p.GetComponent<EnemyProjectile>();
        pscript.updateDirection(new Vector2(directionNormalized.x, directionNormalized.y));
        //reset attackcooldown
        attackCooldown = cooldown;
        ai.attacking = false;
    }
}
