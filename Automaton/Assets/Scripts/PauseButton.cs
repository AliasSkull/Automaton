using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public GameObject pauseMenuScreen;
/*
    void Start()
    {
        pauseMenuScreen.SetActive(false)
    }
*/
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            pauseMenuScreen.SetActive(true);
            Debug.Log("Pause Time :D");
        }
    }
}
