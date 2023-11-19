using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedWindChainReturn : MonoBehaviour
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
        if (transform.parent != parent && !returned)
        {
            Invoke("Return", returnTime);
            returned = true;
        }
    }

    public void Return()
    {
        Transform newWall = parent.parent.Find("Wind").GetChild(0);
        newWall.SetParent(null);
        newWall.position = this.transform.position;
        newWall.localScale = new Vector3(this.transform.localScale.x, newWall.localScale.y, newWall.localScale.z);
        newWall.rotation = this.transform.rotation;

        transform.SetParent(parent);
        transform.position = parent.position;
        returned = false;
    }
}
