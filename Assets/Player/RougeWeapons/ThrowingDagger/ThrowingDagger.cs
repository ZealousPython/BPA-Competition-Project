using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//script to manage the properties of the throwing dagger projectile
public class ThrowingDagger : RougeWeapon
{
    //declare variables
    private float spinTime = 0.3f;
    private Animator anim;
    bool spining = false;
    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //countdown until the dagger spins when it starts spinning lower the damage delt and make it so it has no pierce
        spinTime -= Time.deltaTime;
        if (spinTime <= 0 && !spining){
            anim.SetTrigger("spin");
            damage /= 2;
            spining = true;
            peirce -= 2;
        }
    }
}
