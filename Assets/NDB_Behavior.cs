using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NDB_Behavior : MonoBehaviour
{
    public int food = 1000;
    public int gold = 1000;
    public int iron = 400;

    public int foodUpkeep = 10;
    public int goldUpkeep = 10;

    public GameObject goldTotal;
    public GameObject foodTotal;
    public GameObject goldDef;
    public GameObject foodDef;


    public GameObject MercList;

    public void NDB_press()
    {
        foodUpkeep = 10;
        goldUpkeep = 10;

        if (MercList.GetComponent<mercCont>().mercList != null)
        {
            foreach (GameObject merc in MercList.GetComponent<mercCont>().mercList)
            {
                foodUpkeep += merc.GetComponent<Merc>().foodCost;
                goldUpkeep += merc.GetComponent<Merc>().goldCost;
            }
        }

        //mission progresses each day
        GameObject missionContainer = GameObject.Find("MissionContainer");
        if (missionContainer != null)
        {
            missionContainer.GetComponent<MissionContainerScript>().nextDay();
            //missionContainer.GetComponent<MissionContainerScript>().populateViewPort();
        }
        GameObject missionPanel = GameObject.Find("MissionPanel");
        if (missionPanel != null)
        {
            missionPanel.GetComponent<missionGenerator>().generateMission();
        }
        

        food -= foodUpkeep;
        gold -= goldUpkeep;
        goldTotal.GetComponent<Text>().text = gold.ToString();
        foodTotal.GetComponent<Text>().text = food.ToString();


        goldDef.GetComponent<Text>().text = "(-" + goldUpkeep.ToString() + ")"; 
        foodDef.GetComponent<Text>().text = "(-" + foodUpkeep.ToString() + ")";
    }
}
