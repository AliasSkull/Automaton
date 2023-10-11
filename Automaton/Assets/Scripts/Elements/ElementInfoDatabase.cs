using System.Collections;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;

[CreateAssetMenu(fileName = "Element List", menuName = "Lists/Elements")]
public class ElementInfoDatabase : ScriptableObject
{
    [System.Serializable]
    public class Element
    {
        public string name;
        public float projectileSpeed;
        public float projectileLifetime;
        public float shotCooldownTime;
        public GameObject projectileShape;
        public Material elementMaterial;
        
        public int damageType;
    }

    
    public List<Element> elements;
}
