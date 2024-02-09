using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private float timer;
    public float timeToOpen;
    public GameObject smoke;
    private GameObject newSmoke;
    public GameObject UI;

    private BoxCollider boxColl;

    public Vector3 startPos;

    private float endYCoord;
    private float startYCoord;
    private float lerpValue;

    public bool opening;
    public bool opened = false;
    private bool firstOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        boxColl = this.gameObject.GetComponent<BoxCollider>();
        startPos = this.transform.position;
        timer = 0;
        startYCoord = this.transform.position.y;
        endYCoord = startYCoord - 4.7f;

        if (UI != null)
        {
            UI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (opening)
        {
            OpenDaDoor();
        }

        //print(lerpValue);

        if(opened)
        {
            foreach(Transform smoke in newSmoke.transform)
            {
                smoke.gameObject.GetComponent<ParticleSystem>().Stop();
            }

            if (!firstOpen)
            {
                boxColl.enabled = false;
                firstOpen = true;
            }
            //newSmoke.GetComponent<ParticleSystem>().Stop();
        }
    }

    public void ReopenDoor()
    {
        boxColl.enabled = true;
        this.transform.position = startPos;
    }

    public void OpenDoor()
    {
        opening = true;
        newSmoke = Instantiate(smoke, this.transform);
        newSmoke.transform.SetParent(null);
        if(UI != null)
        {
            UI.SetActive(true);
        }
        

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
            opened = true;
            opening = false;
        }
    }
}
