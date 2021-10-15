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

    // Start is called before the first frame update
    void Start()
    {
        MissionList = new Dictionary<int, Dictionary<string, string>>();
        //on new day generate Missions
        generateMission();
    }

    void generateMission()
    {
        //temporary hack
        var button = Instantiate(newMissionButton, Vector3.zero, Quaternion.identity) as Button;
        var rectTransform = button.GetComponent<RectTransform>();

        for (int i = 0; i < 3; i++) {
            //temporary hack
            if (i != 0)
            {
                button = Instantiate(newMissionButton, Vector3.zero, Quaternion.identity) as Button;
                rectTransform = button.GetComponent<RectTransform>();
            }
            //"CurrentMissionsScrollView"
            //button.GetComponentInChildren(Text).text = (i+1).ToString();
            button.name = (i + 1).ToString();
            rectTransform.SetParent(GameObject.Find("MissionCanvas").transform);
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.sizeDelta = new Vector2(100, 100);

            rectTransform.position = new Vector3( (i+1)*100,350,0);
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
        Debug.Log(missionId);
        Debug.Log("Work Units Required: "+MissionList[missionId]["work units"]);

        Debug.Log("Length: "+MissionList[missionId]["length"]+" days");

        Debug.Log("Reward: "+MissionList[missionId]["reward"]+" gold");
    }
    // Update is called once per frame
    void Update()
    {
        //read this for future me:
        //now add code to the above for loop to create randomized missions information DONE
        //then have it display said information for whatever last clicked on mission button
        //extra: have selected mission highlighted somehow
    }

    
}
