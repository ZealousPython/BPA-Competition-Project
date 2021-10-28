using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class OgreStuff : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;
    private Animator anim;
    public bool attacking = false;
    public GameObject SmashProjectile;
    public GameObject BarfProjectile;
    public bool stopped = false;

    public float barfCooldown = 5;
    public float smashCooldown = 5;
    private float attackCooldown = 0;

    public float health = 15;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void Update()
    {
        //attackCoolDown
        if (stopped && !attacking && attackCooldown <= 0)
        {
            attacking = true;
            int attackChosen = Random.Range(0, 1);
            if (attackChosen == 0)
                anim.SetTrigger("attackOne");
            if (attackChosen == 1)
                anim.SetTrigger("attackTwo");
        }
        if(attackCooldown>0)
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
    }
    public void BarfAttack()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation = new Vector3(rotation.x, rotation.y, rotation.z - 90);
        Vector3 direction = (target.transform.position - transform.position);
        direction.z = 0.0f;
        Vector3 directionNormalized = direction.normalized;
        GameObject p = (GameObject)Instantiate(BarfProjectile, transform.position, Quaternion.Euler(rotation));
        EnemyProjectile pscript = p.GetComponent<EnemyProjectile>();
        pscript.updateDirection(new Vector2(directionNormalized.x, directionNormalized.y));
        attackCooldown = barfCooldown;
        attacking = false;
    }
    public void SmashAttack()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation = new Vector3(rotation.x, rotation.y, rotation.z - 90);
        Vector3 direction = (target.transform.position - transform.position);
        direction.z = 0.0f;
        Vector3 directionNormalized = direction.normalized;
        GameObject p = (GameObject)Instantiate(BarfProjectile, transform.position, Quaternion.Euler(rotation));
        EnemyProjectile pscript = p.GetComponent<EnemyProjectile>();
        pscript.updateDirection(new Vector2(directionNormalized.x, directionNormalized.y));
        attackCooldown = smashCooldown;
        attacking = false;
    }
}
