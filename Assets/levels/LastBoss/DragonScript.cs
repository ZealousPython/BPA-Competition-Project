using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class DragonScript : MonoBehaviour
{
    //declare variables
    private Animator anim;
    public bool attacking = false;

    public GameObject Wind;
    public GameObject Fire;
    public GameObject rageWind;
    public GameObject rageFire;

    public bool stopped = true;
    private GameManager game;
    public Image healthBar;
    

    public float windCooldown = 5;
    public float fireCooldown = 5;
    public float attackCooldown = 0;

    private EnemyHealth healthScript;
    private float health = 250;
    public float maxHealth = 250;
    Quaternion towardsPlayer;

    bool rageMode = false;
    void Start()
    {
        //grab components and initilize values
        healthScript = GetComponent<EnemyHealth>();
        game = GameManager.instance;
        anim = GetComponent<Animator>();
        health = maxHealth;
        healthScript.health = maxHealth;
    }
    void Update()
    {
        //manage the attackcool down and select between attacks when needed conditions are met
        if (stopped && !attacking && attackCooldown <= 0)
        {
            attacking = true;
            
            
            
            int attackChosen = Random.Range(0, 2);
            if (attackChosen == 0)
                anim.SetTrigger("attackOne");
            else {
                anim.SetTrigger("attackTwo");
            }

        }
        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;
            
        
        

        //get Rotation towards player
        float angle = Mathf.Atan2(game.player.transform.position.y - transform.position.y-5, game.player.transform.position.x - transform.position.x) * Mathf.Rad2Deg + 90;
        towardsPlayer = Quaternion.Euler(new Vector3(0, 0, angle));

        //manage the health of the dragon and determine if the dragon is in rage mode. in rage mode the dragons attacks get stronger
        health = healthScript.health;
        game.bossHealth = health;
        healthBar.fillAmount = healthScript.health / maxHealth;
        if (health <= (maxHealth / 2)) {
            fireCooldown = .5f;
            windCooldown = 3.25f;
            rageMode = true;
        }
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void LateUpdate()
    {
        //update health last to make sure dragon does not die prematurly 
        game.bossHealth = health;
        healthBar.fillAmount = healthScript.health / maxHealth;
    }
    public void Fireblast()
    {
        //spawn a fireball with a rotation and direction based on the players position
        Vector3 newPosition = new Vector3(0, transform.position.y - 5, 0);
        Vector3 rotation = towardsPlayer.eulerAngles;
        Vector3 direction = (game.player.transform.position - newPosition);
        direction.z = 0.0f;
        Vector3 directionNormalized = direction.normalized;
        GameObject p;
        //create a different stronger fireball
        if (rageMode)
            p = (GameObject)Instantiate(rageFire, newPosition, Quaternion.Euler(rotation));
        else
            p = (GameObject)Instantiate(Fire, newPosition, Quaternion.Euler(rotation));

        EnemyProjectile pscript = p.GetComponent<EnemyProjectile>();
        pscript.updateDirection(new Vector2(directionNormalized.x, directionNormalized.y));
        attackCooldown = fireCooldown;
        attacking = false;
    }
    public void WindAttack()
    {
        Vector3 newRotation = towardsPlayer.eulerAngles;
        newRotation = new Vector3(0, 0, -90);
        GameObject p;
        Vector3 newPosition = new Vector3(0, transform.position.y, 0);

        if (rageMode) 
            p = (GameObject)Instantiate(rageWind, newPosition, Quaternion.Euler(newRotation));
        else
            p = (GameObject)Instantiate(Wind, newPosition, Quaternion.Euler(newRotation));

        ProjectileHolder pscript = p.GetComponent<ProjectileHolder>();
        pscript.updateDirection(new Vector2(0,-1));
        attackCooldown = windCooldown;
        attacking = false;
    }
    public void hit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
