using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Merc : MonoBehaviour
{
    public string mercName = "Name McNamerson";
    public Sprite artwork;

    public int currHP = 50;
    public int maxHP = 50;
    public int morale = 50;
    public int strength = 50;

    public int swordSkill = 50;
    public int armorSkill = 50;

    //dont display this value to players. internal number to keep track of armor value(for some reason swords don't need this even thouth they run the same code)
    public int baseArmor = 50;

    public bool recruited = false;
    public bool swordEquip = false;
    public bool defEquip = false;

    public GameObject weaponEquip;
    public GameObject armorEquip;

    // Start is called before the first frame update
    void Start()
    {
        currHP = Random.Range(40, 60);
        maxHP = currHP;
        morale = Random.Range(40, 60);
        strength = Random.Range(40, 60);
        swordSkill = Random.Range(40, 60);
        armorSkill = Random.Range(40, 60);
        float chance = Random.Range(50.0f, 100.0f);
        mercName = chance.ToString();
        name = mercName;
        weaponEquip = GameObject.Find("sword_empty");
        armorEquip = GameObject.Find("armor_empty");

        baseArmor = armorSkill;
    }
    
}
