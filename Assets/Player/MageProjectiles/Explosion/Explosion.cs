using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//manage the damage dealing properties of the explosion prefab
public class Explosion : MonoBehaviour
{
    //delcare variables 
    private BoxCollider2D hitbox;
    public float damage;
    void Awake() {
        hitbox = GetComponent<BoxCollider2D>();
    }
    public void Explode() {
        //after exploding make sure the hitbox is disables
        hitbox.enabled = true;
    }
    void Update() {
        //while the hit box is eneabled damage every enemy within the hitbox radius
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
