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
        public List<GameObject> goblinsInLevel;
    }

    public List<Levels> levels;

    private int currentLevel = 0;

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

    // Update is called once per frame
    void Update()
    {


            if (!levels[currentLevel].craftOpen)
            {
                if (currentLevel <= levels.Count - 1)
                {
                    for (int i = 0; i < levels[currentLevel].goblinsInLevel.Count; i++)
                    {
                        if (levels[currentLevel].goblinsInLevel[i] == null)
                        {
                            levels[currentLevel].goblinsInLevel.RemoveAt(i);
                        }
                    }


                    if (levels[currentLevel].goblinsInLevel.Count == 0)
                    {
                        OpenDoor();
                    }
                }
            }
        
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
