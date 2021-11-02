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

    public GameObject eventButtonTrigger;
    public GameObject eventWindow;
    public GameObject eventText;

    public GameObject option1;
    public GameObject option2;

    private GameObject option1Merc;
    private GameObject option2Merc;
    private int tempListVar;

    public GameObject MercList;


    public List<GameObject> traitList;

    public void NDB_press()
    {

        foodUpkeep = 10;
        goldUpkeep = 10;

        //loop through mercs to get daily upkeep costs
        if (MercList.GetComponent<mercCont>().mercList != null)
        {
            foreach (GameObject merc in MercList.GetComponent<mercCont>().mercList)
            {
                foodUpkeep += merc.GetComponent<Merc>().foodCost;
                goldUpkeep += merc.GetComponent<Merc>().goldCost;
            }
        }

        //keep track of day, inc for mission changes (modify data
        GameObject missionData = GameObject.Find("MissionContainer");
        missionData.GetComponent<MissionData>().incDay();

        
        
        //apply upkeep
        food -= foodUpkeep;
        gold -= goldUpkeep;
        goldTotal.GetComponent<Text>().text = gold.ToString();
        foodTotal.GetComponent<Text>().text = food.ToString();


        goldDef.GetComponent<Text>().text = "(-" + goldUpkeep.ToString() + ")"; 
        foodDef.GetComponent<Text>().text = "(-" + foodUpkeep.ToString() + ")";


        //trait events
        bool fireEvent = true;
        if (fireEvent)
        {
            int eventNum = 1;
            //int eventNum = Random.Range(0, 3);

            //aggressive event. Trait 1
            if (eventNum == 1)
            {
                aggressiveEvent();
            }
        }
    }

    public void conflictEventButton1()
    {

        //aggressive event
        if (tempListVar == 1)
        {
            gold -= 25;
            goldTotal.GetComponent<Text>().text = gold.ToString();
        }
    }

    public void conflictEventButton2()
    {
        //aggressive event
        if (tempListVar == 1)
        {
            option2Merc.GetComponent<Merc>().morale -= 10;
        }
    }

    public void aggressiveEvent()
    {
        //make sure list for mercs with traits is clear
        if (traitList != null)
        {
            traitList.Clear();

        }

        //set var for which trait
        tempListVar = 1;
        //loop through mercs for mercs with trait
        foreach (GameObject merc in MercList.GetComponent<mercCont>().mercList)
        {
            if (merc.GetComponent<Merc>().trait == tempListVar)
            {
                traitList.Add(merc);
            }
        }
        if (traitList != null)
        {
            
            eventButtonTrigger.SetActive(true);

            //get triggering merc and victim
            GameObject eventTriggerMerc = traitList[Random.Range(0, traitList.Count - 1)];
            GameObject eventTriggerRecip = MercList.GetComponent<mercCont>().mercList[Random.Range(0, MercList.GetComponent<mercCont>().mercList.Count - 1)];

            //make sure same merc is not trigger and victim
            if (eventTriggerMerc.GetComponent<Merc>().mercName == eventTriggerRecip.GetComponent<Merc>().mercName)
            {
                eventTriggerRecip = MercList.GetComponent<mercCont>().mercList[Random.Range(0, traitList.Count)];
            }
            //set victim for buttons
            option2Merc = eventTriggerRecip;

            //set text
            eventText.GetComponent<Text>().text = eventTriggerMerc.GetComponent<Merc>().mercName + " has recentley been fighting with " + eventTriggerRecip.GetComponent<Merc>().mercName + ". We need to do something about this. What are your orders.";
            option1.GetComponentInChildren<Text>().text = "Give " + eventTriggerRecip.GetComponent<Merc>().mercName + " some gold to make him happy";
            option2.GetComponentInChildren<Text>().text = "Tell them both to knock it off and stop being childish";

        }
    }
}
