using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarriorWeapons : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = GameManager.instance.playerWeapon;
        weaponLevels = GameManager.instance.weaponLevels;
        weapons = GameManager.instance.weaponsOwned;

        SpellBar = GameManager.instance.spellBar;
        SpriteUIImages = GameManager.instance.spriteUIImages;
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
        createWeapon();
    }
    private void changeWeapons()
    {
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
        if (currentWeapon.GetComponent<Weapon>().name == "Spear")
        {
            print("HELO");
            transform.GetChild(0).GetComponent<Weapon>().destroyWeapon();
            GameObject playerWeapon = (GameObject)Instantiate(currentWeapon, transform.position, transform.rotation);
            playerWeapon.GetComponent<Weapon>().level = currentWeaponLevel;
            playerWeapon.transform.parent = gameObject.transform;
            GameManager.instance.playerWeapon = currentWeapon;
        }
        else {
            transform.GetChild(0).GetComponent<Weapon>().destroyWeapon();
            GameObject playerWeapon = (GameObject)Instantiate(currentWeapon, transform.position, Quaternion.identity);
            playerWeapon.GetComponent<Weapon>().level = currentWeaponLevel;
            playerWeapon.transform.parent = gameObject.transform;
            GameManager.instance.playerWeapon = currentWeapon;
        }
    }
    public void createWeapon() {
        weaponSlot = 0;
        SpellBar.sprite = SpellBarFrames[weaponSlot + 1];
        GameObject playerWeapon = (GameObject)Instantiate(currentWeapon, transform.position, transform.rotation);
        playerWeapon.GetComponent<Weapon>().level = currentWeaponLevel;
        playerWeapon.transform.parent = gameObject.transform;
        GameManager.instance.playerWeapon = currentWeapon;
    }
    // Update is called once per frame
    void Update()
    {
        changeWeapons();
    }
}
