using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MercList : MonoBehaviour
{

    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GameObject mercList;
    [SerializeField] GameObject MercIndivMenu;

    public List<GameObject> deleteList;
    public void ShowList()
    {
        mercCont oldCont = mercList.GetComponent<mercCont>();

        for (int i = 0; i < oldCont.mercList.Count; i++)
        {
            for (int j = 0; j < oldCont.mercList.Count; j++)
            {
                if (oldCont.mercList[i] == oldCont.mercList[j] && i != j)
                {
                    oldCont.mercList.RemoveAt(i);
                }
            }
        }

        foreach (GameObject merc in oldCont.mercList)
        {
            GameObject buttonPrefab = GameObject.Find("MercListButton");
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.transform.parent = GameObject.Find("MercMenuPanel").transform;
            button.GetComponentInChildren<Text>().text = merc.GetComponent<Merc>().mercName;
            deleteList.Add(button);
            button.GetComponent<Button>().onClick.AddListener(delegate { ShowMercButton(merc); });
            
        }
        
        
    }
    
    public void Hide()
    {
        foreach (GameObject garbo in deleteList)
        {
            Destroy(garbo);
        }
    }

    void ShowMercButton(GameObject i)
    {
        GameObject MercManageMenu = GameObject.Find("MercManageMenu");
        MercIndivMenu.SetActive(true);

        GameObject textName = GameObject.Find("MercName");
        GameObject mercHP = GameObject.Find("MercHP");
        GameObject mercOtherStats = GameObject.Find("MercOther");
        string newName = "Name: " + i.GetComponent<Merc>().mercName;
        textName.GetComponent<Text>().text = i.GetComponent<Merc>().mercName;
        mercHP.GetComponent<Text>().text = "HP: " + i.GetComponent<Merc>().currHP.ToString();
        //mercOtherStats.GetComponent<Text>().text = textName.GetComponent<Text>().text + ": Morale: " + i.GetComponent<Merc>().morale + ": strength: " + i.GetComponent<Merc>().strength + ": swordSkill: " + i.GetComponent<Merc>().swordSkill + ": armorSkill: " + i.GetComponent<Merc>().armorSkill;
        MercManageMenu.SetActive(false);
    }
}
