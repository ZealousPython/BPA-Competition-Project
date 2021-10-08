﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CastIceSpike : MonoBehaviour
{
    private GameObject iceSpike;
    int iceSpikeNum = 10;
    public float coolDown = 2;
    public float manaUsage = 30;
    public float castTime;
    private float currentCastTime = 0;
    private bool casting = false;

    private void Awake()
    {
        iceSpike=(GameObject)AssetDatabase.LoadAssetAtPath("Assets/Player/MageProjectiles/IceSpike/IceSpike.prefab",typeof(GameObject));
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

    public void cast() {
        if (!casting) {
            casting = true;
            currentCastTime = castTime;
        }
    }
    public void useSpell() {
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