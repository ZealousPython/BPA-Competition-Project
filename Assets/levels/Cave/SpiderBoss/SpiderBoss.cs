using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class SpiderBoss : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    private NavMeshAgent agent;
    private Animator anim;
    public bool attacking = false;
    public GameObject webBall;
    public GameObject babyegg;
    public bool stopped = false;
    private GameManager game;
    public Image healthBar;
    public BoxCollider2D hitbox;


    private float halfHeight;
    private float halfWidth;
    public GameObject enemyContainer;


    public float summonCooldown = 5;
    public float shootCooldown = 5;
    public float biteCooldown = 5;
    public float attackCooldown = 0;

    private EnemyHealth healthScript;
    private float health = 30;
    public float maxHealth = 30;

    private bool dying = false;
    void Start()
    {

        Camera camera = Camera.main;
        halfHeight = camera.orthographicSize;
        halfWidth = camera.aspect * halfHeight;
        healthScript = GetComponent<EnemyHealth>();
        game = GameManager.instance;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        health = maxHealth;
        healthScript.health = maxHealth;
        hitbox = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        //attackCoolDown
        if (stopped && !attacking && attackCooldown <= 0)
        {
            attacking = true;
            int attackChosen = Random.Range(0, 3);
            if (attackChosen == 0)
                anim.SetTrigger("attackOne");
            if (attackChosen == 1)
                anim.SetTrigger("attackTwo");
            if (attackChosen == 2)
                anim.SetTrigger("attackThree");
        }
        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;
        //Following Player
        if (!attacking)
        {
            agent.SetDestination(target.transform.position);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            stopped = true;
        }
        else
            stopped = false;
        if (!attacking && !stopped)
            anim.SetBool("moving", true);
        else if (!attacking)
            anim.SetBool("moving", false);
        float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        game.bossHealth = health;
        healthBar.fillAmount = healthScript.health / maxHealth;
        if (dying)
        {
            game.bossHealth = 0;
            healthBar.fillAmount = 0;
            Destroy(gameObject);

        }
    }
    public void egg()

    {
        Vector3 spawnPosition = new Vector3(transform.position.x,transform.position.y - 2,0);
        GameObject e = (GameObject)Instantiate(babyegg, spawnPosition, Quaternion.identity);
        e.GetComponent<FollowPlayer>().target = target;
        e.transform.parent = enemyContainer.transform;
        attackCooldown = summonCooldown;
        attacking = false;

    }
    public void Spit()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation = new Vector3(rotation.x, rotation.y, rotation.z - 90);
        Vector3 direction = (target.transform.position - transform.position);
        direction.z = 0.0f;
        Vector3 directionNormalized = direction.normalized;
        GameObject p = (GameObject)Instantiate(webBall, transform.position, Quaternion.Euler(rotation));
        EnemyProjectile pscript = p.GetComponent<EnemyProjectile>();
        pscript.updateDirection(new Vector2(directionNormalized.x, directionNormalized.y));
        attackCooldown = shootCooldown;
        attacking = false;
    }
    public void attackStart()
    {
        hitbox.enabled = true;
    }
    public void attackEnd()
    {
        hitbox.enabled = false;
        attacking = false;
        attackCooldown = biteCooldown;
    }
    public void hit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            dying = true;
        }
    }
}
