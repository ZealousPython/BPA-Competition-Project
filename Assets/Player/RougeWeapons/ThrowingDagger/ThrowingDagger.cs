using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingDagger : RougeWeapon
{
    // Start is called before the first frame update
    /*private Rigidbody2D body;
    private CircleCollider2D hitbox;
    [SerializeField] private float speed;
    private Vector2 directionThrown = new Vector2(1,0);
    [SerializeField] private float lifeTime;
    [SerializeField] private float peirce;
    */
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
        //body.velocity = speed*directionThrown;
        spinTime -= Time.deltaTime;
        if (spinTime <= 0 && !spining){
            anim.SetTrigger("spin");
            damage /= 2;
            spining = true;
            if (peirce < 2) {
                peirce = 0;
            }
        }
    }
    /*
    public void updateDirection(Vector2 direction){
        directionThrown = direction;
    }
    void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Enemy"){
            print("Hit Enemy");
            peirce--;
            if (peirce <= 0){
                Destroy(gameObject);
            }
        }
    }
    */
}
