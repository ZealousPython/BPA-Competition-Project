using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RougeWeapon : Weapon
{
    // Start is called before the first frame update
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
    // Start is called before the first frame update
    void Awake()
    {
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
        body = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<CircleCollider2D>();
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        body.velocity = speed * directionThrown;
    }
    public void updateDirection(Vector2 direction)
    {
        directionThrown = direction;
    }
    virtual public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyHealth>().hit(damage);
            peirce--;
            if (peirce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
