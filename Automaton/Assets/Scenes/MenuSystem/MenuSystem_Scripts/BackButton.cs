using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    [SerializeField] private string MainMenuScene = "MainMenu";
    public void MainMenuLoader()
    {
        SceneManager.LoadScene(MainMenuScene);
    }
}
