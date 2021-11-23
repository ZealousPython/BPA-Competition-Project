using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private BoxCollider2D hitbox;
    public float damage;
    void Awake() {
        hitbox = GetComponent<BoxCollider2D>();
    }
    public void Explode() {
        hitbox.enabled = true;
    }
    void Update() {
        if (hitbox.enabled)
        {
            Collider2D[] colliders = new Collider2D[20];
            ContactFilter2D a = new ContactFilter2D();
            a.NoFilter();
            hitbox.OverlapCollider(a, colliders);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != null)
                {
                    if (colliders[i].tag == "Enemy")
                    {
                        colliders[i].gameObject.GetComponent<EnemyHealth>().hit(damage);
                    }
                }
            }
            hitbox.enabled = false;
        }
    }
    void Dissapate() {
        Destroy(gameObject);
    }
}
