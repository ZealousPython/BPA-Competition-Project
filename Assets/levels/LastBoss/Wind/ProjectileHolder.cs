using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHolder : EnemyProjectile
{
    // Start is called before the first frame update
    public GameObject[] Projectiles;
    new public void updateDirection(Vector2 direction) {
        print("Hi");
        print(direction);
        for (int i = 0; i < Projectiles.Length; i++) {
            Projectiles[i].GetComponent<EnemyProjectile>().updateDirection(direction);
            
        }
    }
    void Update() { 
    
    }
     void Awake() { 
    
    }
}
