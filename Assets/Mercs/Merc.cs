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

    public bool recruited = false;
    public bool swordEquip = false;

    public GameObject weaponEquip;

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
        swordEquip = true;
        weaponEquip = GameObject.Find("sword_empty");
    }
    
}
