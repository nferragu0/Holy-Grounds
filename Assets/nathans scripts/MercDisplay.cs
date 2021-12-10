using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MercDisplay : MonoBehaviour
{
    [SerializeField] Button mercButton;
    [SerializeField] Button missionMenuButton;
    [SerializeField] Button backButton;
    [SerializeField] GameObject mercText;
    List<GameObject> mercs;
    public int totalUnits;
    public List<GameObject> selectedMercs;
    void reset()
    {
        totalUnits = 0;
        selectedMercs = new List<GameObject>();
        mercText.GetComponent<Text>().text = "Total Work Units from Selected Mercenaries:";
    }
    void OnEnable()
    {
        backButton.onClick.AddListener(reset);
        displayMercs();
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }
    public void displayMercs()
    {
        //cleanup remove old mercs
        if (GameObject.Find("MercContent").transform != null)
        {
            foreach (Transform child in GameObject.Find("MercContent").transform)
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
                    button.transform.SetParent(GameObject.Find("MercContent").GetComponent<RectTransform>().transform, false);
                    button.name = "merc" + i.ToString();
                    
                    string name = merc.GetComponent<Merc>().mercName;
                    button.GetComponentInChildren<Text>().text = name;

                    Debug.Log(name);

                    button.onClick.AddListener(()=>selectMerc(name));
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
        GameObject.Find("SelectedMercText").GetComponent<Text>().text = "Total Work Units from Selected Mercenaries: " + totalUnits.ToString();

    }
    public void updateSelectedMercText()
    {
        GameObject.Find("SelectedMercText").GetComponent<Text>().text = "Total Work Units from Selected Mercenaries: " + totalUnits.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        

    }
}
