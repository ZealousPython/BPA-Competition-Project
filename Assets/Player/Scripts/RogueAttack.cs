using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RogueAttack : MonoBehaviour
{
    //variable declaration
    private GameObject weapon;
    private float coolDown;
    private float currentCoolDown = 0;
    public List<GameObject> weapons;
    public List<int> weaponLevels = new List<int>();
    public int currentWeaponLevel = 1;
    private int weaponSlot = 0;
    public Image SpellBar;
    public Sprite[] SpellBarFrames;
    private Vector2[] SpellUIPositions = new Vector2[] { new Vector2(-56, 8), new Vector2(-9, 10), new Vector2(34, 11) };
    public Sprite[] WeaponUISprites;
    public Image[] SpriteUIImages;
    private float mana;
    private float maxMana;
    public float manaRegenRate = 5;
    // Start is called before the first frame update
    void Start()
    {
        //grab variables from gamemanager
        maxMana = GameManager.instance.playerMaxMana;
        mana = GameManager.instance.playerMana;
        weapon = GameManager.instance.playerWeapon;
        weapons = GameManager.instance.weaponsOwned;
        SpellBar = GameManager.instance.spellBar;
        SpriteUIImages = GameManager.instance.spriteUIImages;
        weaponLevels = GameManager.instance.weaponLevels;

        //update the Player UI
        for (int i = 0; i < weapons.Count; i++)
        {
            SpriteUIImages[i].enabled = true;
            if (weapons[i].GetComponent<Weapon>().name == "Dagger")
                SpriteUIImages[i].sprite = WeaponUISprites[0];
            else if (weapons[i].GetComponent<Weapon>().name == "Javelin")
            {
                SpriteUIImages[i].sprite = WeaponUISprites[1];
                SpriteUIImages[i].transform.localScale = new Vector3(.5f, .5f, 1);
            }
            else if (weapons[i].GetComponent<Weapon>().name == "Shurikan")
            {
                SpriteUIImages[i].sprite = WeaponUISprites[2];
                SpriteUIImages[i].transform.localScale = new Vector3(.5f, .5f, 1);
            }
            else
            {
                SpriteUIImages[i].transform.localScale = new Vector3(1, 1, 1);
                SpriteUIImages[i].sprite = null;
                SpriteUIImages[i].enabled = false;
            }
        }
        SpellBar.sprite = SpellBarFrames[1];
        weapon = weapons[0];
        coolDown = weapon.GetComponent<RougeWeapon>().cooldown;
        currentWeaponLevel = weaponLevels[0];
    }

    void Update()
    {
        if ( mana < maxMana)
            mana += manaRegenRate * Time.deltaTime;
        if (mana > maxMana)
            mana = maxMana;
        currentCoolDown -= Time.deltaTime;
        if (Input.GetButton("attack") && currentCoolDown <= 0){
            shoot();
            currentCoolDown = coolDown;
        }
        if (Input.GetButton("attack2") && mana >= 90)
        {
            mana -= 90;
            Special();
        }
        changeWeapons();
        GameManager.instance.playerMana = mana;
    }
    private void changeWeapons() {
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            weaponSlot += 1;
            if (weaponSlot > weapons.Count - 1)
            {
                weaponSlot = 0;
            }
            SpellBar.sprite = SpellBarFrames[weaponSlot + 1];
            weapon = weapons[weaponSlot];
            coolDown = weapon.GetComponent<RougeWeapon>().cooldown;
            currentWeaponLevel = weaponLevels[weaponSlot];
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            weaponSlot -= 1;
            if (weaponSlot < 0)
            {
                weaponSlot = weapons.Count - 1;
            }
            SpellBar.sprite = SpellBarFrames[weaponSlot + 1];
            weapon = weapons[weaponSlot];
            coolDown = weapon.GetComponent<RougeWeapon>().cooldown;
            currentWeaponLevel = weaponLevels[weaponSlot];
        }
    }
    public void updateSpellUI()
    {
        weapons = GameManager.instance.weaponsOwned;
        weapons.Clear();
        for (int i = 0; i < weapons.Count; i++)
        {
            SpriteUIImages[i].enabled = true;
            if (weapons[i].GetComponent<Weapon>().name == "Dagger")
                SpriteUIImages[i].sprite = WeaponUISprites[0];
            else if (weapons[i].GetComponent<Weapon>().name == "Javelin")
                SpriteUIImages[i].sprite = WeaponUISprites[1];
            else if (weapons[i].GetComponent<Weapon>().name == "Shurikan")
                SpriteUIImages[i].sprite = WeaponUISprites[2];
            else
            {
                SpriteUIImages[i].sprite = null;
                SpriteUIImages[i].enabled = false;
            }
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
    private void Special() {
        for (int i = 0; i < 10; i++)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = (mousePos - transform.position);
            direction.z = 0.0f;

            Vector2 directionNormalized = direction.normalized;
            directionNormalized = Quaternion.AngleAxis(Random.Range(-15f, 15f), Vector3.forward) * directionNormalized * Random.Range(.5f, 1f);

            float angle = Mathf.Atan2(directionNormalized.y, directionNormalized.x) * Mathf.Rad2Deg - 90;

            GameObject p = (GameObject)Instantiate(weapon, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
            RougeWeapon pscript = p.GetComponent<RougeWeapon>();
            pscript.updateDirection(new Vector2(directionNormalized.x, directionNormalized.y));
        }
    }
}
