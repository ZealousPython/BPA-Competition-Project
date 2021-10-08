using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBasicAttack : MonoBehaviour
{
    [SerializeField] private float basicAttackCoolDown;

    private float currentCoolDown = 0;
    private float spellCoolDown = 0;
    private bool casting = false;
    private float mana = 0;
    public float maxMana;
    public float manaRegenRate;
    private float manaRegenTimer;
    public float manaRegenTime;
    public GameObject basicAttack;
    private CastIceSpike iceCast;
    // Start is called before the first frame update
    void Start()
    {
        iceCast = GetComponent<CastIceSpike>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCoolDown>0)
            currentCoolDown -= Time.deltaTime;
        if (spellCoolDown>0 && !casting) 
            spellCoolDown -= Time.deltaTime;
        if (manaRegenTimer <= 0 && mana < maxMana)
            mana += manaRegenRate * Time.deltaTime;
        if (mana > maxMana)
            mana = maxMana;
        if (manaRegenTimer > 0)
            manaRegenTimer -= Time.deltaTime;
        if (Input.GetMouseButton(0) && currentCoolDown <= 0)
        {
            shoot();
            currentCoolDown = basicAttackCoolDown;
        }
        if (Input.GetMouseButtonDown(1) && spellCoolDown <= 0&& mana>iceCast.manaUsage && !casting) {
            iceCast.cast();
            spellCoolDown = iceCast.coolDown;
            casting = true;
            mana -= iceCast.manaUsage;
            manaRegenTimer = manaRegenTime;
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
