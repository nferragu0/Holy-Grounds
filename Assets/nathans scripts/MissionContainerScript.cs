using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissionContainerScript : MonoBehaviour
{
    public List<Dictionary<string,string>> missionsInProgress;
    void Start()
    {
        missionsInProgress = new List<Dictionary<string, string>>();
    }
    public void addMission(Dictionary<string, string> mission)
    {
        missionsInProgress.Add(mission);
        populateViewPort();
    }
    public void populateViewPort()
    {
        int counter = 1;
        
        foreach (Dictionary<string, string> dict in missionsInProgress)
        {
            GameObject currObj = GameObject.Find("missionBtn" + counter.ToString());
            currObj.GetComponentInChildren<Text>().text = dict["length"] + " days remaining";
            currObj.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            counter += 1;
        }
    }
}
