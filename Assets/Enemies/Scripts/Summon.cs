using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Summon : MonoBehaviour
{
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
        game = GameManager.instance;
        Camera camera = Camera.main;
        halfHeight = camera.orthographicSize;
        halfWidth = camera.aspect * halfHeight;
        ai = GetComponent<FollowPlayer>();
        anim = GetComponent<Animator>();
        attackCooldown = cooldown;
        player = game.player;
    }
    void Update()
    {
        enemyContainer.transform.rotation = Quaternion.Euler(0.0f, 0.0f, gameObject.transform.rotation.z * -1.0f);
        if (ai.stopped && !ai.attacking && attackCooldown <= 0)
        {
            ai.attacking = true;
            anim.SetTrigger("attack");
        }
        attackCooldown -= Time.deltaTime;

    }
    public void summon()
    {
        for (int i = 0; i < 3; i++)
        {
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
            GameObject e = (GameObject)Instantiate(enemy, spawnPosition, Quaternion.identity);
            NavMeshAgent agent = e.GetComponent<NavMeshAgent>();
            if (!agent.isOnNavMesh)
            {
                Destroy(e);
            }
            else
            {
                e.GetComponent<FollowPlayer>().target = player;
                e.transform.parent = enemyContainer.transform;
            }
        }
    attackCooldown = cooldown;
    ai.attacking = false;
    }
}
