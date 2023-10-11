using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class ElementDamageType : MonoBehaviour
{
    public int damageType;
    public Material newMat;
    public List<MeshRenderer> meshRenderers;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetDamageType(int dType, Material mat)
    {
        damageType = dType;
        newMat = mat;
        foreach(MeshRenderer mr in meshRenderers)
        {
            mr.materials[0].color = newMat.color;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            print("hit enemy");
        }
    }
}
