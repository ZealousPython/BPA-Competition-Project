using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    // Start is called before the first frame update
    public float coolDown;
    public float manaUsage;
    public float castTime;
    private float currentCastTime = 1;
    private bool casting = false;
    public int price;
    void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
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
        if (!casting)
        {
            casting = true;
            currentCastTime = castTime;
        }
    }
    public virtual void useSpell() { }
}
