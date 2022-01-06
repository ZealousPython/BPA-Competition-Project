using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//projectile movement for enemy projectiles
public class EnemyProjectile : MonoBehaviour
{
    //initilize variables
    private Rigidbody2D body;
    private CircleCollider2D hitbox;
    [SerializeField] private float speed;
    private Vector2 directionCast = new Vector2(1, 0);
    [SerializeField] private float lifeTime;
    public float damage = 1;
    public bool transparent = false;
    // Start is called before the first frame update
    void Awake()
    {
        //get the body and hitbox components and set the object to destroy based on its lifetime
        body = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<CircleCollider2D>();
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        //move the projectile in the direction set
        body.velocity = speed * directionCast;
    }
    public void updateDirection(Vector2 direction)
    {
        //set the direction cast to change where it is shot
        directionCast = direction;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //if the projectile collides with the player hurt the player and destroy the projectile
        if (col.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerHealth>().hit(damage);
            Destroy(gameObject);
        }
        //if the projectile hits anything else and is not labeled transparent destroy itself
        else if (col.tag != "Enemy" && col.tag != "Projectile" && !transparent)
        {
            Destroy(gameObject);
        }
    }
}
