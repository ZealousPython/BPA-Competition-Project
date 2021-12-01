using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MageBasicAttack : MonoBehaviour
{
    [SerializeField] private float basicAttackCoolDown;

    private float currentCoolDown = 0;
    private float spellCoolDown = 0;
    private bool casting = false;
    private float mana;
    private float maxMana;
    public float manaRegenRate;
    private float manaRegenTimer;
    public float manaRegenTime;
    public GameObject basicAttack;
    private CastIceSpike iceCast;
    private RockCast rockCast;
    private FireCast fireCast;
    private int spellSelection = 0;
    private List<Spell> spells = new List<Spell>();
    public List<string> currentSpells = new List<string>();
    private Spell currentSpell;
    public Image SpellBar;
    public Sprite[] SpellBarFrames;
    private Vector2[] SpellUIPositions = new Vector2[] {new Vector2(-56,8), new Vector2(-9, 10), new Vector2(34, 11)};
    public Sprite[] SpellUISprites;
    public Image[] SpriteUIImages;

    // Start is called before the first frame update
    void Start()
    {
        iceCast = GetComponent<CastIceSpike>();
        rockCast = GetComponent<RockCast>();
        fireCast = GetComponent<FireCast>();
        currentSpells = GameManager.instance.mageSpells;
        for (int i = 0; i < currentSpells.Count; i++) {
            if (currentSpells[i] == "Ice")
                spells[i] = iceCast;
            else if (currentSpells[i] == "Fire")
                spells[i] = fireCast;
            else if (currentSpells[i] == "Rock")
                spells[i] = rockCast;
        }
        maxMana = GameManager.instance.playerMaxMana;
        mana = GameManager.instance.playerMana;
        SpellBar = GameManager.instance.spellBar;
        SpriteUIImages = GameManager.instance.spriteUIImages;
        for (int i = 0; i < spells.Count;i++) {
            SpriteUIImages[i].enabled = true;
            if (spells[i] == fireCast)
                SpriteUIImages[i].sprite = SpellUISprites[2];
            else if (spells[i] == rockCast)
                SpriteUIImages[i].sprite = SpellUISprites[1];
            else if (spells[i] == iceCast)
                SpriteUIImages[i].sprite = SpellUISprites[0];
            else {
                SpriteUIImages[i].sprite = null;
                SpriteUIImages[i].enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentCoolDown > 0)
            currentCoolDown -= Time.deltaTime;
        if (spellCoolDown > 0 && !casting)
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
        if (spells.Count <= 0)
        {
            SpellBar.sprite = SpellBarFrames[0];
            spellCoolDown = 9999;
            currentSpell = null;
        }
        else if (Input.GetMouseButton(1) && spellCoolDown <= 0 && mana > currentSpell.manaUsage && !casting)
        {
                currentSpell.cast();
                spellCoolDown = currentSpell.coolDown;
                mana -= currentSpell.manaUsage;
                manaRegenTimer = manaRegenTime;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            spellSelection += 1;
            if (spellSelection > spells.Count-1)
            {
                spellSelection = 0;
            }
            SpellBar.sprite = SpellBarFrames[spellSelection+1] ;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
            spellSelection -= 1;
            if (spellSelection < 0)
            {
                spellSelection = spells.Count-1;
            }
            SpellBar.sprite = SpellBarFrames[spellSelection + 1];
        }
        currentSpell = spells[spellSelection];
        GameManager.instance.playerMana = mana;
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
