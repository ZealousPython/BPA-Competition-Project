using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mace : MonoBehaviour
{
    // Start is called before the first frame update
    private bool swinging = false;
    private Animator anim;
    public float damage = 1;
    void Start()
    {
        anim = GetComponent<Animator>();     
    }

    // Update is called once per frame
    void Update()
    {
        if (!swinging && Input.GetMouseButtonDown(0)){
            anim.SetTrigger("Swing");
            anim.SetBool("Startup",true);
        }        
        else if (!Input.GetMouseButton(0)){
            anim.SetBool("Swinging",false);
            swinging = false;
        }
    }
    public void startUpFinished(){
        anim.SetBool("Swinging",true);
        swinging = true;
        anim.SetBool("Startup",false);
    }
    void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Enemy" && swinging){
            col.gameObject.GetComponent<EnemyHealth>().hit(damage);
        }
    }
}
