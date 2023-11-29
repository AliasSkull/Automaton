using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextLock : MonoBehaviour
{
    public RectTransform trans;
    
    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        trans.position = transform.parent.position;

    }
}
