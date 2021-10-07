using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBasicAttack : MonoBehaviour
{
    [SerializeField] private float coolDown;
    private float currentCoolDown = 0;
    public GameObject basicAttack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentCoolDown -= Time.deltaTime;
        if (Input.GetMouseButton(0) && currentCoolDown <= 0)
        {
            shoot();
            currentCoolDown = coolDown;
        }
    }
    private void shoot(){
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - transform.position);
        direction.z = 0.0f;
        Vector3 directionNormalized = direction.normalized;
        GameObject p = (GameObject)Instantiate(basicAttack, transform.position, Quaternion.identity);
        ProjectileMovement pscript = p.GetComponent<ProjectileMovement>();
        pscript.updateDirection(new Vector2(directionNormalized.x,directionNormalized.y));
    }
}
