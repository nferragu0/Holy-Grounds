using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class missionGenerator : MonoBehaviour
{
    private GameObject chanceText;
    public Button btn;

    // Start is called before the first frame update
    void Start()
    {
        //get button
        btn = gameObject.GetComponentInParent(typeof(Button)) as Button;
        //when clicked, set mission
        btn.onClick.AddListener(regenerateMission);

        //get chance text for work units and set it
        regenerateMission();

    }

    void regenerateMission()
    {
        chanceText = GameObject.Find("ChanceText");
        float chance1 = Random.Range(0.0f, 1.0f);
        int chance = (int)(chance1 * 10) + 1;
        chanceText.GetComponent<UnityEngine.UI.Text>().text = "Work Units Required: " + chance.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }

    
}
