using System.Collections;
using System.Collections.Generic;
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

        public enum EffectorType
        {
            all,
            none,
            onHit,
            onDestruction,
        }

        public EffectorType effectorType;
    }

    
    public List<Element> elements;
    /*
    using UnityEngine;

[CreateAssetMenu(fileName = "Part Sprite List", menuName = "Lists/Part")]
public class PartSprites : ScriptableObject
{
    public Sprite[] headSprites;
    public Sprite[] bodySprites;
    public Sprite[] ArmRSprites;
    public Sprite[] ArmLSprites;
    public Sprite[] Elements;
}
    */
}
