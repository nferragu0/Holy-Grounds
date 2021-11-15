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

    public GameObject armor0;
    public GameObject armor1;
    public GameObject armor2;
    public GameObject armor3;

    GameObject currMerc;

    public List<GameObject> deleteList;

    public GameObject sword0actual;
    public GameObject sword1actual;
    public GameObject sword2actual;
    public GameObject sword3actual;

    public GameObject armor0actual;
    public GameObject armor1actual;
    public GameObject armor2actual;
    public GameObject armor3actual;

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
        GameObject MercAttack = GameObject.Find("MercAttack");
        GameObject MercDef = GameObject.Find("MercDef");
        GameObject MercMorale = GameObject.Find("MercMorale");
        GameObject MercTrait = GameObject.Find("MercTrait");
        string newName = "Name: " + i.GetComponent<Merc>().mercName;
        textName.GetComponent<Text>().text = i.GetComponent<Merc>().mercName;
        mercHP.GetComponent<Text>().text = "HP: " + i.GetComponent<Merc>().currHP.ToString();
        //mercOtherStats.GetComponent<Text>().text = textName.GetComponent<Text>().text + ": Morale: " + i.GetComponent<Merc>().morale + ": strength: " + i.GetComponent<Merc>().strength + ": swordSkill: " + i.GetComponent<Merc>().swordSkill + ": armorSkill: " + i.GetComponent<Merc>().armorSkill;
        MercAttack.GetComponent<Text>().text = "Attack: " + (i.GetComponent<Merc>().strength + i.GetComponent<Merc>().swordSkill).ToString();
        MercDef.GetComponent<Text>().text = "Defense: " + i.GetComponent<Merc>().armorSkill.ToString();
        MercMorale.GetComponent<Text>().text = "Morale: " + i.GetComponent<Merc>().morale.ToString();

        MercTrait.GetComponent<Text>().text = "Trait: " + i.GetComponent<Merc>().traitName;

        MercManageMenu.SetActive(false);



    }
    
    public void sword0func()
    {
        equipSword(currMerc, sword0);
        //GameObject swordInvNum = GameObject.Find("SwordinvAmount5");
        //swordInvNum.GetComponent<Text>().text = "(" + sword0.GetComponent<weaponInit>().numInv;
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

    public void armor0func()
    {
        equipArmor(currMerc, armor0);
    }
    public void armor1func()
    {
        equipArmor(currMerc, armor1);
    }
    public void armor2func()
    {
        equipArmor(currMerc, armor2);
    }
    public void armor3func()
    {
        equipArmor(currMerc, armor3);
    }


    void equipSword(GameObject i,GameObject e)
    {
        //check if equipment available
        if (e.GetComponent<equipInnit>().equipmentPoiner.GetComponent<weaponInit>().numInv > 0)
        {
            //check if anything equiped
            if (i.GetComponent<Merc>().swordEquip)
            {
                i.GetComponent<Merc>().swordEquip = true;
                i.GetComponent<Merc>().weaponEquip.GetComponent<weaponInit>().numInv += 1;
                e.GetComponent<equipInnit>().equipmentPoiner.GetComponent<weaponInit>().numInv = e.GetComponent<equipInnit>().equipmentPoiner.GetComponent<weaponInit>().numInv - 1;

                int q = i.GetComponent<Merc>().swordSkill;
                int n = e.GetComponent<equipInnit>().equipmentPoiner.GetComponent<weaponInit>().attackVal;
                i.GetComponent<Merc>().swordSkill = q + n;

                q = i.GetComponent<Merc>().swordSkill;
                n = i.GetComponent<Merc>().weaponEquip.GetComponent<weaponInit>().attackVal;
                i.GetComponent<Merc>().swordSkill = q - n;

                i.GetComponent<Merc>().weaponEquip = e.GetComponent<equipInnit>().equipmentPoiner;


            }

            //check if nothing equiped
            if (!i.GetComponent<Merc>().swordEquip)
            {
                i.GetComponent<Merc>().swordEquip = true;
                e.GetComponent<equipInnit>().equipmentPoiner.GetComponent<weaponInit>().numInv = e.GetComponent<equipInnit>().equipmentPoiner.GetComponent<weaponInit>().numInv - 1;
                int q = i.GetComponent<Merc>().swordSkill;
                int n = e.GetComponent<equipInnit>().equipmentPoiner.GetComponent<weaponInit>().attackVal;
                i.GetComponent<Merc>().swordSkill = q + n;
                i.GetComponent<Merc>().weaponEquip = e.GetComponent<equipInnit>().equipmentPoiner;
            }
        }


        updateWeaponCountDisp();
    }

    void equipArmor(GameObject i, GameObject e)
    {
        //check if equipment available
        if (e.GetComponent<equipInnit>().equipmentPoiner.GetComponent<armorInit>().numInv > 0)
        {
            //check if anything equiped
            if (i.GetComponent<Merc>().defEquip)
            {
                i.GetComponent<Merc>().defEquip = true;
                i.GetComponent<Merc>().armorEquip.GetComponent<armorInit>().numInv += 1;
                e.GetComponent<equipInnit>().equipmentPoiner.GetComponent<armorInit>().numInv = e.GetComponent<equipInnit>().equipmentPoiner.GetComponent<armorInit>().numInv - 1;

                int q = i.GetComponent<Merc>().armorSkill;
                int n = e.GetComponent<equipInnit>().equipmentPoiner.GetComponent<armorInit>().defVal;
                i.GetComponent<Merc>().armorSkill = i.GetComponent<Merc>().baseArmor + n;
                /*
                q = i.GetComponent<Merc>().armorSkill;
                n = i.GetComponent<Merc>().armorEquip.GetComponent<armorInit>().defVal;
                i.GetComponent<Merc>().armorSkill = q - n;
                */
                i.GetComponent<Merc>().armorEquip = e.GetComponent<equipInnit>().equipmentPoiner;


            }

            //check if nothing equiped
            if (!i.GetComponent<Merc>().defEquip)
            {
                i.GetComponent<Merc>().defEquip = true;
                e.GetComponent<equipInnit>().equipmentPoiner.GetComponent<armorInit>().numInv = e.GetComponent<equipInnit>().equipmentPoiner.GetComponent<armorInit>().numInv - 1;
                int q = i.GetComponent<Merc>().armorSkill;
                int n = e.GetComponent<equipInnit>().equipmentPoiner.GetComponent<armorInit>().defVal;
                i.GetComponent<Merc>().armorSkill = q + n;
                i.GetComponent<Merc>().weaponEquip = e.GetComponent<equipInnit>().equipmentPoiner;
            }
        }

        updateArmorCountDisp();

    }

    public void updateWeaponCountDisp()
    {
        GameObject swordInvNum = GameObject.Find("SwordinvAmount5");
        swordInvNum.GetComponent<Text>().text = "(" + sword0actual.GetComponent<weaponInit>().numInv + ")";

        swordInvNum = GameObject.Find("SwordinvAmount10");
        swordInvNum.GetComponent<Text>().text = "(" + sword1actual.GetComponent<weaponInit>().numInv + ")";

        swordInvNum = GameObject.Find("SwordinvAmount15");
        swordInvNum.GetComponent<Text>().text = "(" + sword2actual.GetComponent<weaponInit>().numInv + ")";

        swordInvNum = GameObject.Find("SwordinvAmount20");
        swordInvNum.GetComponent<Text>().text = "(" + sword3actual.GetComponent<weaponInit>().numInv + ")";
    }
    public void updateArmorCountDisp()
    {
        GameObject swordInvNum = GameObject.Find("ArmorinvAmount5");
        swordInvNum.GetComponent<Text>().text = "(" + armor0actual.GetComponent<armorInit>().numInv.ToString() + ")";

        swordInvNum = GameObject.Find("ArmorinvAmount10");
        swordInvNum.GetComponent<Text>().text = "(" + armor1actual.GetComponent<armorInit>().numInv + ")";

        swordInvNum = GameObject.Find("ArmorinvAmount15");
        swordInvNum.GetComponent<Text>().text = "(" + armor2actual.GetComponent<armorInit>().numInv + ")";

        swordInvNum = GameObject.Find("ArmorinvAmount20");
        swordInvNum.GetComponent<Text>().text = "(" + armor3actual.GetComponent<armorInit>().numInv + ")";
    }
}
