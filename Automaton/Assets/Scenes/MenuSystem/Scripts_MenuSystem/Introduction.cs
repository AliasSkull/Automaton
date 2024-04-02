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
    }
}
