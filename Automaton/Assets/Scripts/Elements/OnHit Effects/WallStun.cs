using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class WallStun : MonoBehaviour
{
    public List<GameObject> wallLevels;
    public float stunTime;
    private int wallHP;
    private int prevHP;

    public ParticleSystem ice;

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

        Invoke("StopParticleAnim", 0.6f);
    }

    public void StopParticleAnim()
    {
        ice.Pause();
        Invoke("StartParticleAnim", 8.75f);
    }

    public void StartParticleAnim()
    {
        ice.Play();
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
            gob.StartCrowdControl(1,3f, this.transform.position, false);
            gob.damageScript.TakeDamage(3, " Stun");

            //wallHP -= 1;
        }
    }
}
