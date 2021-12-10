using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCast : Spell
{
    public GameObject rock;

    private void Awake()
    {
    }
    

    public override void useSpell()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - transform.position);
        direction.z = 0.0f;

        Vector2 directionNormalized = direction.normalized;

        float angle = Mathf.Atan2(directionNormalized.y, directionNormalized.x) * Mathf.Rad2Deg;

        GameObject p = (GameObject)Instantiate(rock, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        ProjectileMovement pscript = p.GetComponent<ProjectileMovement>();
        pscript.updateDirection(new Vector2(directionNormalized.x, directionNormalized.y));
    }
}
