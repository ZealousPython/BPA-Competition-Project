using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RougeWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D body;
    private CircleCollider2D hitbox;
    [SerializeField] private float speed;
    private Vector2 directionThrown = new Vector2(1, 0);
    [SerializeField] private float lifeTime;
    [SerializeField] private float peirce;
    [SerializeField] public float cooldown;
    // Start is called before the first frame update
    void Awake()
    {
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
        print("Direction Change");
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            print("Hit Enemy");
            peirce--;
            if (peirce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
