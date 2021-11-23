﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FireCast : Spell
{
    private GameObject fireBolt;

    private void Awake()
    {
        fireBolt = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Player/MageProjectiles/Explosion/FireProjectile.prefab", typeof(GameObject));
    }


    public override void useSpell()
    {
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