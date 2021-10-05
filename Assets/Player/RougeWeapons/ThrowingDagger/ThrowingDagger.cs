using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingDagger : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D body;
    private CircleCollider2D hitbox;
    [SerializeField] private float speed;
    private Vector2 directionThrown = new Vector2(1,0);
    [SerializeField] private float lifeTime;
    private float spinTime = 0.25f;
    private Animator anim;
    // Start is called before the first frame update
    void Awake(){
        body = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = speed*directionThrown;
        spinTime -= Time.deltaTime;
        if (spinTime <= 0){
            anim.SetTrigger("spin");
        }
    }
    public void updateDirection(Vector2 direction){
        directionThrown = direction;
    }
    void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Enemy"){
            print("Hit Enemy");
            Destroy(gameObject);
        }
    }
}
