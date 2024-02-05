using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCrushSecond : MonoBehaviour
{
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
        if (other.gameObject.layer == 7)
        {
            Goblin gobScript = other.gameObject.GetComponent<Goblin>();
            gobScript.StartCrowdControl(2, 0, this.transform.position, false);
            Destroy(this.gameObject);
        }
    }
}
