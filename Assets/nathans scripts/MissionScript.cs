using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionScript : MonoBehaviour
{
    public int workUnits;
    public int length;
    public int reward;

    // Start is called before the first frame update
    void Start()
    {
        //workUnits = Random.range(1, 10);
        //length = Random.range(1, 3);
        reward = length * workUnits * 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
