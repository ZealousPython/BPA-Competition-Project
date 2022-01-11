using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//script use to summon a barrage of ice spikes
public class CastIceSpike : Spell
{
    //declare variables
    public GameObject iceSpike;
    int iceSpikeNum = 10;
    public override void useSpell() {
        //spawn the amount of icespikes in different direction and speeds
        for (int i = 0; i < iceSpikeNum; i++)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = (mousePos - transform.position);
            direction.z = 0.0f;

            Vector2 directionNormalized = direction.normalized;
            directionNormalized = Quaternion.AngleAxis(Random.Range(-15f, 15f), Vector3.forward) * directionNormalized * Random.Range(.5f, 1f);

            float angle = Mathf.Atan2(directionNormalized.y, directionNormalized.x) * Mathf.Rad2Deg - 90;

            GameObject p = (GameObject)Instantiate(iceSpike, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
            ProjectileMovement pscript = p.GetComponent<ProjectileMovement>();
            pscript.updateDirection(new Vector2(directionNormalized.x, directionNormalized.y));
        }
    }
}
