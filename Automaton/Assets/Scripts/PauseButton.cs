using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public GameObject pauseMenuScreen;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Cursor.visible = true;
            Time.timeScale = 0;
            pauseMenuScreen.SetActive(true);
            Debug.Log("Pause Time :D");
        }
    }
}
