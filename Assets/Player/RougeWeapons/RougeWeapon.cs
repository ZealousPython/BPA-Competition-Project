using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//base class for rouge weapons
public class RougeWeapon : Weapon
{
    //declare variables
    private Rigidbody2D body;
    private CircleCollider2D hitbox;
    [SerializeField] private float speed;
    private Vector2 directionThrown = new Vector2(1, 0);
    [SerializeField] private float lifeTime;
    [SerializeField] public float peirce;
    [SerializeField] public float cooldown;
    public float piercelvl1;
    public float piercelvl2;
    public float piercelvl3;
    public float damagelvl1;
    public float damagelvl2;
    public float damagelvl3;
    public float damage;
   
    void Awake()
    {
        //assign damage and pierce based on level
        if (level == 1) {
            peirce = piercelvl1;
            damage = damagelvl1;
        }
        else if (level == 2) {
            peirce = piercelvl2;
            damage = damagelvl2;
        }
        else if (level == 3) {
            peirce = piercelvl3;
            damage = damagelvl3;
        }
        //ge body and hitbox and destroy the projectile after a set amount of time
        body = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<CircleCollider2D>();
        Destroy(gameObject, lifeTime);
    }
    public void updateProjectileLevel()
    {
        //update the piecer and damage based on level
        if (level == 1)
        {
            peirce = piercelvl1;
            damage = damagelvl1;
        }
        else if (level == 2)
        {
            peirce = piercelvl2;
            damage = damagelvl2;
        }
        else if (level == 3)
        {
            peirce = piercelvl3;
            damage = damagelvl3;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //move the projectile every physics frame
        body.velocity = speed * directionThrown;
    }
    public void updateDirection(Vector2 direction)
    {
        //update the movement direction
        directionThrown = direction;
    }
    virtual public void OnTriggerEnter2D(Collider2D col)
    {
        //hit enemy and destroy the object based on pierce
        if (col.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyHealth>().hit(damage);
            peirce--;
            if (peirce <= 0)
            {
                Destroy(gameObject);
            }
        }
        else if (col.tag != "Projectile" && col.tag != "Player")
            Destroy(gameObject);
    }
}
