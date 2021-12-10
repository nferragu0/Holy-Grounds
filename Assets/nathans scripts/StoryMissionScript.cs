using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StoryMissionScript : MonoBehaviour
{
    [SerializeField] Button mercButton;
   // [SerializeField] Button missionMenuButton;
    [SerializeField] Button confirmButton;
    List<GameObject> mercs;
    public int totalUnits;
    public List<GameObject> selectedMercs;

    public void displayMercs()
    {
        //cleanup remove old mercs
        if (GameObject.Find("MercContent2").transform != null)
        {
            foreach (Transform child in GameObject.Find("MercContent2").transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            Button button;
            mercs = GameObject.Find("MercContainer").GetComponent<mercCont>().mercList;
            var i = 0;
            foreach (GameObject merc in mercs)
            {
                if (!merc.GetComponent<Merc>().isBusy)
                {
                    button = Instantiate(mercButton, Vector3.zero, Quaternion.identity) as Button;
                    button.transform.SetParent(GameObject.Find("MercContent2").GetComponent<RectTransform>().transform, false);
                    button.name = "merc" + i.ToString();

                    string name = merc.GetComponent<Merc>().mercName;
                    button.GetComponentInChildren<Text>().text = name;

                    //Debug.Log(name);

                    button.onClick.AddListener(() => selectMerc(name));
                    i++;
                }
            }
        }

    }
    public void selectMerc(string n)
    {
        GameObject mercenary = new GameObject();
        //finds and adds the merc to selected mercs
        foreach (GameObject merc in mercs)
        {
            if (merc.GetComponent<Merc>().mercName == n)
            {
                selectedMercs.Add(merc);
                mercenary = merc;
            }
        }



        var mercButton = EventSystem.current.currentSelectedGameObject;

        //change shade of button
        if (mercButton.GetComponent<Image>().color.Equals(Color.gray))
        {
            mercButton.GetComponent<Image>().color = Color.white;
            totalUnits -= mercenary.GetComponent<Merc>().getMissionUnit();
            selectedMercs.Remove(mercenary);

        }
        else
        {
            mercButton.GetComponent<Image>().color = Color.gray;
            totalUnits += mercenary.GetComponent<Merc>().getMissionUnit();
            selectedMercs.Add(mercenary);

        }
        GameObject.Find("MercText").GetComponent<Text>().text = "Total Work Units from Selected Mercenaries: " + totalUnits.ToString();

    }

    public void updateSelectedMercText()
    {
        GameObject.Find("MercText").GetComponent<Text>().text = "Total Work Units from Selected Mercenaries: " + totalUnits.ToString();
    }

    void OnEnable()
    {
        displayMercs();
        GameObject.Find("MissionContainer").GetComponent<MissionData>().generateStoryMission();

        //get current day
        int day = GameObject.Find("MissionContainer").GetComponent<MissionData>().getDay();

        string firstPath = "assets/storiesText.txt";

        string[] lines = System.IO.File.ReadAllLines(firstPath);

        GameObject.Find("StoryText").GetComponent<Text>().text = lines[day/10];


        confirmButton.onClick.AddListener(startMission);
    }
    void startMission()
    {
        

        List<GameObject> mercs = GameObject.Find("MercContainer").GetComponent<mercCont>().mercList;
        foreach (var m in selectedMercs)
        {
            mercs[mercs.IndexOf(m)].GetComponent<Merc>().isBusy = true;
            mercs[mercs.IndexOf(m)].GetComponent<Merc>().daysBusy = int.Parse(GameObject.Find("MissionContainer").GetComponent<MissionData>().storyMission["length"]);
        }
        totalUnits = 0;
        selectedMercs = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
