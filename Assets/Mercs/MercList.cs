using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MercList : MonoBehaviour
{

    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GameObject mercList;
    [SerializeField] GameObject MercIndivMenu;
    [SerializeField] GameObject weaponButton;

    public GameObject sword0;
    public GameObject sword1;
    public GameObject sword2;
    public GameObject sword3;

    GameObject currMerc;

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
        currMerc = i;

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

    public void showEquipMenu()
    {
        //setup buttons for equipment
        sword0.GetComponent<Button>().onClick.AddListener(delegate { equipSword(currMerc, sword0); });
        sword1.GetComponent<Button>().onClick.AddListener(delegate { equipSword(currMerc, sword1); });
        sword2.GetComponent<Button>().onClick.AddListener(delegate { equipSword(currMerc, sword2); });
        sword3.GetComponent<Button>().onClick.AddListener(delegate { equipSword(currMerc, sword3); });
    }

    public void sword0func()
    {
        equipSword(currMerc, sword0);
    }
    public void sword1func()
    {
        equipSword(currMerc, sword1);
    }
    public void sword2func()
    {
        equipSword(currMerc, sword2);
    }
    public void sword3func()
    {
        equipSword(currMerc, sword3);
    }

    void equipSword(GameObject i,GameObject e)
    {
        i.GetComponent<Merc>().weaponEquip.GetComponent<weaponInit>().numInv += 1;
        
        //check to make sure you have enough
        if (i.GetComponent<Merc>().weaponEquip.GetComponent<weaponInit>().weaponName == "empty")
        {
            i.GetComponent<Merc>().weaponEquip.GetComponent<weaponInit>().numInv += 1;
            i.GetComponent<Merc>().weaponEquip = e.GetComponent<equipInnit>().equipmentPoiner;
        }
        if (e.GetComponent<equipInnit>().equipmentPoiner.GetComponent<weaponInit>().numInv > 0)
        {
            //unequip old weapon
            //if (i.GetComponent<Merc>().swordEquip)
            //{
            i.GetComponent<Merc>().weaponEquip.GetComponent<weaponInit>().numInv += 1;
                //i.GetComponent<Merc>().swordSkill -= i.GetComponent<Merc>().weaponEquip.GetComponent<weaponInit>().attackVal;

            int minus1 = i.GetComponent<Merc>().swordSkill;
            int minus2 = i.GetComponent<Merc>().weaponEquip.GetComponent<weaponInit>().attackVal;
            i.GetComponent<Merc>().swordSkill = minus1 - minus2;
            i.GetComponent<Merc>().swordEquip = false;
            //check for equiped sword
            if (!i.GetComponent<Merc>().swordEquip)
            {
                //equip init
                i.GetComponent<Merc>().weaponEquip.GetComponent<weaponInit>().numInv -= 1;
                i.GetComponent<Merc>().swordEquip = true;
                i.GetComponent<Merc>().weaponEquip = e.GetComponent<equipInnit>().equipmentPoiner;

                //equip sword
                int q = i.GetComponent<Merc>().swordSkill;
                int n = e.GetComponent<equipInnit>().equipmentPoiner.GetComponent<weaponInit>().attackVal;
                i.GetComponent<Merc>().swordSkill = q + n;
                e.GetComponent<equipInnit>().equipmentPoiner.GetComponent<weaponInit>().numInv = e.GetComponent<equipInnit>().equipmentPoiner.GetComponent<weaponInit>().numInv - 1;

                
            }
        }
    }
}
