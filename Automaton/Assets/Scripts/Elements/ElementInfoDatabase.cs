using System.Collections;
using System.Collections.Generic;
//using UnityEditor.AssetImporters;
using UnityEngine;

[CreateAssetMenu(fileName = "Element List", menuName = "Lists/Elements")]
public class ElementInfoDatabase : ScriptableObject
{
    [System.Serializable]
    public class Element
    {
        public string name;
        public string spellType;
        public float projectileSpeed;
        public float projectileLifetime;
        public float shotCooldownTime;
        public float afterHoldCooldownTime;
        public GameObject projectileShape;
        public GameObject optionalObjectPool;
        public bool mouseDistance;
        public bool slowPlayer;
        public bool holdingSpell;
    }

    
    public List<Element> elements;
}
