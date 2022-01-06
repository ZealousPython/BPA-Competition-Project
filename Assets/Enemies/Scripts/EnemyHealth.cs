using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//controls enemys health
public class EnemyHealth : MonoBehaviour
{   //initilize variables
    public float health = 3;
    private GameObject coin;
    private GameObject potion;
    public GameObject ItemContainer;
    public bool isBoss;

    private void Start()
    {
        //on start load both the coin and potion prefabs
        coin = (GameObject)Resources.Load("Items/Coin");
        potion = (GameObject)Resources.Load("Items/Potion");
    }
    public void hit(float damage) {
        // when hit subtract health equal to damage and if health is less than zero drop an item and destroy itself
        health -= damage;
        if (health <= 0 && !isBoss) {
            drop();
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
    private void drop()
    {
        //give a 50 percent chance to spawn a coin in its current position and reparents it to a separate gameobject
        int drop = Random.Range(0, 100);
        if (drop <= 50)
        {
            GameObject p = (GameObject)Instantiate(coin, transform.position, Quaternion.identity);
            p.transform.parent = ItemContainer.transform;
        }
        //gives a two percent chance to drop a potion and reparent it to a separet gameobject
        else if (drop <= 52)
        {
            GameObject p = (GameObject)Instantiate(potion, transform.position, Quaternion.identity);
            p.transform.parent = ItemContainer.transform;
        }
    }
}
