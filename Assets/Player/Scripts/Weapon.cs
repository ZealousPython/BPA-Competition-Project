using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int level = 1;
    public int price = 0;
    public string name = "null";
    public void destroyWeapon()
    {
        Destroy(gameObject);
    }
}
