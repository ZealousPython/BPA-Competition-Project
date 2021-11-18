using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class DragonScript : MonoBehaviour
{
    private Animator anim;
    public bool attacking = false;
    public GameObject Wind;
    public GameObject Fire;
    public bool stopped = true;
    private GameManager game;
    public Image healthBar;
    

    public float windCooldown = 5;
    public float fireCooldown = 5;
    public float attackCooldown = 0;

    private EnemyHealth healthScript;
    private float health = 15;
    public float maxHealth = 15;
    Quaternion towardsPlayer;
    void Start()
    {

        healthScript = GetComponent<EnemyHealth>();
        game = GameManager.instance;
        anim = GetComponent<Animator>();
        health = maxHealth;
        healthScript.health = maxHealth;
    }
    void Update()
    {
        //attackCoolDown
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


        health = healthScript.health;
        game.bossHealth = health;
        healthBar.fillAmount = healthScript.health / maxHealth;
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void LateUpdate()
    {
        game.bossHealth = health;
        healthBar.fillAmount = healthScript.health / maxHealth;
    }
    public void Fireblast()
    {
        Vector3 newPosition = new Vector3(0, transform.position.y - 5, 0);
        Vector3 rotation = towardsPlayer.eulerAngles;
        Vector3 direction = (game.player.transform.position - newPosition);
        direction.z = 0.0f;
        Vector3 directionNormalized = direction.normalized;
        
        GameObject p = (GameObject)Instantiate(Fire, newPosition, Quaternion.Euler(rotation));
        EnemyProjectile pscript = p.GetComponent<EnemyProjectile>();
        pscript.updateDirection(new Vector2(directionNormalized.x, directionNormalized.y));
        attackCooldown = fireCooldown;
        attacking = false;
    }
    public void WindAttack()
    {
        Vector3 newRotation = towardsPlayer.eulerAngles;
        //rotation = new Vector3(rotation.x, rotation.y, rotation.z);
        newRotation = new Vector3(0, 0, -90);
        //Vector3 direction = (game.player.transform.position - transform.position);
        //direction.z = 0.0f;
        //Vector3 directionNormalized = direction.normalized;

        Vector3 newPosition = new Vector3(0, transform.position.y, 0);

        GameObject p = (GameObject)Instantiate(Wind, newPosition, Quaternion.Euler(newRotation));
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
