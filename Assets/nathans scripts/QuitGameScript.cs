using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class QuitGameScript : MonoBehaviour
{
    [SerializeField] Button quitBtn;

    // Start is called before the first frame update
    void Start()
    {
        quitBtn.onClick.AddListener(quitGame);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void quitGame()
    {
        Application.Quit();
    }
}
