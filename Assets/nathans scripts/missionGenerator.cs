using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class missionGenerator : MonoBehaviour
{
    //button for selecting missions
    [SerializeField] Button newMissionButton;
    //[SerializeField] GameObject missionContainer;


    //list of available missions, id: {length,work units, reward}
    public Dictionary<int,Dictionary<string, string>> MissionList;
    public Dictionary<string, string> newMission;

    public int currentlySelectedMission;

    // Start is called before the first frame update
    void Start()
    {
        currentlySelectedMission = -1;
        MissionList = new Dictionary<int, Dictionary<string, string>>();
        //on new day generate Missions
        generateMission();

        //for accepting missions
        GameObject.Find("ConfirmMissionButton").GetComponent<Button>().onClick.AddListener(confirmMission);
        

    }

    void generateMission()
    {
        //make three missions
        for (int i = 0; i < 3; i++) {
            
            var button = Instantiate(newMissionButton, Vector3.zero, Quaternion.identity) as Button;
            var rectTransform = button.GetComponent<RectTransform>();
            
            button.GetComponentInChildren<Text>().text = "Mission "+(i+1).ToString();
            button.name = (i + 1).ToString();
            rectTransform.SetParent(GameObject.Find("MissionCanvas").transform);
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.sizeDelta = new Vector2(100, 100);

            rectTransform.position = new Vector3( 100+(i+1)*100,300,0);
            button.onClick.AddListener(selectMission);

            //create mission information into a dictionary
            newMission = new Dictionary<string, string>();
            //create mission gameobject with dictionary of values
            newMission.Add("work units", Random.Range(1, 4).ToString());
            newMission.Add("length", Random.Range(1, 4).ToString());
            newMission.Add("reward", Random.Range(75, 200).ToString());
            //append mission to container
            MissionList.Add(i+1,newMission);

        }
    }
    void selectMission()
    {
        int missionId = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        if (currentlySelectedMission != -1)
        {
            GameObject.Find(currentlySelectedMission.ToString()).GetComponent<Image>().color = Color.white;
        }
        currentlySelectedMission = missionId;
        GameObject.Find(missionId.ToString()).GetComponent<Image>().color = Color.gray;
        //set selected mission information on UI
        GameObject.Find("SelectedMissionText").GetComponent<Text>().text = 
        "Work Units Required: " + MissionList[missionId]["work units"] + "\n" +
        "Length: " + MissionList[missionId]["length"] + " days" + "\n" +
        "Reward: " + MissionList[missionId]["reward"] + " gold";


    }
    void confirmMission()
    {
        var missions = GameObject.Find("MissionContainer").GetComponent<MissionContainerScript>();
        int missionId = currentlySelectedMission;
        
        missions.addMission(MissionList[missionId]);
    }
    // Update is called once per frame
    void Update()
    {

    }

    
}
