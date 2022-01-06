using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//put on items to tell them what to do when collided with
public class ItemDetect : MonoBehaviour
{
    //value assigned using the editor
    public int itemType; //0 coin 1 potion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if item collides with player resolve collision based on item type and destroy the gameobject
        if (collision.tag == "Player")
        {
            if (itemType == 0)
                GameManager.instance.gold+= Random.Range(2, 4);
            else
                GameManager.instance.potions++;
            Destroy(gameObject);
        }
    }
}
