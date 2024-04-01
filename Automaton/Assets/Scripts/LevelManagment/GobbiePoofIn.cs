using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobbiePoofIn : MonoBehaviour
{
    public bool dummies;
    private GameObject[] dumms;

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
            if (dummies)
            {
                dumms = GameObject.FindGameObjectsWithTag("Damageable");
                
                foreach (GameObject dum in dumms)
                {
                    print(dum);
                    Destroy(dum);
                }
            }

            GameObject.Find("EnemySpawningManager").gameObject.GetComponent<EnemySpawningManager>().WaveSpawning();
            door.ReopenDoor();
            Destroy(this.gameObject);
        }
    }
}
