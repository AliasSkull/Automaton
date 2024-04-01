using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private float timer;
    public float timeToOpen;
    public GameObject smoke;
    public GameObject fadeBlack;
    public MeshRenderer fadeBlackT;
    private GameObject newSmoke;
    public GameObject UI;
    public AudioSource _as;

    private BoxCollider boxColl;

    public Vector3 startPos;

    public float endYCoord;
    private float startYCoord;
    private float lerpValue;
    private float lerpValueT;

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

        if (UI != null)
        {
            UI.SetActive(false);
        }

        if (fadeBlack != null)
        {
            fadeBlack.SetActive(true);
        }

        if (fadeBlackT != null)
        {
            fadeBlackT.gameObject.SetActive(true);
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

        if (fadeBlack != null)
        {
            fadeBlack.SetActive(false);
        }

        _as.time = 0;
        _as.Play();
    }

    public void OpenDaDoor()
    {
        if (timer < timeToOpen)
        {
            lerpValue = Mathf.Lerp(startYCoord, endYCoord, timer / timeToOpen);
            lerpValueT = Mathf.Lerp(1, 0, timer / timeToOpen);

            fadeBlackT.material.color = new Color(fadeBlackT.material.color.r, fadeBlackT.material.color.g, fadeBlackT.material.color.b, lerpValueT);
            this.transform.position = new Vector3(transform.position.x, lerpValue, transform.position.z);
            timer += Time.deltaTime;
        }
        else
        {
            opened = true;
            opening = false;

            if (fadeBlackT != null)
            {
                fadeBlackT.gameObject.SetActive(false);
            }
        }
    }
}
