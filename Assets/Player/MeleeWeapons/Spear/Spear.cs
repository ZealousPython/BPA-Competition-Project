using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private BoxCollider2D hitbox;
    public float damage;

    void Start()
    {
        anim = GetComponent<Animator>();
        hitbox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            anim.SetBool("stabbing", true);

        }
        else {
            anim.SetBool("stabbing", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().hit(damage);
        }
    }
    void SwingStart()
    {
        hitbox.enabled = true;
    }
    void SwingEnd()
    {
        hitbox.enabled = false;
    }
}
