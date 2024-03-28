using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class SettingsButton : MonoBehaviour
{
    private Animator mAnimator;
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }
    //[SerializeField] private string MainMenuSettingsScene = "Settings";
    public void MainSettingsLoader()
    {
        mAnimator.SetTrigger("SettingsButtonPressed"); //SceneManager.LoadScene(MainMenuSettingsScene);
    }
}
