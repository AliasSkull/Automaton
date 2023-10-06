using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPuddle : MonoBehaviour
{

    public GameObject puddle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropPuddle(Vector3 pos)
    {
        Instantiate(puddle, pos, transform.rotation);
        Destroy(this.transform.parent.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            DropPuddle(other.ClosestPoint(transform.position));
        }
    }
}
