using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummySpawn : MonoBehaviour
{
    public GameObject Spawn1;
    public GameObject Spawn2;
    public GameObject Spawn3;
    public GameObject Spawn4;

    public GameObject prefab;

    public int[] dummyCount;

    public List<GameObject> DummyList;
    // Start is called before the first frame update
    void Start()
    {
        SpawnDummies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnDummies() 
    {
        GameObject Dummy1 = Instantiate(prefab, Spawn1.transform.position, Quaternion.identity);
        DummyList.Add(Dummy1);
        GameObject Dummy2 = Instantiate(prefab, Spawn2.transform.position, Quaternion.identity);
        DummyList.Add(Dummy2);
        GameObject Dummy3 = Instantiate(prefab, Spawn3.transform.position, Quaternion.identity);
        DummyList.Add(Dummy3);
        GameObject Dummy4 = Instantiate(prefab, Spawn4.transform.position, Quaternion.identity);
        DummyList.Add(Dummy4);
    }
}
