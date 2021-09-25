using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MercSpawner : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GameObject mercList;

    [SerializeField] GameObject MercRecruitMenu;
    [SerializeField] GameObject MercRecruitIndividualMenu;
    public List<GameObject> recruitMercList;
    public List<GameObject> deleteList;

    public void spawnMerc()
    {

        for(int i = 0; i<4; i++)
        {
            mercCont oldCont = mercList.GetComponent<mercCont>();
            GameObject go1 = new GameObject();
            recruitMercList.Add(go1);
            oldCont.recruitList.Add(go1);
            Merc newScript = go1.AddComponent<Merc>();
            
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.transform.parent = GameObject.Find("RecruitMenuPanel").transform;
            button.GetComponentInChildren<Text>().text = go1.GetComponent<Merc>().mercName;
            deleteList.Add(button);
            button.GetComponent<Button>().onClick.AddListener(delegate { displayMerc(go1,button); });
        }

        /*
        GameObject go1 = new GameObject();
        Merc newScript = go1.AddComponent<Merc>();

        mercCont oldCont = mercList.GetComponent<mercCont>();
        oldCont.mercList.Add(go1);
        */
     
    }

    void displayMerc(GameObject i,GameObject currButton)
    {
        //GameObject MercManageMenu = GameObject.Find("RecruitMenu");
        MercRecruitIndividualMenu.SetActive(true);

        GameObject textName = GameObject.Find("MercRecruitName");
        GameObject mercHP = GameObject.Find("MercRecruitHP");
        GameObject mercOtherStats = GameObject.Find("MercRecruitOther");
        string newName = "Name: " + i.GetComponent<Merc>().mercName;
        textName.GetComponent<Text>().text = newName;
        mercHP.GetComponent<Text>().text = "HP: " + i.GetComponent<Merc>().currHP.ToString();
        //mercOtherStats.GetComponent<Text>().text = textName.GetComponent<Text>().text + ": Morale: " + i.GetComponent<Merc>().morale + ": strength: " + i.GetComponent<Merc>().strength + ": swordSkill: " + i.GetComponent<Merc>().swordSkill + ": armorSkill: " + i.GetComponent<Merc>().armorSkill;
        MercRecruitMenu.SetActive(false);

        GameObject recruitButton = GameObject.Find("RecruitToActive");
        recruitButton.GetComponent<Button>().onClick.AddListener(delegate { addMerctoPool(i, currButton); });

    }

    void addMerctoPool(GameObject n, GameObject currNButton)
    {
        mercCont oldCont = mercList.GetComponent<mercCont>();
        if (n.GetComponent<Merc>().recruited == false)
        {
            oldCont.recruitList.Remove(n);
            oldCont.mercList.Add(n);
            GameObject MercManageMenu = GameObject.Find("RecruitMenu");
            MercRecruitMenu.SetActive(true);
            MercRecruitIndividualMenu.SetActive(false);
            //Merc newScript = i.GetComponent<Merc>();
            //newScript.recruited = true;
            Destroy(currNButton);
            recruitMercList.Remove(n);
        }
        
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
    }

    public void deleteRecruitList()
    {
        mercCont oldCont = mercList.GetComponent<mercCont>();

        foreach(GameObject garbo in oldCont.recruitList)
        {
            Destroy(garbo);
        }
    }
    
}
