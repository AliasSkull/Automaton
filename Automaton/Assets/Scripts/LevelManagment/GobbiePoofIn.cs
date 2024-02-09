using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobbiePoofIn : MonoBehaviour
{
    public Door door;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 14)
        {
            GameObject.Find("EnemySpawningManager").gameObject.GetComponent<EnemySpawningManager>().WaveSpawning();
            door.ReopenDoor();
            Destroy(this.gameObject);
        }
    }
}
