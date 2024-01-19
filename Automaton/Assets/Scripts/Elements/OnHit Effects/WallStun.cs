using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStun : MonoBehaviour
{
    public List<GameObject> wallLevels;
    public float stunTime;
    private int wallHP;
    private int prevHP;
    private bool touchable;

    public bool poolObject;
    
    // Start is called before the first frame update
    void Start()
    {
        wallHP = wallLevels.Count;
        prevHP = wallHP;

        for (int i = 0; i < wallLevels.Count - 1; i++)
        {
            wallLevels[i].SetActive(false);
        }

        Invoke("SetTouchable", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (wallHP == 0)
        {
            if (poolObject)
            {
                GetComponent<TimedReturn>().Return();
            }
            else
            {
                Destroy(this.gameObject);
            }

        }
        else if(wallHP < prevHP)
        {
            wallLevels[wallHP - 1].SetActive(true);
            wallLevels[wallHP].SetActive(false);
            prevHP = wallHP;
        }
    }

    public void ResetWall()
    {
        wallHP = wallLevels.Count;
        prevHP = wallHP;

        for (int i = 0; i < wallLevels.Count - 1; i++)
        {
            wallLevels[i].SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Damageable" && other.gameObject.layer == 7)
        {
            Goblin gob = other.gameObject.GetComponent<Goblin>();
            gob.StartCrowdControl(1,3f, this.transform.position);
            gob.damageScript.TakeDamage(0, "Stun");

            wallHP -= 1;
        }
    }

    public void SetTouchable()
    {
        touchable = true;
    }

}
