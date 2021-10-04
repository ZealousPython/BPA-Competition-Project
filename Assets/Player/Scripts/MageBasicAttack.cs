using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBasicAttack : MonoBehaviour
{   
    public GameObject fireBall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            shoot();
        }
    }
    private void shoot(){
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - transform.position);
        direction.z = 0.0f;
        Vector3 directionNormalized = direction.normalized;
        GameObject p = (GameObject)Instantiate(fireBall, transform.position, Quaternion.identity);
        ProjectileMovement pscript = p.GetComponent<ProjectileMovement>();
        pscript.updateDirection(new Vector2(directionNormalized.x,directionNormalized.y));
    }
}
