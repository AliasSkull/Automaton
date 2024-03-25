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

    public List<GameObject> DummyList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DummyList.Count == 0 && tutorialManager.tutorialOn == false)
        {
            SpawnDummies();
        }

        if (DummyList.Count == 0 && tutorialManager.tutorialStage == stage.Combat1)
        {
            tutorialManager.tutorialStage = stage.Combat2Intro;
        }

        if (DummyList.Count == 0 && tutorialManager.tutorialStage == stage.Combat3)
        {
            tutorialManager.tutorialStage = stage.CombatEnd;
            StartCoroutine(WaitToSpawn());
        }

    }

    IEnumerator WaitToSpawn() 
    {
        yield return new WaitForSeconds(3);
        SpawnDummies();
    }

    public void SpawnDummies() 
    {
       GameObject dummy1 = Instantiate(prefab, Spawn1.position, Quaternion.identity);
        DummyList.Add(dummy1);
        GameObject dummy2 = Instantiate(prefab, Spawn2.position, Quaternion.identity);
        DummyList.Add(dummy2);
        GameObject dummy3 = Instantiate(prefab, Spawn3.position, Quaternion.identity);
        DummyList.Add(dummy3);
        GameObject dummy4 = Instantiate(prefab, Spawn4.position, Quaternion.identity);
        DummyList.Add(dummy4);

    }

    public void SpawnRangedDummeis() 
    {

        
        //spawn ranged dummies
        GameObject dummy1 = Instantiate(prefab, Spawn1.position, Quaternion.identity);
        dummy1.GetComponent<MeleeDummy>().isRangedDummy = true;
        DummyList.Add(dummy1);
        GameObject dummy2 = Instantiate(prefab, Spawn2.position, Quaternion.identity);
        dummy2.GetComponent<MeleeDummy>().isRangedDummy = true;
        DummyList.Add(dummy2);
        GameObject dummy3 = Instantiate(prefab, Spawn3.position, Quaternion.identity);
        dummy3.GetComponent<MeleeDummy>().isRangedDummy = true;
        DummyList.Add(dummy3);
        GameObject dummy4 = Instantiate(prefab, Spawn4.position, Quaternion.identity);
        dummy4.GetComponent<MeleeDummy>().isRangedDummy = true;
        DummyList.Add(dummy4);
    }
}
