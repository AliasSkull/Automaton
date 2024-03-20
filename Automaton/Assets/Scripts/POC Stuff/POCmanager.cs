using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class POCmanager : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> goblinsInScene;

    public List<Vector3> goblinSpawnPoints;

    public GameObject goblinPrefab;
    
    private int goblinCount;
    private bool noGoblins;
    private Transform playerSpawn;


    // Start is called before the first frame update
    void Start()
    {
        playerSpawn = player.transform;
        noGoblins = false;
        goblinCount = goblinsInScene.Count;

        foreach (GameObject goblin in goblinsInScene)
        {
            goblinSpawnPoints.Add(goblin.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < goblinsInScene.Count; i++)
        {
            if (goblinsInScene[i] == null)
            {
                goblinsInScene.RemoveAt(i);
            }
        }

        if(goblinsInScene.Count == 0 && noGoblins == false)
        {
            print(" NO GOBLINS ");
            noGoblins = true;
            StartCoroutine(RespawnGoblins());
        }
    }

    public IEnumerator RespawnGoblins()
    {
        yield return new WaitForSeconds(5);
        for (int i = 0; i < goblinCount; i++)
        {
            GameObject newGobby = Instantiate(goblinPrefab, goblinSpawnPoints[i], goblinPrefab.transform.rotation);
            goblinsInScene.Add(newGobby);
        }
        noGoblins = false;
    }

    public void PlayerRespawn(PlayerController pc)
    {
        SceneManager.LoadScene("Aidan's - POC");
    }

}
