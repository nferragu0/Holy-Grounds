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
    public List<GameObject> mercInf;


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
                activebutton.GetComponentInChildren<Text>().text = "Mess Hall";
                break;
            case 2: //Training Area
                activebutton.GetComponent<Image>().color = Color.green;
                activebutton.GetComponentInChildren<Text>().text = "Training Area";
                break;
            case 3: //Blacksmith
                activebutton.GetComponent<Image>().color = Color.blue;
                activebutton.GetComponentInChildren<Text>().text = "Blacksmith";
                break;
            case 4: // Infirmary
                activebutton.GetComponent<Image>().color = Color.yellow;
                activebutton.GetComponentInChildren<Text>().text = "Infirmary";
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
                makeMessMenu("MessHallListButton");
                break;
            case 2: // Training Area
                menu.SetActive(false);
                //TODO add Training Area Functionality
                break;
            case 3: // Blacksmith
                menu.SetActive(false);
                bs.SetActive(true);
                break;
            case 4: // Infirmary
                menu.SetActive(false);
                mL = mercList.GetComponent<mercCont>().mercList;
                messMenu.SetActive(true);
                makeInfMenu("MessHallListButton");
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
    public void makeMessMenu(string ob)
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

    public void makeInfMenu(string ob)
    {
        foreach(GameObject merc in mL)
        {
            GameObject buttonPrefab = GameObject.Find(ob);
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.transform.parent = panel.transform;
            button.GetComponentInChildren<Text>().text = merc.GetComponent<Merc>().mercName;
            old.Add(button);
            button.GetComponent<Button>().onClick.AddListener(delegate { addMercToInf(merc); });

            }
    }

    public void addMercToInf(GameObject m)
    {
        //Debug.Log(m.GetComponent<Merc>().isBusy);
        m.GetComponent<Merc>().isBusy = true;
        double curr = (double)m.GetComponent<Merc>().currHP;
        double ma = (double)m.GetComponent<Merc>().maxHP;
        double calc = ((ma - curr) / 10.0);
        int days = (int)System.Math.Ceiling(calc);

        m.GetComponent<Merc>().daysBusy = days;
        resource.GetComponent<NDB_Behavior>().infirmList.Add(m);
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
