using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MissionScript : MonoBehaviour
{
    //button for selecting missions
    [SerializeField] Button newMissionButton;
    //merc list (to update after selectinng mercs)
    [SerializeField] GameObject mercScrollView;
    [SerializeField] GameObject missionPanel;
    [SerializeField] GameObject missionConfirmPanel;
    //storing missions data
    public List<Dictionary<string, string>> missionsInProgress;
    public List<Dictionary<string, string>> availableMissions;
    //mission object
    public Dictionary<string, string> newMission;
    public int currentlySelectedMission;

    public int dayNum;//keeping track of days
    
    void Start()
    {
        missionsInProgress = new List<Dictionary<string, string>>();
        availableMissions = new List<Dictionary<string, string>>();

        currentlySelectedMission = -1;
        dayNum = 0;
        //on new day generate Missions
         generateMission();
        
        
        

    }

    public void generateMission()
    {
        //delete previously created missions
        if (GameObject.Find("1") != null)
        {
            for (int i = 1; i <= 3; i++)
            {
                Destroy(GameObject.Find(i.ToString()));
            }
            
        }
        availableMissions = new List<Dictionary<string, string>>();
        //make three missions
        for (int i = 0; i < 3; i++)
        {

            var button = Instantiate(newMissionButton, Vector3.zero, Quaternion.identity) as Button;
            var rectTransform = button.GetComponent<RectTransform>();

            button.GetComponentInChildren<Text>().text = "Mission " + (i + 1).ToString();
            button.name = (i + 1).ToString();
            rectTransform.SetParent(GameObject.Find("MissionPanel").transform);
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.sizeDelta = new Vector2(100, 100);

            rectTransform.position = new Vector3(225 + (i + 1) * 100, 375, 0);
            button.onClick.AddListener(selectMission);


            availableMissions = new List<Dictionary<string, string>>(GameObject.Find("MissionContainer").GetComponent<MissionData>().getAvailableMissions());
        }
    }

    void selectMission()
    {
        //grab last clicked mission
        int missionId = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        if (currentlySelectedMission != -1)
        {
            GameObject.Find(currentlySelectedMission.ToString()).GetComponent<Image>().color = Color.white;
        }
        currentlySelectedMission = missionId;
        GameObject.Find(missionId.ToString()).GetComponent<Image>().color = Color.gray;

        
        
        missionConfirmPanel.SetActive(true);
        //after selected
        //set selected mission information on UI
        int position = missionId - 1;
        GameObject.Find("SelectedMissionText").GetComponent<Text>().text =
        availableMissions[position]["missionText"]+"\n\n"+
        "Work Units Required: " + availableMissions[position]["work units"] + "\n" +
        "Length: " + availableMissions[position]["length"] + " days" + "\n" +
        "Reward: " + availableMissions[position]["reward"] + " gold";

        GameObject.Find("ConfirmMissionButton").GetComponent<Button>().onClick.AddListener(confirmMission);

    }
    string calculateSuccess(int missionUnits, int mercUnits)
    {
        if (mercUnits < missionUnits)
        {
            if ((missionUnits - mercUnits) > 30)
            {
                if (Random.Range(0, 100) < 10)
                {
                    return "Partial Success";
                } else
                {
                    return "Failure";
                }
            }
            else if ((missionUnits - mercUnits) > 20)
            {
                if (Random.Range(0, 100) < 25)
                {
                    return "Partial Success";
                }
                else
                {
                    return "Failure";
                }
            }
            else if ((missionUnits - mercUnits) > 10)
            {
                if (Random.Range(0, 100) < 50)
                {
                    return "Partial Success";
                }
                else
                {
                    return "Failure";
                }
            }
            else if ((missionUnits - mercUnits) > 0)
            {
                if (Random.Range(0, 100) < 70)
                {
                    return "Partial Success";
                } 
                else
                {
                    return "Failure";
                }
            }
        }
        return "Success";
    }


    void confirmMission()
    {
        var mercDisplay = mercScrollView.GetComponent<MercDisplay>();
        List<int> selectedMercs = mercDisplay.selectedMercs;
        var totalUnits = mercDisplay.totalUnits;
        if (selectedMercs.Count > 0)
        {
            GameObject.Find(currentlySelectedMission.ToString()).GetComponent<Image>().color = Color.clear;
            var mission = availableMissions[currentlySelectedMission - 1];
            
            //do calculations for winning or losing mission [still in progress]
            //when mercs meet requirements -> small chance of problems
            int missionWorkUnits = int.Parse(mission["work units"]);
            mission.Add("result", calculateSuccess(missionWorkUnits, totalUnits));

            missionsInProgress.Add(mission);



            
            List<GameObject> mercs = GameObject.Find("MercContainer").GetComponent<mercCont>().mercList;
            foreach (var m in selectedMercs)
            {
                mercs[m].GetComponent<Merc>().isBusy = true;
                mercs[m].GetComponent<Merc>().daysBusy = int.Parse(mission["length"]);
            }
            populateViewPort();
            //mercDisplay.displayMercs();
            mercDisplay.totalUnits = 0;
            //mercDisplay.updateSelectedMercText();
            mercDisplay.selectedMercs = new List<int>();
            //GameObject.Find("MissionConfirmPanel").SetActive(false);
        }

        
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
    
    public void ifDayChanged()
    {
        int realDay = GameObject.Find("MissionContainer").GetComponent<MissionData>().getDay();
        if (dayNum != realDay)
        {
            dayNum = realDay;
            missionsInProgress = GameObject.Find("MissionContainer").GetComponent<MissionData>().getMissionsInProgress();
            if (missionPanel.activeInHierarchy)
            {
                generateMission();
                populateViewPort();
            }
            
            
            
        } 
        //call functions to draw and populate stuff
    }

    // Update is called once per frame
    void Update()
    {
        ifDayChanged();
    }
}
