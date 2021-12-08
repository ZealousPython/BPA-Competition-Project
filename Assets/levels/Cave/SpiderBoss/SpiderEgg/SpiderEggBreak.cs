using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderEggBreak : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject babySpider;
    public GameObject enemyContainer;
    private GameManager game;
    private GameObject player;
    public GameObject ItemContainer;
    void Start()
    {
        game = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void eggBreak(){
        GameObject e = (GameObject)Instantiate(babySpider, gameObject.transform.position, Quaternion.identity);
        NavMeshAgent agent = e.GetComponent<NavMeshAgent>();
        e.GetComponent<FollowPlayer>().target = player;
        e.transform.parent = enemyContainer.transform;
        e.GetComponent<EnemyHealth>().ItemContainer = ItemContainer;
        Destroy(gameObject);
    }
}
