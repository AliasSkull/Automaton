using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private string MidGamePauseScene = "MidGameSettings";
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(MidGamePauseScene);
            Debug.Log("Pause Time :D");
        }
    }
}
