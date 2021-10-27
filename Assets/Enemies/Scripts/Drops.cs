using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Drops : MonoBehaviour
{
    private GameObject coin;
    private GameObject potion;

    private void Start()
    {
        coin = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Items/Coin.prefab", typeof(GameObject));
    }
    public void drop() {
        int drop = Random.Range(0,100);
        if (drop <= 50)
        {
            GameObject p = (GameObject)Instantiate(coin, transform.position, Quaternion.identity);
        }
        else if (drop <= 60) { 
            //Drop Potion
        }
    }
}
