using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject weapon;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            shoot();
        }
    }
    private void shoot(){
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation = new Vector3(rotation.x, rotation.y, rotation.z + 180);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - transform.position);
        direction.z = 0.0f;
        Vector3 directionNormalized = direction.normalized;
        GameObject p = (GameObject)Instantiate(weapon, transform.position, Quaternion.Euler(rotation));
        ThrowingDagger pscript = p.GetComponent<ThrowingDagger>();
        pscript.updateDirection(new Vector2(directionNormalized.x,directionNormalized.y));
    }
}
