using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NDB_Behavior : MonoBehaviour
{
    [SerializeField] GameObject storyMissionPanel;
    public int food = 1000;
    public int gold = 1000;
    public int iron = 400;
    public int wood = 1000;
    public int curr_day = 1;

    public int foodUpkeep = 10;
    public int goldUpkeep = 10;

    public GameObject goldTotal;
    public GameObject foodTotal;
    public GameObject ironTotal;
    public GameObject woodTotal;
    public GameObject goldDef;
    public GameObject foodDef;
    public GameObject dayNum;

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

    public float timeRemaining = 10;
    public int maximum = 100;
    public float current;
    public Image mask;

    public List<GameObject> infirmList;
    public List<GameObject> trainList;
    public List<GameObject> holdIndex;

    public bool farmActive = false;
    public bool mineActive = false;
    public bool lumberyardActive = false;

    public int farmlvl = 0;
    public int minelvl = 0;
    public int lumberyardlvl = 0;



    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            current = timeRemaining * 10;
            float fillAmount = (float)current / (float)maximum;
            mask.fillAmount = fillAmount;
        }
        else
        {
            timeRemaining = 10;
            NDB_press();
        }
    }

    public void upkeepUpdate()
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
        //story mission popup
        if ((curr_day+1) % 10 == 0)
        {
            storyMissionPanel.SetActive(true);
        }
        //decrement daysBusy for mercs
        List<GameObject> mercs = GameObject.Find("MercContainer").GetComponent<mercCont>().mercList;
        foreach (GameObject merc in mercs)
        {
            if (merc.GetComponent<Merc>().daysBusy == 1)
            {
                merc.GetComponent<Merc>().daysBusy = 0;
                merc.GetComponent<Merc>().isBusy = false;
            } else if (merc.GetComponent<Merc>().daysBusy > 1)
            {
                merc.GetComponent<Merc>().daysBusy -= 1;
            }
        }

        //apply upkeep
        food -= foodUpkeep;
        gold -= goldUpkeep;
        goldTotal.GetComponent<Text>().text = gold.ToString();
        foodTotal.GetComponent<Text>().text = food.ToString();


        goldDef.GetComponent<Text>().text = "(-" + goldUpkeep.ToString() + ")";
        foodDef.GetComponent<Text>().text = "(-" + foodUpkeep.ToString() + ")";
    }

    public void NDB_press()
    {
        upkeepUpdate();
        curr_day++;
        dayNum.GetComponent<Text>().text = curr_day.ToString();

        //trait events
        int eventFire = Random.Range(0, 10);
        if(eventFire % 2 == 1)
        {
            int eventNum = 4;
            //int eventNum = Random.Range(0, 3);

            //aggressive event. Trait 1
            if (eventNum == 1)
            {
                aggressiveEvent();
            }
            if (eventNum == 2)
            {
                foodSpoilage();
            }

            // theft event. option 4
            if (eventNum == 4)
            {
                theftEvent();
            }
        }

        if (infirmList.Count != 0)
        {
            //List<GameObject> holdIndex = new List<GameObject>();
            for (int i = 0; i < infirmList.Count; i++)
            {
                infirmList[i].GetComponent<Merc>().daysBusy -= 1;

                if (infirmList[i].GetComponent<Merc>().daysBusy == 0)
                {
                    infirmList[i].GetComponent<Merc>().isBusy = false;
                    infirmList[i].GetComponent<Merc>().currHP = infirmList[i].GetComponent<Merc>().maxHP;
                    holdIndex.Add(infirmList[i]);
                }
            }


            foreach (GameObject merc in holdIndex)
            {
                Debug.Log(merc.GetComponent<Merc>().mercName + " was removed from the infirmary");
                infirmList.Remove(merc);
            }

            holdIndex.Clear();
        }

        if (trainList.Count != 0)
        {
            foreach (GameObject merc in trainList)
            {
                merc.GetComponent<Merc>().daysBusy -= 1;

                if (merc.GetComponent<Merc>().daysBusy == 0)
                {
                    merc.GetComponent<Merc>().isBusy = false;
                    holdIndex.Add(merc);
                    int r = randInt(5);
                    int statID = randInt(3);
                    //Debug.Log("r: " + r);
                    //Debug.Log("statID: " + statID);

                    switch (statID)
                    {
                        case 1: // HP
                            if (merc.GetComponent<Merc>().maxHP == merc.GetComponent<Merc>().currHP)
                            {
                                merc.GetComponent<Merc>().maxHP += r;
                                merc.GetComponent<Merc>().currHP += r;
                            } else
                            {
                                merc.GetComponent<Merc>().maxHP += r;
                            }
                            break;
                        case 2: // Attack
                            merc.GetComponent<Merc>().strength += r;
                            break;
                        case 3: // Defense
                            merc.GetComponent<Merc>().armorSkill += r;
                            break;
                    }
                }
            }
            foreach (GameObject merc in holdIndex)
            {
                Debug.Log(merc.GetComponent<Merc>().mercName + " has finished training");
                trainList.Remove(merc);
            }

            holdIndex.Clear();

        }

        if (farmActive)
        {
            food += (150 + (farmlvl * 50));
            foodTotal.GetComponent<Text>().text = food.ToString();
        }

        if (mineActive)
        {
            iron += (20 + (minelvl * 5));
            ironTotal.GetComponent<Text>().text = iron.ToString();
        }

        if (lumberyardActive)
        {
            wood += (30 + (lumberyardlvl * 10));
            woodTotal.GetComponent<Text>().text = wood.ToString();
        }

        

        // daily health regen
        foreach (GameObject merc in MercList.GetComponent<mercCont>().mercList)
        {
            //make sure merc is alive
            if (merc.GetComponent<Merc>().currHP > 0)
            {

                if (merc.GetComponent<Merc>().currHP < merc.GetComponent<Merc>().maxHP)
                {
                    //if health difference too small just full heal
                    if (merc.GetComponent<Merc>().maxHP - merc.GetComponent<Merc>().currHP < 3)
                    {
                        merc.GetComponent<Merc>().currHP = merc.GetComponent<Merc>().maxHP;
                    }
                    else
                    {
                        //increase current health by random
                        merc.GetComponent<Merc>().currHP += Random.Range(1, 3);
                    }
                }

                //make sure they do not have more than max health
                if (merc.GetComponent<Merc>().currHP > merc.GetComponent<Merc>().maxHP)
                {
                    merc.GetComponent<Merc>().currHP = merc.GetComponent<Merc>().maxHP;
                }
            }
        }
    }
    
    public int randInt(int lim)
    {
        lim += 1;
        return Random.Range(1, lim);
    }

    public void conflictEventButton1()
    {

        

        //aggressive event
        if (tempListVar == 1)
        {
            gold -= 25;
            goldTotal.GetComponent<Text>().text = gold.ToString();
        }
        //food spoil
        if (tempListVar == 2)
        {
            gold -= 100;
            goldTotal.GetComponent<Text>().text = gold.ToString();
        }

        //theft event
        if (tempListVar == 4)
        {
            if (MercList.GetComponent<mercCont>().mercList.Count != 0)
            {

                for (int i = 0; i < MercList.GetComponent<mercCont>().mercList.Count; i++)
                {
                    if (MercList.GetComponent<mercCont>().mercList[i] == option1Merc)
                    {
                        MercList.GetComponent<mercCont>().mercList.Remove(option1Merc);
                    }
                }
            }
        }
    }

    public void conflictEventButton2()
    {
        

        //aggressive event
        if (tempListVar == 1)
        {
            option2Merc.GetComponent<Merc>().morale -= 10;
        }

        //food spoil
        if (tempListVar == 2)
        {
            food -= 100;
            foodTotal.GetComponent<Text>().text = food.ToString();
        }

        //theft event
        if (tempListVar == 4)
        {
            if (MercList.GetComponent<mercCont>().mercList.Count != 0)
            {
                foreach (GameObject merc in MercList.GetComponent<mercCont>().mercList)
                {
                    merc.GetComponent<Merc>().morale -= 2;
                }
            }
        }
    }

    public void aggressiveEvent()
    {
        //make sure list for mercs with traits is clear
        if (traitList.Count != 0)
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
        if (traitList.Count != 0)
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
            eventText.GetComponent<Text>().text = eventTriggerMerc.GetComponent<Merc>().mercName + " has recently been fighting with " + eventTriggerRecip.GetComponent<Merc>().mercName + ". We need to do something about this. What are your orders.";
            option1.GetComponentInChildren<Text>().text = "Give " + eventTriggerRecip.GetComponent<Merc>().mercName + " some gold to make him happy";
            option2.GetComponentInChildren<Text>().text = "Tell them both to knock it off and stop being childish";

        }
    }

    public void theftEvent()
    {
        traitList.Clear();
        if (MercList.GetComponent<mercCont>().mercList.Count != 0)
        {

            if (traitList.Count != 0)
            {
                traitList.Clear();
            }

            //4 checks for greedy trait
            tempListVar = 4;
            foreach (GameObject merc in MercList.GetComponent<mercCont>().mercList)
            {
                if (merc.GetComponent<Merc>().trait == tempListVar)
                {
                    traitList.Add(merc);
                }
            }

            if (traitList.Count != 0)
            {
                eventButtonTrigger.SetActive(true);

                //get triggering merc and victim
                GameObject eventTriggerMerc = traitList[Random.Range(0, traitList.Count - 1)];

                //make sure same merc is not trigger and victim
                if (eventTriggerMerc.GetComponent<Merc>().mercName == eventTriggerMerc.GetComponent<Merc>().mercName)
                {
                    eventTriggerMerc = traitList[Random.Range(0, traitList.Count - 1)];
                }

                option1Merc = eventTriggerMerc;

                eventText.GetComponent<Text>().text = "It has come to our attention that " + eventTriggerMerc.GetComponent<Merc>().mercName + " has been stealing. He must be dealth with. What are your orders.";
                option1.GetComponentInChildren<Text>().text = "He dares to steal from us? Fire him";
                option2.GetComponentInChildren<Text>().text = "Ignore it for now, we need him";
            }
        }
    }

    public void foodSpoilage()
    {
        tempListVar = 2;
        eventButtonTrigger.SetActive(true);
        eventText.GetComponent<Text>().text = "Some of our food has gone bad. We must rectify this problem";
        option1.GetComponentInChildren<Text>().text = "Buy more food";
        option2.GetComponentInChildren<Text>().text = "We can't spare any gold right now";
    }

}
