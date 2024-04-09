using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobbiePoofIn : MonoBehaviour
{
    public bool dummies;
    private GameObject[] dumms;

    public Door door;

    public AudioSource _as;
    
    // Start is called before the first frame update
    void Start()
    {
        //_as.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyMyself()
    {
        Destroy(this.gameObject);
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
                    Destroy(dum);
                }
            }

            GameObject.Find("EnemySpawningManager").gameObject.GetComponent<EnemySpawningManager>().WaveSpawning();
            door.ReopenDoor();
            _as.Play();
            Invoke("DestroyMyself", 1f);
        }
    }
}
