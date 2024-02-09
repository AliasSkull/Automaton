using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
    [SerializeField] private string AutomatonGameScene = "POC";
    public void GameplayLoader()
    {
        SceneManager.LoadScene(AutomatonGameScene);
    }
}
