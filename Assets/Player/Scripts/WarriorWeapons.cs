using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//manage the warriors weapon selection and special usage
public class WarriorWeapons : MonoBehaviour
{
    //declare variables
    private GameObject currentWeapon;
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
    private float speedTimer;
    public bool Speed = false;
    private PlayerMovement moveScript;
    // Start is called before the first frame update
    void Start()
    {
        //initilize variables
        moveScript = GetComponent<PlayerMovement>();
        maxMana = GameManager.instance.playerMaxMana;
        mana = GameManager.instance.playerMana;
        currentWeapon = GameManager.instance.playerWeapon;
        weaponLevels = GameManager.instance.weaponLevels;
        weapons = GameManager.instance.weaponsOwned;

        SpellBar = GameManager.instance.spellBar;
        SpriteUIImages = GameManager.instance.spriteUIImages;
        //set up the weapon UI
        for (int i = 0; i < weapons.Count; i++)
        {
            SpriteUIImages[i].enabled = true;
            if (weapons[i].GetComponent<Weapon>().name == "Sword")
                SpriteUIImages[i].sprite = WeaponUISprites[0];
            else if (weapons[i].GetComponent<Weapon>().name == "Spear")
            {
                SpriteUIImages[i].sprite = WeaponUISprites[1];
                SpriteUIImages[i].transform.localScale = new Vector3(1, 1, 1);
            }
            else if (weapons[i].GetComponent<Weapon>().name == "Mace")
            {
                SpriteUIImages[i].sprite = WeaponUISprites[2];
                SpriteUIImages[i].transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                SpriteUIImages[i].transform.localScale = new Vector3(1, 1, 1);
                SpriteUIImages[i].sprite = null;
                SpriteUIImages[i].enabled = false;
            }
        }
        SpellBar.sprite = SpellBarFrames[1];
        //spawn the current weapon
        createWeapon();
    }
    private void changeWeapons()
    {
        //change the current weapon
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            weaponSlot += 1;
            if (weaponSlot > weapons.Count - 1)
            {
                weaponSlot = 0;
            }
            SpellBar.sprite = SpellBarFrames[weaponSlot+1];
            currentWeaponLevel = weaponLevels[weaponSlot];
            currentWeapon = weapons[weaponSlot];
            swapCurrentWeapon();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            weaponSlot -= 1;
            if (weaponSlot < 0)
            {
                weaponSlot = weapons.Count - 1;
            }
            SpellBar.sprite = SpellBarFrames[weaponSlot+1];
            currentWeaponLevel = weaponLevels[weaponSlot];
            currentWeapon = weapons[weaponSlot];
            swapCurrentWeapon();
        }
    }
    public void updateSpellUI()
    {
        //update the weapon UI
        weapons = GameManager.instance.weaponsOwned;
        weapons.Clear();
        for (int i = 0; i < weapons.Count; i++)
        {
            SpriteUIImages[i].enabled = true;
            if (weapons[i].GetComponent<Weapon>().name == "Sword")
                SpriteUIImages[i].sprite = WeaponUISprites[0];
            else if (weapons[i].GetComponent<Weapon>().name == "Spear")
            {
                SpriteUIImages[i].sprite = WeaponUISprites[1];
                SpriteUIImages[i].transform.localScale = new Vector3(1, 1, 1);
            }
            else if (weapons[i].GetComponent<Weapon>().name == "Mace")
            {
                SpriteUIImages[i].sprite = WeaponUISprites[2];
                SpriteUIImages[i].transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                SpriteUIImages[i].transform.localScale = new Vector3(1, 1, 1);
                SpriteUIImages[i].sprite = null;
                SpriteUIImages[i].enabled = false;
            }
        }
    }
    private void swapCurrentWeapon() {
        //change the current weapon being used 
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).GetComponent<Weapon>().destroyWeapon();
        if (currentWeapon.GetComponent<Weapon>().name == "Spear")
        {
            GameObject playerWeapon = (GameObject)Instantiate(currentWeapon, transform.position, transform.rotation);
            playerWeapon.transform.parent = gameObject.transform;
            GameManager.instance.playerWeapon = currentWeapon;
            playerWeapon.GetComponent<Weapon>().level = weaponLevels[weaponSlot];
        }
        else {
            
            GameObject playerWeapon = (GameObject)Instantiate(currentWeapon, transform.position, Quaternion.identity);
            playerWeapon.transform.parent = gameObject.transform;
            GameManager.instance.playerWeapon = currentWeapon;
            playerWeapon.GetComponent<Weapon>().level = weaponLevels[weaponSlot];
        }
    }
    public void createWeapon() {
        //spawn the weapon
        weaponSlot = 0;
        SpellBar.sprite = SpellBarFrames[weaponSlot + 1];
        GameObject playerWeapon = (GameObject)Instantiate(currentWeapon, transform.position, transform.rotation);
        playerWeapon.GetComponent<Weapon>().level = currentWeaponLevel;
        playerWeapon.transform.parent = gameObject.transform;
        GameManager.instance.playerWeapon = currentWeapon;
        swapCurrentWeapon();
    }
    // Update is called once per frame
    void Update()
    {
        //mange mana consumtion
        if (mana < maxMana)
            mana += manaRegenRate * Time.deltaTime;
        if (mana > maxMana)
            mana = maxMana;
        changeWeapons();
        //when attack two is pressed use your special
        if (Input.GetButtonDown("attack2") && mana >= 30)
        {
            mana -= 30;
            Special();
        }
        //while the speed ability is active make the player move faster
        if (Speed) {
            speedTimer -= Time.deltaTime;
            moveScript.speed = 5;
            if (speedTimer <= 0)
            {
                Speed = false;
                moveScript.speed = 3;
            }
        }
        GameManager.instance.playerMana = mana;
    }
    private void Special() {
        //activate extra speed
        speedTimer = 1;
        Speed = true;
    }
}
