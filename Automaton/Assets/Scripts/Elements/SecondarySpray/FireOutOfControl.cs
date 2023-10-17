using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOutOfControl : MonoBehaviour
{
    
    public bool growable;

    public GameObject GrowingFire;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetGrowable", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGrowable()
    {
        growable = true;
    }

    public void JoinAndGrow(GameObject secondaryFire)
    {
        Destroy(secondaryFire.GetComponent<Collider>());
        Destroy(this.gameObject.GetComponent<Collider>());
        secondaryFire.SetActive(false);
        this.gameObject.SetActive(false);
        GameObject newFireGrow = Instantiate(GrowingFire, this.transform.position, this.transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (growable && Vector3.Distance(other.transform.GetChild(0).position, this.transform.GetChild(0).position) <= 1f && !other.gameObject.GetComponent<FireOutOfControl>().growable)
        {
            JoinAndGrow(other.gameObject);
        }
    }

}
