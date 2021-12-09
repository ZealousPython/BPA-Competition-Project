using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHealth : MonoBehaviour
{
    public float health = 3;
    private GameObject coin;
    private GameObject potion;
    public GameObject ItemContainer;
    public bool isBoss;

    private void Start()
    {
        coin = (GameObject)Resources.Load("Items/Coin.prefab", typeof(GameObject));
        potion = (GameObject)Resources.Load("Items/Potion.prefab", typeof(GameObject));
    }
    public void hit(float damage) {

        health -= damage;
        if (health <= 0 && !isBoss) {
            drop();
            Destroy(gameObject);
        }
    }
    private void drop()
    {
        int drop = Random.Range(0, 100);
        if (drop <= 50)
        {
            GameObject p = (GameObject)Instantiate(coin, transform.position, Quaternion.identity);
            p.transform.parent = ItemContainer.transform;
        }
        else if (drop <= 52)
        {
            GameObject p = (GameObject)Instantiate(potion, transform.position, Quaternion.identity);
            p.transform.parent = ItemContainer.transform;
        }
    }
}
