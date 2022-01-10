using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//explodes multiple times  before destroying itself
public class FireProjectile : ProjectileMovement
{
    public GameObject explosion;
    public float explodeTimes = 3;
    void OnTriggerEnter2D(Collider2D col)
    {
        //explode on collision with any object that is not the player(It can even explode on its own explosions)
        if (col.tag != "Player") 
            explode();
    }
    void explode()
    {
        //creates and explosion gameobject and checks weather or not to destroy this gameobject
        GameObject p = (GameObject)Instantiate(explosion,transform.position,transform.rotation);
        explodeTimes--;
        if(explodeTimes <= 0)
            Destroy(gameObject);
    }
}
