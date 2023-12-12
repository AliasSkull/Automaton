using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private float timer;
    public float timeToOpen;

    private float endYCoord;
    private float startYCoord;
    private float lerpValue;

    public bool opening;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        startYCoord = this.transform.position.y;
        endYCoord = startYCoord - 4.7f;
    }

    // Update is called once per frame
    void Update()
    {
        if (opening)
        {
            OpenDaDoor();
        }
    }

    public void OpenDoor()
    {
        opening = true;
    }

    public void OpenDaDoor()
    {
        
        if (timer < timeToOpen)
        {
            lerpValue = Mathf.Lerp(startYCoord, endYCoord, timer / timeToOpen);
            this.transform.position = new Vector3(transform.position.x, lerpValue, transform.position.z);
            timer += Time.deltaTime;
        }
        else
        {
            opening = false;
        }
    }
}
