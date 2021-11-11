using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MercDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> mercs = GameObject.Find("MercContainer").GetComponent<mercCont>().mercList;
        

        GameObject button = (GameObject)Instantiate(GameObject.Find("MercListButton"));
        
        button.transform.SetParent(GameObject.Find("MercScrollViewContent").GetComponent<RectTransform>().transform, false);
        if (mercs.Count > 0)
        {

            button.GetComponentInChildren<Text>().text = mercs[0].GetComponent<Merc>().mercName;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
