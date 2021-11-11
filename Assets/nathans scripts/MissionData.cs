using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionData : MonoBehaviour
{

    [SerializeField] GameObject completionPanel;
    [SerializeField] Text completionText;

    //storing missions data
    public List<Dictionary<string, string>> missionsInProgress;
    public List<Dictionary<string, string>> availableMissions;

    public int day;
    public int getDay()
    {
        return day;
    }
    public List<Dictionary<string, string>> getAvailableMissions()
    {
        return availableMissions;
    }
    public List<Dictionary<string, string>> getMissionsInProgress()
    {
        return missionsInProgress;
    }
    // Start is called before the first frame update
    void Start()
    {
        missionsInProgress = new List<Dictionary<string, string>>();
        availableMissions = new List<Dictionary<string, string>>();
        day = 0;
        generateMissions();
    }
    public void incDay() //inc day and modify missions information/progress missions
    {
        day += 1; //keep track of day to know when to redisp data on missionscript
        nextDay();//reduces length of missions in prog
        generateMissions(); //creates new missions that can be taken
    }
    public void generateMissions()
    {
        availableMissions = new List<Dictionary<string, string>>();
        //mission
        for (int i = 0; i < 3; i++)
        {
            //create mission information into a dictionary
            Dictionary<string, string> newMission = new Dictionary<string, string>();
            //create mission gameobject with dictionary of values
            newMission.Add("work units", Random.Range(1, 4).ToString());
            newMission.Add("length", Random.Range(1, 4).ToString());
            newMission.Add("reward", Random.Range(75, 200).ToString());
            //append mission to container
            availableMissions.Add(newMission);
        }
    }
    public void nextDay() //mission progresses
    {
        if (missionsInProgress.Count > 0)
        {
            //decrement lengths of missions
            List<Dictionary<string, string>> listCopy = new List<Dictionary<string, string>>(missionsInProgress);
            List<Dictionary<string, string>> updatedMissions = new List<Dictionary<string, string>>();
            foreach (Dictionary<string, string> dict in listCopy)
            {
                if (int.Parse(dict["length"]) > 1)
                {
                    int i = listCopy.IndexOf(dict);
                    missionsInProgress[i]["length"] = (int.Parse(dict["length"]) - 1).ToString();
                    updatedMissions.Add(missionsInProgress[i]);
                }
                else
                {
                    
                    completionPanel.SetActive(true);
                    completionText.text += "Mission Completed:\n Reward: "+dict["reward"]+"\n\n";

                    
                }

            } //update inprogress missions
            missionsInProgress = new List<Dictionary<string, string>>(updatedMissions);
        }
        

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
