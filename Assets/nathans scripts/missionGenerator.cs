using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class missionGenerator : MonoBehaviour
{
    public GameObject chanceText;
    // Start is called before the first frame update
    void Start()
    {
        float chance = Random.Range(50.0f, 100.0f);
        chanceText.GetComponent<UnityEngine.UI.Text>().text = "Success Chance: "+chance.ToString()+"%";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
