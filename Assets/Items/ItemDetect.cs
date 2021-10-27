using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetect : MonoBehaviour
{
    private GameManager game;
    public int itemType; //0 coin 1 potion
    void Start()
    {
        game = GameManager.instance;
    }
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Hi");
        if (collision.tag == "Player")
        {
            if (itemType == 0)
                game.gold++;
            else
                game.potions++;
            Destroy(gameObject);
        }
    }
}
