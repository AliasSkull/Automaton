using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPuddle : MonoBehaviour
{
    public ElementDamageType edt;
    public GameObject puddle;

    // Start is called before the first frame update
    void Start()
    {
        edt = gameObject.GetComponent<ElementDamageType>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropPuddle(Vector3 pos)
    {
        GameObject newPuddle = Instantiate(puddle, pos, transform.rotation);
        newPuddle.GetComponent<ElementDamageType>().SetDamageType(edt.damageType, edt.newMat);

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            DropPuddle(other.ClosestPoint(transform.position));
        }
    }
}
