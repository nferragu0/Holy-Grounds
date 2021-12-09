using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    
    public Animator animator;

    // Update is called once per frame
    void Update()
    {

     if(Input.GetMouseButtonDown(0)){

         FadeToScene(2);
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

     }

    }

    public void FadeToScene(int sceneIndex){

        animator.SetTrigger("FadeOut");

    }
}
