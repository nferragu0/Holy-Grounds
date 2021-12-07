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
    public List<int> selectedMercs;
    
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
                    button.GetComponentInChildren<Text>().text = merc.GetComponent<Merc>().mercName;
                    button.onClick.AddListener(selectMerc);
                    i++;
                }
            }
        }

    }
    
    void selectMerc()
    {
        var slen = 1;
        var mercButton = EventSystem.current.currentSelectedGameObject;
        var name = mercButton.name;
        if (name.Length > 5) //just in case the player happens to have more than 9 mercs
            slen = 2;
        var mercID = int.Parse(name.Substring(4, slen));//id of selected merc

        //change shade of button
        if (mercButton.GetComponent<Image>().color.Equals(Color.gray))
        {
            mercButton.GetComponent<Image>().color = Color.white;
            totalUnits -= mercs[mercID].GetComponent<Merc>().getMissionUnit();
            selectedMercs.Remove(mercID);
            //mercs[mercID].GetComponent<Merc>().isBusy = false;

        }
        else
        {
            mercButton.GetComponent<Image>().color = Color.gray;
            totalUnits += mercs[mercID].GetComponent<Merc>().getMissionUnit();
            selectedMercs.Add(mercID);
            //mercs[mercID].GetComponent<Merc>().isBusy = true;

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
        confirmButton.onClick.AddListener(startMission);
    }
    void startMission()
    {
        List<GameObject> mercs = GameObject.Find("MercContainer").GetComponent<mercCont>().mercList;
        foreach (var m in selectedMercs)
        {
            mercs[m].GetComponent<Merc>().isBusy = true;
            mercs[m].GetComponent<Merc>().daysBusy = int.Parse(GameObject.Find("MissionContainer").GetComponent<MissionData>().storyMission["length"]);
        }
        totalUnits = 0;
        selectedMercs = new List<int>();
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
