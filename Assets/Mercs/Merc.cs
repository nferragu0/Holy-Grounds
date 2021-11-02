using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Merc : MonoBehaviour
{
    public string mercName;
    public Sprite artwork;

    public int currHP = 50;
    public int maxHP = 50;
    public int morale = 50;
    public int strength = 50;

    public int foodCost = 15;
    public int goldCost = 25;

    public int swordSkill = 50;
    public int armorSkill = 50;

    public int trait;

    //dont display this value to players. internal number to keep track of armor value(for some reason swords don't need this even thouth they run the same code)
    public int baseArmor = 50;

    public bool recruited = false;
    public bool swordEquip = false;
    public bool defEquip = false;

    public GameObject weaponEquip;
    public GameObject armorEquip;

    string firstPath = "assets/mercs/firstNamesList.txt";
    string lastPath = "assets/mercs/lastNames.txt";

    private void Awake()
    {
        string[] lines = System.IO.File.ReadAllLines(firstPath);
        string[] linesSecond = System.IO.File.ReadAllLines(lastPath);
        mercName = lines[Random.Range(0, lines.Length)] + " " + linesSecond[Random.Range(0, linesSecond.Length)];
        name = mercName;
    }

    // Start is called before the first frame update
    void Start()
    {
        

        

        currHP = Random.Range(40, 60);

        trait = Random.Range(0, 3);

        maxHP = currHP;
        morale = Random.Range(40, 60);
        strength = Random.Range(40, 60);
        swordSkill = Random.Range(40, 60);
        armorSkill = Random.Range(40, 60);
        float chance = Random.Range(50.0f, 100.0f);
        weaponEquip = GameObject.Find("sword_empty");
        armorEquip = GameObject.Find("armor_empty");

        baseArmor = armorSkill;
        
    }

    public int getMissionUnit()
    {
        float missionUnit;

        missionUnit = (morale + strength + swordSkill + armorSkill) / 10;
        Mathf.FloorToInt(missionUnit);

        int returnUnit = Mathf.FloorToInt(missionUnit);

        return returnUnit;
    }
    
}
