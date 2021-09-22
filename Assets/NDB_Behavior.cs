using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NDB_Behavior : MonoBehaviour
{
    public int food = 1000;
    public int gold = 1000;
    public void NDB_press()
    {
        food -= 10;
        gold -= 10;
        Debug.Log("Remaining food: " + food);
        Debug.Log("Remaining gold: " + gold);
    }
}
