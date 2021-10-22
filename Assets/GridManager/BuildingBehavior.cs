using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBehavior : MonoBehaviour
{
    //public GameObject activebutton;

    public List<ArrayList> Building_list = new List<ArrayList>();

    void Start()
    {
    }

    public void getActive(Button b)
    {
        Debug.Log(b.name);
    }

}
