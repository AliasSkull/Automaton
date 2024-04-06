using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Levels
    {
        public bool craftOpen;
        public Door door;
    }

    public List<Levels> levels;

    [HideInInspector]
    public int currentLevel = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void CheckDoorOpen()
    {   
        if (levels[currentLevel].craftOpen)
        {
            OpenDoor();
        }
    }

    public void CheckCraftDoorOpen()
    {
        if (levels[currentLevel].craftOpen && currentLevel == 2)
        {
            OpenDoor();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenDoor()
    {
        levels[currentLevel].door.OpenDoor();

        currentLevel++;
    }

    public void PlayerRespawn(PlayerController pc)
    {
        SceneManager.LoadScene(0);
    }
}
