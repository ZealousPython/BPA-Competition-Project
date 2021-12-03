using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueAttack : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject weapon;
    private float coolDown;
    private float currentCoolDown = 0;
    public List<int> weaponLevels = new List<int>();
    public int currentWeaponLevel = 1;
    void Start()
    {
        weapon = GameManager.instance.playerWeapon;
        coolDown = weapon.GetComponent<RougeWeapon>().cooldown;
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
        RougeWeapon pscript = p.GetComponent<RougeWeapon>();
        pscript.level = currentWeaponLevel;
        pscript.updateProjectileLevel();
        pscript.updateDirection(new Vector2(directionNormalized.x,directionNormalized.y));
    }
}
