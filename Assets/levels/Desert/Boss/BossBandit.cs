using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
//code used for the second boss of the game the bandit
public class BossBandit : MonoBehaviour
{
    //Declare Variables
    public GameObject target;
    private NavMeshAgent agent;
    private Animator anim;
    public bool attacking = false;
    public GameObject Bullet;
    public GameObject wolf;
    public bool stopped = false;
    private GameManager game;
    public Image healthBar;


    private float halfHeight;
    private float halfWidth;
    public GameObject enemyContainer;


    public float summonCooldown = 5;
    public float shootCooldown = 5;
    public float attackCooldown = 0;

    private EnemyHealth healthScript;
    private float health = 30;
    public float maxHealth = 30;

    private bool dying = false;
    public GameObject ItemContainer;
    void Start()
    {
        //set up the camera and variables to allow the boss to spawn enemies as an attack
        Camera camera = Camera.main;
        halfHeight = camera.orthographicSize;
        halfWidth = camera.aspect * halfHeight;
        //initilize other variables
        healthScript = GetComponent<EnemyHealth>();
        game = GameManager.instance;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        health = maxHealth;
        healthScript.health = maxHealth;
    }
    void Update()
    {
        //choose wich attack to use and set attack cooldown
        if (stopped && !attacking && attackCooldown <= 0)
        {
            attacking = true;
            int attackChosen = Random.Range(0, 2);
            if (attackChosen == 0)
                anim.SetTrigger("attackOne");
            if (attackChosen == 1)
                anim.SetTrigger("attackTwo");
        }
        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;
        //Following Player and check if boss is in range to attack
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
        //managing moving animations and rotate towards player
        if (!attacking && !stopped)
            anim.SetBool("moving", true);
        else if (!attacking)
            anim.SetBool("moving", false);
        float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //manage the bosses health and update the health bar
        health = healthScript.health;
        game.bossHealth = health;
        healthBar.fillAmount = healthScript.health / maxHealth;
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    public void Summon()
    {
        //summon four different wolves 
        for (int i = 0; i < 4; i++)
        {
            Vector3 spawnPosition = new Vector3(target.transform.position.x, target.transform.position.y, 0);
            int offscreenDirection = Random.Range(0, 4);
            if (offscreenDirection == 0)
            {
                spawnPosition.x += -halfWidth - 1;
                spawnPosition.y += Random.Range(-halfHeight, halfHeight);
            }
            else if (offscreenDirection == 1)
            {
                spawnPosition.x += halfWidth + 1;
                spawnPosition.y += Random.Range(-halfHeight, halfHeight);
            }
            else if (offscreenDirection == 2)
            {
                spawnPosition.y += -halfHeight - 1;
                spawnPosition.x += Random.Range(-halfWidth, halfWidth);
            }
            else if (offscreenDirection == 3)
            {
                spawnPosition.y += halfHeight + 1;
                spawnPosition.x += Random.Range(-halfWidth, halfWidth);
            }

            GameObject e = (GameObject)Instantiate(wolf, spawnPosition, Quaternion.identity);
            NavMeshAgent agent = e.GetComponent<NavMeshAgent>();
            if (!agent.isOnNavMesh)
            {
                Destroy(e);
            }
            else
            {
                e.GetComponent<FollowPlayer>().target = target;
                e.transform.parent = enemyContainer.transform;
                e.GetComponent<EnemyHealth>().ItemContainer = ItemContainer;
            }
            attackCooldown = summonCooldown;
            attacking = false;
        }
    }
    public void ShootALot()
    {
        //shoot three different bullet projectiles towards player
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation = new Vector3(rotation.x, rotation.y, rotation.z - 90);
        Vector3 direction = (target.transform.position - transform.position);
        direction.z = 0.0f;
        Vector3 directionNormalized = direction.normalized;
        GameObject p = (GameObject)Instantiate(Bullet, transform.position, Quaternion.Euler(rotation));
        EnemyProjectile pscript = p.GetComponent<EnemyProjectile>();
        pscript.updateDirection(new Vector2(directionNormalized.x, directionNormalized.y));
        attackCooldown = shootCooldown;
        attacking = false;
    }
    public void hit(float damage)
    {
        //take damage and deal with dying
        health -= damage;
        if (health <= 0)
        {
            dying = true;
        }
    }
}
