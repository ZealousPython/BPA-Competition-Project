using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//create a fireball that explodes mutiple times
public class FireCast : Spell
{
    public GameObject fireBolt;


    public override void useSpell()
    {
        //create the fire ball that explodes multiple times
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - transform.position);
        direction.z = 0.0f;

        Vector2 directionNormalized = direction.normalized;

        float angle = Mathf.Atan2(directionNormalized.y, directionNormalized.x) * Mathf.Rad2Deg;

        GameObject p = (GameObject)Instantiate(fireBolt, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        ProjectileMovement pscript = p.GetComponent<ProjectileMovement>();
        pscript.updateDirection(new Vector2(directionNormalized.x, directionNormalized.y));
    }
}
