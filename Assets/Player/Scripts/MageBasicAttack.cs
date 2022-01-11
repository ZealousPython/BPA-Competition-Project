using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MageBasicAttack : MonoBehaviour
{

    //Declaring variables
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
    public CastIceSpike iceCast;
    public RockCast rockCast;
    public FireCast fireCast;
    private int spellSelection = 0;
    private List<Spell> spells = new List<Spell>();
    public List<string> currentSpells = new List<string>();
    private Spell currentSpell;
    public Image SpellBar;
    public Sprite[] SpellBarFrames;
    private Vector2[] SpellUIPositions = new Vector2[] {new Vector2(-56,8), new Vector2(-9, 10), new Vector2(34, 11)};
    public Sprite[] SpellUISprites;
    public Image[] SpriteUIImages;
    public bool active = true;
    public int basicAttackLevel = 1;

    // Start is called before the first frame update
    void Start()
    {
        //get current spells from gamemanager and add them to an list
        iceCast = GetComponent<CastIceSpike>();
        rockCast = GetComponent<RockCast>();
        fireCast = GetComponent<FireCast>();
        currentSpells = GameManager.instance.mageSpells;
        for (int i = 0; i < currentSpells.Count; i++) {
            if (currentSpells[i] == "Ice")
                spells.Add(iceCast);
            else if (currentSpells[i] == "Fire")
                spells.Add(fireCast);
            else if (currentSpells[i] == "Rock")
                spells.Add(rockCast);
        }
        //grab mana values from gamemanager
        maxMana = GameManager.instance.playerMaxMana;
        mana = GameManager.instance.playerMana;
        basicAttackLevel = GameManager.instance.MageBasicAttackLevel;
        //grab Image gameobjects from the scene and update the players UI Accordingly
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
    //Update spell UI while the player is instanced used in shops
    public void updateSpellUI() {
        //update the current spell list
        currentSpells = GameManager.instance.mageSpells;
        spells.Clear();
        for (int i = 0; i < currentSpells.Count; i++)
        {

            if (currentSpells[i] == "Ice")
                spells.Add(iceCast);
            else if (currentSpells[i] == "Fire")
                spells.Add(fireCast);
            else if (currentSpells[i] == "Rock")
                spells.Add(rockCast);
        }
        //update the players ui based on current spell list
        for (int i = 0; i < spells.Count; i++)
        {
            SpriteUIImages[i].enabled = true;
            if (spells[i] == fireCast)
                SpriteUIImages[i].sprite = SpellUISprites[2];
            else if (spells[i] == rockCast)
                SpriteUIImages[i].sprite = SpellUISprites[1];
            else if (spells[i] == iceCast)
                SpriteUIImages[i].sprite = SpellUISprites[0];
            else
            {
                SpriteUIImages[i].sprite = null;
                SpriteUIImages[i].enabled = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        if (active)
        {
            //manage mana consumption and other coolowns
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
            //attack if attack button is held down and cooldown is done
            if (Input.GetButton("attack") && currentCoolDown <= 0)
            {
                shoot();
                currentCoolDown = basicAttackCoolDown;
            }
            //make sure the player cannot use spells if they do not have any
            if (spells.Count <= 0)
            {
                SpellBar.sprite = SpellBarFrames[0];
                spellCoolDown = 9999;
                currentSpell = null;
            }
            //if the player uses attack2 use the selected spell
            else if (Input.GetButton("attack2") && spellCoolDown <= 0 && mana > currentSpell.manaUsage && !casting)
            {
                currentSpell.cast();
                spellCoolDown = currentSpell.coolDown;
                mana -= currentSpell.manaUsage;
                manaRegenTimer = manaRegenTime;
            }
            //change spell selection based on scrollwheel inputs
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                spellSelection += 1;
                if (spellSelection > spells.Count - 1)
                {
                    spellSelection = 0;
                }
                SpellBar.sprite = SpellBarFrames[spellSelection + 1];
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                spellSelection -= 1;
                if (spellSelection < 0)
                {
                    spellSelection = spells.Count - 1;
                }
                SpellBar.sprite = SpellBarFrames[spellSelection + 1];
            }
            //change the spell selected and update mana
            if(spells.Count > 0)
                currentSpell = spells[spellSelection];
            GameManager.instance.playerMana = mana;
        }
    }
    private void shoot() {
        //get mouse position and get the distance from the player to that point and normalize it 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - transform.position);
        direction.z = 0.0f;
        Vector3 directionNormalized = direction.normalized;
        //create a Projectile and and set values to it
        GameObject p = (GameObject)Instantiate(basicAttack, transform.position, Quaternion.identity);
        ProjectileMovement pscript = p.GetComponent<ProjectileMovement>();
        pscript.level = basicAttackLevel;
        pscript.updateProjectileLevel();
        pscript.updateDirection(new Vector2(directionNormalized.x,directionNormalized.y));
    }
}
