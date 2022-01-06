using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//used on enemies and bosses that need to follow the player
public class FollowPlayer : MonoBehaviour
{
    //declare variables
    public GameObject target = null;
    private NavMeshAgent agent;
    private Animator anim;
    public bool attacking = false;
    public bool stopped = false;
    private GameManager game;

    // Start is called before the first frame update
    void Start()
    {
        //get and set up the agent for pathfinding and get the gamemanager and animator
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        game = GameManager.instance;

    }
    // Update is called once per frame
    void Update()
    {
        //resets the target to prevent an enemy from getting lost
        if (target == null) {
            target = game.player;
        }
        //set the target position for the agent to the targets positoin
        agent.SetDestination(target.transform.position);
        //prevent the enemy from moving in the z axis
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        //check if the agent has stopped moving
        if (agent.remainingDistance <= agent.stoppingDistance) {
            stopped = true;
        }
        else
            stopped = false;
        //set the animator to move or not based on if the enemy is moving or attacking
        if (!attacking && !stopped)
            anim.SetBool("moving", true);
        else if (attacking || stopped)
            anim.SetBool("moving", false);
        //rotate the enemy in the direction of the player
        float angle = Mathf.Atan2(target.transform.position.y-transform.position.y, target.transform.position.x-transform.position.x) * Mathf.Rad2Deg+90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    
}
