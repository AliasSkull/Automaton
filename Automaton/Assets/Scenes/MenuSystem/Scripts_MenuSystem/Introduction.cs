using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Introduction : MonoBehaviour
{
    //set object reference
    //public GameObject introScreen;
    //public Animator anim;
    [SerializeField] private string MainMenuScene = "MainMenu";

    public void Update()
    {
        SceneManager.LoadScene(MainMenuScene);
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //move object - moves it # of units per second
            //Tam - tried physically moving with script but it's causing issues so imma try animation in unity
            //introScreen.transform.position = new Vector3(0,2,0) * Time.deltaTime;
            /*
            anim.Play("IntroMove");
            time = yield return new WaitForSeconds(5f);
            if (time = 5f)
            {
                SceneManager.LoadScene(MainMenuScene);
            }
            
        }
        */
    }
}
