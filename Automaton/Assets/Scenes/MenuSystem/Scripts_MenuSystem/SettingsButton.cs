using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private string MainMenuSettingsScene = "Settings";
    public void MainSettingsLoader()
    {
        SceneManager.LoadScene(MainMenuSettingsScene);
    }
}
