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
    public Sprite gt = null;
    public Sprite mh = null;
    public List<GameObject> old;
    public int cost = 0;
    public List<GameObject> mercInf;
    public GameObject ug = null;

    public int fooodUpCost = 0;
    public int lumberUpCost = 0;
    public int mineUpCost = 0;
    public bool upgradeActive = false;

    public List<GameObject> mL;
    public GameObject mercList = null;
    public GameObject panel = null;

    public void getActive(Button b)
    {
        Hide();
        activebutton = b;
    }

    public void changeGridID(int ID)
    {
        activebutton.GetComponent<Building_data>().ID = ID;
        switch (ID)
        {
            
            case 1: //Mess Hall
                    // activebutton.GetComponent<Image>().color = Color.red;
                activebutton.GetComponent<Image>().sprite = mh;
                activebutton.GetComponentInChildren<Text>().text = "Mess Hall";
                buildingCost(50);
                break;
            case 2: //Training Area
                //activebutton.GetComponent<Image>().color = Color.green;
                activebutton.GetComponent<Image>().sprite = gt;
                activebutton.GetComponentInChildren<Text>().text = "Training Area";
                buildingCost(150);
                break;
            case 3: //Blacksmith
                //activebutton.GetComponent<Image>().color = Color.blue;
                activebutton.GetComponent<Image>().sprite = gt;
                activebutton.GetComponentInChildren<Text>().text = "Blacksmith";
                buildingCost(100, 50);
                break;
            case 4: // Infirmary
                //activebutton.GetComponent<Image>().color = Color.yellow;
                activebutton.GetComponent<Image>().sprite = gt;
                activebutton.GetComponentInChildren<Text>().text = "Infirmary";
                buildingCost(100);
                break;
            case 5: //Farm
                activebutton.GetComponent<Image>().sprite = gt;
                activebutton.GetComponentInChildren<Text>().text = "Farm";
                resource.GetComponent<NDB_Behavior>().farmActive = true;
                //resource.GetComponent<NDB_Behavior>().farmlvl = 1;
                buildingCost(100, 0, 50);
                break;
            case 6: //Lumber yard
                activebutton.GetComponent<Image>().sprite = gt;
                activebutton.GetComponentInChildren<Text>().text = "Lumber yard";
                resource.GetComponent<NDB_Behavior>().lumberyardActive = true;
                //resource.GetComponent<NDB_Behavior>().lumberyardlvl = 1;
                buildingCost(150, 50);
                break;
            case 7: //Mine
                activebutton.GetComponent<Image>().sprite = gt;
                activebutton.GetComponentInChildren<Text>().text = "Mine";
                resource.GetComponent<NDB_Behavior>().mineActive = true;
                //resource.GetComponent<NDB_Behavior>().minelvl = 1;
                buildingCost(100, 50);
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
                string mh = "Select a soldier to restore morale";
                indivBuildingMenu(0, mh);
                break;
            case 2: // Training Area
                string ta = "Select a soldier to start training";
                hideMenu();
                indivBuildingMenu(2, ta);
                break;
            case 3: // Blacksmith
                hideMenu();
                bs.SetActive(true);
                break;
            case 4: // Infirmary
                string im = "select a soldier to heal";
                indivBuildingMenu(1, im);
                break;
            case 5: //Farm
                if (upgradeActive)
                {
                    showUpgradeMenu(1, "Farm");
                }
                
                break;
            case 6: //Lumber yard
                if (upgradeActive)
                {
                    showUpgradeMenu(2, "Lumber Yard");
                }
                break;
            case 7: //Mine
                if (upgradeActive)
                {
                    showUpgradeMenu(3, "Mine");
                }
                break;
        }
    }

    public void hideMenu()
    {
        menu.SetActive(false);
    }

    public void showUpgradeMenu(int ID, string txt)
    {
        hideMenu();
        ug.SetActive(true);
        GameObject bn = GameObject.Find("buildingName");
        bn.GetComponent<Text>().text = txt;

        switch (ID)
        {
            case 1: //Farm
                Debug.Log("Farm");
                break;
            case 2: //Lumber yard
                Debug.Log("Lumber");
                break;
            case 3: //Mine
                Debug.Log("Mine");
                break;
        }
    }

    public void indivBuildingMenu(int ID, string info)
    {
        hideMenu();
        mL = mercList.GetComponent<mercCont>().mercList;
        messMenu.SetActive(true);
        GameObject ob = GameObject.Find("BuildingInfo");
        ob.GetComponentInChildren<Text>().text = info;
        makeMessMenu("MessHallListButton", ID);
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
    public void makeMessMenu(string ob, int func)
    {
        foreach (GameObject merc in mL)
        {
            if (merc.GetComponent<Merc>().isBusy)
            {
                continue;
            }
            GameObject buttonPrefab = GameObject.Find(ob);
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.transform.parent = panel.transform;
            button.GetComponentInChildren<Text>().text = merc.GetComponent<Merc>().mercName;
            old.Add(button);
            //button.GetComponent<Button>().onClick.AddListener(delegate { addMorale(merc); });

            switch (func)
            {
                case 0: // Mess Hall
                    button.GetComponent<Button>().onClick.AddListener(delegate { addMorale(merc); });
                    break;
                case 1: // Infirmary
                    button.GetComponent<Button>().onClick.AddListener(delegate { addMercToInf(merc); });
                    break;
                case 2: // Training Hall
                    button.GetComponent<Button>().onClick.AddListener(delegate { addTraining(merc); });
                    break;
            }

        }
    }

    public void addTraining(GameObject m)
    {
        m.GetComponent<Merc>().daysBusy = 3;
        m.GetComponent<Merc>().isBusy = true;
        resource.GetComponent<NDB_Behavior>().trainList.Add(m);
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

    public void buildingCost(int wood, int iron=0, int food=0)
    {
        //Debug.Log(resource.GetComponent<NDB_Behavior>().iron);
        resource.GetComponent<NDB_Behavior>().wood -= wood;
        resource.GetComponent<NDB_Behavior>().iron -= iron;
        resource.GetComponent<NDB_Behavior>().food -= food;
        GameObject ir = GameObject.Find("IronTotal");
        GameObject wo = GameObject.Find("WoodTotal");
        GameObject fo = GameObject.Find("FoodTotal");
        ir.GetComponent<Text>().text = resource.GetComponent<NDB_Behavior>().iron.ToString();
        wo.GetComponent<Text>().text = resource.GetComponent<NDB_Behavior>().wood.ToString();
        fo.GetComponent<Text>().text = resource.GetComponent<NDB_Behavior>().food.ToString();
    }
}
