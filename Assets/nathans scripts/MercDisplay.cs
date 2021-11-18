using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MercDisplay : MonoBehaviour
{
    [SerializeField] Button mercButton;
    [SerializeField] Button missionMenuButton;
    List<GameObject> mercs;
    public int totalUnits;
    public List<int> selectedMercs;
    
    void OnEnable()
    {
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
        var mercID = int.Parse(name.Substring(4,slen));//id of selected merc

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
