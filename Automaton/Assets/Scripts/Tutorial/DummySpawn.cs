using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DummySpawn : MonoBehaviour
{
    public Transform Spawn1;
    public Transform Spawn2;
    public Transform Spawn3;
    public Transform Spawn4;

    public GameObject prefab;


    public TutorialManager tutorialManager;
    public int[] dummyCount;

    public List<GameObject> DummyList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DummyList.Count == 0)
        {
            SpawnDummies();
        }
        else if (tutorialManager.tutorialOn == true && DummyList.Count == 0)
        {
           // tutorialManager.ChangeStage();
        }
    }

    public void SpawnDummies() 
    {
       GameObject dummy1 = Instantiate(prefab, Spawn1.position, Quaternion.identity);
        DummyList.Add(dummy1);

    }

    public void SpawnRangedDummeis() 
    { 
        //spawn ranged dummies
    }
}
