using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissionContainerScript : MonoBehaviour
{
    public List<Dictionary<string,string>> missionsInProgress;

    public List<Dictionary<string, string>> availableMissions;
    void Update()
    {
    }
    void Start()
    {
        missionsInProgress = new List<Dictionary<string, string>>();
        availableMissions = new List<Dictionary<string, string>>();
    }
    public void setAvailableMissions(List<Dictionary<string, string>> MissionList)
    {
        availableMissions = new List<Dictionary<string, string>>(MissionList);
    }
    public void addMission(Dictionary<string, string> mission)
    {
        missionsInProgress.Add(mission);
        populateViewPort();
    }
    public void populateViewPort()
    {
        //clear
        for (int i = 1; i < 7; i++)
        {
            GameObject currObj = GameObject.Find("missionBtn" + i.ToString());
            currObj.GetComponentInChildren<Text>().text = "";
            currObj.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
        }


        //populate
        int counter = 1;

        foreach (Dictionary<string, string> dict in missionsInProgress)
        {
            GameObject currObj = GameObject.Find("missionBtn" + counter.ToString());
            currObj.GetComponentInChildren<Text>().text = dict["length"] + " days remaining";
            currObj.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            counter += 1;
        }
        
    }
    public void nextDay()
    {
        List<Dictionary<string, string>> listCopy = new List<Dictionary<string, string>>(missionsInProgress);

        List<Dictionary<string, string>> missionsToRemove = new List<Dictionary<string, string>>();
        
        foreach (Dictionary<string, string> dict in listCopy)
        {
            if (int.Parse(dict["length"]) > 1)
            {
                int i = listCopy.IndexOf(dict);
                missionsInProgress[i]["length"] = (int.Parse(dict["length"]) - 1).ToString();
            }
            else //remove finished missions
            {
                missionsToRemove.Add(dict);

            }
        } //remove finished missions
        foreach (Dictionary<string, string> dict in missionsToRemove)
        {
            missionsInProgress.Remove(dict);
        }

    }
}
