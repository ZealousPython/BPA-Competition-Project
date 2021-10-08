using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RockCast : MonoBehaviour
{
    private GameObject rock;
    public float coolDown = 1;
    public float manaUsage = 15;
    public float castTime;
    private float currentCastTime = 0;
    private bool casting = false;

    private void Awake()
    {
        rock = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Player/MageProjectiles/RockThrow/Rock.prefab", typeof(GameObject));
    }
    private void Update()
    {
        if (casting)
            currentCastTime -= Time.deltaTime;
        if (currentCastTime <= 0)
        {

            currentCastTime = castTime;
            useSpell();
            casting = false;
        }
    }

    public void cast()
    {
        if (!casting)
        {
            casting = true;
            currentCastTime = castTime;
        }
    }
    public void useSpell()
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
