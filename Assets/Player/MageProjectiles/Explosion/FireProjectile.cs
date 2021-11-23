using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : ProjectileMovement
{
    public GameObject explosion;
    public float explodeTimes = 3;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Player") 
            explode();
    }
    void explode()
    {
        GameObject p = (GameObject)Instantiate(explosion,transform.position,transform.rotation);
        explodeTimes--;
        if(explodeTimes <= 0)
            Destroy(gameObject);
    }
}
