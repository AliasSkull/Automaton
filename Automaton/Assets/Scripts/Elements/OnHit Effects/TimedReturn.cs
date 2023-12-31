using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedReturn : MonoBehaviour
{
    public float returnTime;

    private Transform parent;
    private bool returned;
    
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent != parent && !returned)
        {
            Invoke("Return", returnTime);
            returned = true;
        }
    }

    public void Return()
    {
        if (this.gameObject.TryGetComponent<ChainWind>(out ChainWind _cw))
        {
            _cw.UnSpeed();
        }

        if(parent != null)
        {
            transform.SetParent(parent);
            transform.position = parent.position;
            returned = false;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
}
