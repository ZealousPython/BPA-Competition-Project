using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
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
        body = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<CircleCollider2D>();
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = speed * directionCast;

    }
    public void updateDirection(Vector2 direction)
    {
        directionCast = direction;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerHealth>().hit(damage);
            Destroy(gameObject);
        }
        else if (col.tag != "Enemy" && col.tag != "Projectile" && !transparent)
        {
            Destroy(gameObject);
        }
    }
}
