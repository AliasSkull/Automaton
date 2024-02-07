using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToGame : MonoBehaviour
{
    public GameObject pauseMenuScreen;
    public void ReturnToGameLoader()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
        Debug.Log("Back to Game");
    }
}
