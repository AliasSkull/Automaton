using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStun : MonoBehaviour
{
    public List<GameObject> wallLevels;
    public float stunTime;
    private int wallHP;
    private int prevHP;
    public bool touchable;
    public GameObject wallColl;
    
    // Start is called before the first frame update
    void Start()
    {
        wallHP = wallLevels.Count;
        prevHP = wallHP;

        for (int i = 0; i < wallLevels.Count - 1; i++)
        {
            wallLevels[i].SetActive(false);
        }

        Invoke("SetTouchable", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (wallHP == 0)
        {
            Invoke("WallGoBoom", 0.5f);
            this.gameObject.GetComponent<Collider>().enabled = false;
            for(int i = 0; i< wallLevels.Count; i++)
            {
                if (wallLevels[i].activeSelf)
                {
                    wallLevels[i].SetActive(false);
                }
            }
            wallColl.SetActive(false);
        }
        else if(wallHP < prevHP)
        {
            wallLevels[wallHP - 1].SetActive(true);
            wallLevels[wallHP].SetActive(false);
            prevHP = wallHP;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Damageable" && other.gameObject.layer == 7 && touchable)
        {
            Goblin gob = other.gameObject.GetComponent<Goblin>();
            StartCoroutine(gob.Stun(3f));
            gob.damageScript.TakeDamage(0, "Stun");

            wallHP -= 1;
        }
    }



    public void SetTouchable()
    {
        touchable = true;
    }

    public void WallGoBoom()
    {
        Destroy(this.gameObject);
    }
}
