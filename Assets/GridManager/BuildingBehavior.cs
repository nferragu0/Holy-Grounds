using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBehavior : MonoBehaviour
{
    public Button activebutton = null;
    public GameObject menu = null;
    public GameObject messMenu = null;
    public GameObject resource = null;
    public GameObject bs = null;
    public List<GameObject> old;
    public int cost = 0;


    //public List<GameObject> deleteList;

    public List<GameObject> mL;
    public GameObject mercList = null;
    public GameObject panel = null;

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
        switch (ID)
        {
            case 1: //Mess Hall
                activebutton.GetComponent<Image>().color = Color.red;
                break;
            case 2: //Training Area
                activebutton.GetComponent<Image>().color = Color.green;
                break;
            case 3: //Blacksmith
                activebutton.GetComponent<Image>().color = Color.blue;
                break;
        }
    }


    public void showBuildMenu()
    {
        int check = -1;
        //Debug.Log(activebutton.GetComponent<Building_data>().ID);
        check = activebutton.GetComponent<Building_data>().ID;
        //Debug.Log(check);

        switch (check)
        {
            case 0:
                menu.SetActive(true);
                break;
            case 1: // Mess Hall
                menu.SetActive(false);
                mL = mercList.GetComponent<mercCont>().mercList;
                messMenu.SetActive(true);
                makeMenu("MessHallListButton");
                break;
            case 2: // Training Area
                menu.SetActive(false);
                //TODO add Training Area Functionality
                break;
            case 3: // Blacksmith
                menu.SetActive(false);
                //TODO add Blacksmith Functionality
                bs.SetActive(true);
                break;
        }
    }

    public void addMorale(GameObject m)
    {

        m.GetComponent<Merc>().morale += 5;
        resource.GetComponent<NDB_Behavior>().food -= 5;
        GameObject ob = GameObject.Find("FoodTotal");
        ob.GetComponent<Text>().text = resource.GetComponent<NDB_Behavior>().food.ToString();

    }

    public void Hide()
    {
        foreach (GameObject m in old)
        {
            Destroy(m);
        }
        messMenu.SetActive(false);
    }
    public void makeMenu(string ob)
    {
        foreach (GameObject merc in mL)
        {
            GameObject buttonPrefab = GameObject.Find(ob);
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.transform.parent = panel.transform;
            button.GetComponentInChildren<Text>().text = merc.GetComponent<Merc>().mercName;
            old.Add(button);
            button.GetComponent<Button>().onClick.AddListener(delegate { addMorale(merc); });

        }
    }

    public void makeEquipment(string name)
    {
        string sub = name.Substring(0,5);
        //Debug.Log(sub);
        GameObject eq = GameObject.Find(name);
        switch (sub)
        {
            case "sword":
                eq.GetComponent<weaponInit>().numInv += 1;
                break;
            case "armor":
                eq.GetComponent<armorInit>().numInv += 1;
                break;
        }
        
        resource.GetComponent<NDB_Behavior>().iron -= cost;
        GameObject ob = GameObject.Find("IronTotal");
        ob.GetComponent<Text>().text = resource.GetComponent<NDB_Behavior>().iron.ToString();
 
    }
    
    public void setCost(int c)
    {
        cost = c;
    }
}
