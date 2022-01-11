using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//base class for all spells
public class Spell : MonoBehaviour
{
    // declare variables
    public float coolDown;
    public float manaUsage;
    public float castTime;
    private float currentCastTime = 1;
    private bool casting = false;
    public int price;
    // Update is called once per frame
    private void Update()
    {
        //manage casting time to make sure you cannot cast two spells at once
        if (casting)
            currentCastTime -= Time.deltaTime;
        if (currentCastTime <= 0 && casting)
        {
            currentCastTime = castTime;
            useSpell();
            casting = false;
        }
    }
    public void cast() {
        //starts casting
        if (!casting)
        {
            casting = true;
            currentCastTime = castTime;
        }
    }
    public virtual void useSpell() { }
}
