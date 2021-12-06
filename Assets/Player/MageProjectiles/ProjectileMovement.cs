using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : Weapon
{
    private Rigidbody2D body;
    private CircleCollider2D hitbox;
    [SerializeField] private float speed;
    private Vector2 directionCast = new Vector2(1,0);
    [SerializeField] private float lifeTime;
    public float damage = 1;
    public float damagelvl1;
    public float damagelvl2;
    public float damagelvl3;

    // Start is called before the first frame update
    void Awake(){
        if (level == 1)
            damage = damagelvl1;
        else if (level == 2)
            damage = damagelvl2;
        else if (level == 3)
            damage = damagelvl3;
        body = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<CircleCollider2D>();
        Destroy(gameObject, lifeTime);
    }
    public void updateProjectileLevel() {
        if (level == 1)
            damage = damagelvl1;
        else if (level == 2)
            damage = damagelvl2;
        else if (level == 3)
            damage = damagelvl3;
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = speed*directionCast;
        
    }
    public void updateDirection(Vector2 direction){
        directionCast = direction;
    }
    void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyHealth>().hit(damage);
            Destroy(gameObject);
        }
        else if (col.tag != "Player" && col.tag !="Projectile") {
            Destroy(gameObject);
        }
    }
}
