using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//an animation is set to call this scripts eggBreak function when it ends
public class SpiderEggBreak : MonoBehaviour
{
    //declare variables
    public GameObject babySpider;
    public GameObject enemyContainer;
    private GameObject player;
    public GameObject ItemContainer;
    
    public void eggBreak(){
        //create a babyspider at this position and set its parent and itemcontainer then destroy this object
        GameObject e = (GameObject)Instantiate(babySpider, gameObject.transform.position, Quaternion.identity);
        e.transform.parent = enemyContainer.transform;
        e.GetComponent<EnemyHealth>().ItemContainer = ItemContainer;
        Destroy(gameObject);
    }
}
