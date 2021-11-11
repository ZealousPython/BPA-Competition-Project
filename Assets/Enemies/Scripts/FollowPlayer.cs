using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FollowPlayer : MonoBehaviour
{
    public GameObject target = null;
    private NavMeshAgent agent;
    private Animator anim;
    public bool attacking = false;
    public bool stopped = false;
    private GameManager game;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        game = GameManager.instance;

    }
    // Update is called once per frame
    void Update()
    {
        if (target == null) {
            target = game.player;
        }
        if (!attacking)
        {
            agent.SetDestination(target.transform.position);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        if (agent.remainingDistance <= agent.stoppingDistance) {
            stopped = true;
        }
        else
            stopped = false;
        if (!attacking && !stopped)
            anim.SetBool("moving", true);
        else if (!attacking)
            anim.SetBool("moving", false);
        float angle = Mathf.Atan2(target.transform.position.y-transform.position.y, target.transform.position.x-transform.position.x) * Mathf.Rad2Deg+90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    
}
