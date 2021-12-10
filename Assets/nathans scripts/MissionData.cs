using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionData : MonoBehaviour
{

    [SerializeField] GameObject completionPanel;
    [SerializeField] Text completionText;
    [SerializeField] GameObject mercScrollView;
    //storing missions data
    public List<Dictionary<string, string>> missionsInProgress;
    public List<Dictionary<string, string>> availableMissions;
    public Dictionary<string, string> storyMission;

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
        storyMission = new Dictionary<string, string>();
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

            string firstPath = "assets/missionsText.txt";

            string[] lines = System.IO.File.ReadAllLines(firstPath);
            string missionText = lines[Random.Range(0, lines.Length)];
            //create mission information into a dictionary
            Dictionary<string, string> newMission = new Dictionary<string, string>();
            //create mission gameobject with dictionary of values
            newMission.Add("work units", Random.Range(10, 100).ToString());
            newMission.Add("length", Random.Range(1, 4).ToString());
            newMission.Add("reward", Random.Range(100, 300).ToString());
            newMission.Add("missionText", missionText);
            //append mission to container
            availableMissions.Add(newMission);
        }
    }

    public void generateStoryMission()
    {
        
        
        //creates new story mission
        storyMission = new Dictionary<string, string>();
        storyMission.Add("length", "5");
        storyMission.Add("reward", (day * 20).ToString());
        storyMission.Add("work units", (day * 5).ToString());
    }

    public void nextDay() //mission progresses
    {
        if (storyMission.Count != 0)
        {
            if (storyMission["length"] != "0")
            {
                int len = int.Parse(storyMission["length"]);
                storyMission["length"] = (len - 1).ToString();
            }
            else
            {
                //display completed
                completionPanel.SetActive(true);
                completionText.text += "Story Mission Completed:\n Reward: " + storyMission["reward"] + "\n\n";
                GameObject.Find("Resource Manager").GetComponent<NDB_Behavior>().gold += int.Parse(storyMission["reward"]);
                storyMission = new Dictionary<string, string>();
            }
        }
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
                    //mission complete pop up
                    string res = dict["result"];
                    int reward = int.Parse(dict["reward"]);
                    if (res == "Partial Success")
                    {
                        reward = (int)(0.75f * (float)reward);
                    } else if (res == "Failure")
                    {
                        reward = 0;
                    } else if (res == "Partial Failure")
                    {
                        reward /= 2;
                    }

                    completionPanel.SetActive(true);
                    completionText.text += "Mission Completed with "+dict["result"]+"\n Reward: "+reward.ToString()+"\n\n";
                    GameObject.Find("Resource Manager").GetComponent<NDB_Behavior>().gold += reward;
                    
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
