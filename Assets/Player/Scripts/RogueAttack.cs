using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject weapon;
    [SerializeField] private float coolDown;
    private float currentCoolDown = 0;
    void Start()
    {
        
    }

    void Update()
    {
        currentCoolDown -= Time.deltaTime;
        if (Input.GetMouseButton(0) && currentCoolDown <= 0){
            shoot();
            currentCoolDown = coolDown;
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
