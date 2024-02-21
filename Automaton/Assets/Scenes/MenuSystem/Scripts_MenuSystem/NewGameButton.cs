using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
    [SerializeField] private string AutomatonGameScene = "POC - Copy - Copy";
    public void GameplayLoader()
    {
        SceneManager.LoadScene(AutomatonGameScene);
    }
}
