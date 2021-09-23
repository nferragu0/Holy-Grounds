using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MercSpawner : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GameObject mercList;


    public void spawnMerc()
    {
        GameObject go1 = new GameObject();
        Merc newScript = go1.AddComponent<Merc>();

        mercCont oldCont = mercList.GetComponent<mercCont>();
        oldCont.mercList.Add(go1);


        /*
        //go1.transform.parent = GameObject.Find("Panel").transform;
        GameObject buttonPrefab = GameObject.Find("MercListButton");

        GameObject button = (GameObject)Instantiate(buttonPrefab);
        //button.GetComponent<Text>().text = newScript.name;
        button.transform.parent = GameObject.Find("MercMenuPanel").transform;
        button.GetComponentInChildren<Text>().text = newScript.name;
        */
    }
    
}
