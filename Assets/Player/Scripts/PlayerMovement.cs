using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private float rotationOffset = 90;
    private CircleCollider2D colliderCircle;
    private Rigidbody2D body;
    private Animator anim;

    private Vector2 movementDirection;

    private void Awake(){
        colliderCircle = GetComponent<CircleCollider2D>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update(){
        
        rotateTowardsMouse();
        //get input for movement
        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;

        //animation
        if (body.velocity == Vector2.zero) anim.SetBool("moving",false);
        else anim.SetBool("moving",true);

    }
    private void FixedUpdate(){
        //move player
        body.velocity = movementDirection*speed;
    }
    private void rotateTowardsMouse(){
        //rotate player towards mouse position
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg+rotationOffset;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
