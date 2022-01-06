using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//attacking script used for the necromancer
public class Summon : MonoBehaviour
{
    //declare variables
    public GameObject[] summons;
    public float[] summonChance;
    private FollowPlayer ai;
    private Animator anim;

    private float halfHeight;
    private float halfWidth;

    public float cooldown = 5;
    private float attackCooldown = 5;
    private GameObject player;
    private GameManager game;

    public GameObject enemyContainer;
    void Start()
    {
        //grab game manager and the camera 
        game = GameManager.instance;
        Camera camera = Camera.main;
        //set the half width and height for spawning the enemies
        halfHeight = camera.orthographicSize;
        halfWidth = camera.aspect * halfHeight;
        //initilize other variables
        ai = GetComponent<FollowPlayer>();
        anim = GetComponent<Animator>();
        attackCooldown = cooldown;
        player = game.player;
    }
    void Update()
    {
        //attack when stopped not attacking and whenn cooldown is finished and tick down the cooldown timer
        if (ai.stopped && !ai.attacking && attackCooldown <= 0)
        {
            ai.attacking = true;
            anim.SetTrigger("attack");
        }
        attackCooldown -= Time.deltaTime;

    }
    public void summon()
    {
        //summon three enemies
        for (int i = 0; i < 3; i++)
        {
            //get the players position choose a random direction and set the spawn position based on the direction and playerposition
            Vector3 spawnPosition = new Vector3(player.transform.position.x, player.transform.position.y, 0);
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
            //given a set number of enemies select a random one and that random one is based of chance values assigned to the necromancer through the editor
            int rangeNum = Random.Range(0, 100);
            GameObject enemy = summons[0];
            float randomVal = Random.Range(0, 100);

            for (int j = 0; j < summons.Length; j++)
            {
                if (randomVal <= ((summonChance[j] * 100) - 1))
                {
                    enemy = summons[j];
                    break;
                }
            }
            //create the enemy and check wheather or not to spawn it based on its position relative to the navmesh
            GameObject e = (GameObject)Instantiate(enemy, spawnPosition, Quaternion.identity);
            NavMeshAgent agent = e.GetComponent<NavMeshAgent>();
            if (!agent.isOnNavMesh)
            {
                Destroy(e);
            }
            else
            {
                //set the enemies target and its itemcontainer is set to itself so it will not spawn and drops
                e.GetComponent<FollowPlayer>().target = player;
                e.GetComponent<EnemyHealth>().ItemContainer = e;
                //reparent the enemy to the enemy container
                e.transform.parent = transform.parent.transform;
            }
        }
    //after done attacking reset cooldown and attacking status
    attackCooldown = cooldown;
    ai.attacking = false;
    }
}
