using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBehavior : MonoBehaviour
{
    public Button activebutton = null;

    /*
    void Start()
    {
    }
    */
    public void getActive(Button b)
    {
        //Debug.Log(b.name);
        //b.GetComponent<Building_data>().ID = 5;
        activebutton = b;
        //Debug.Log(activebutton);

    }

    public void changeGridID(int ID)
    {
        activebutton.GetComponent<Building_data>().ID = ID;
    }
}
