using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class WallStun : MonoBehaviour
{
    public List<GameObject> wallLevels;
    public float stunTime;

    public ParticleSystem ice;
    public ParticleSystem ice2;


    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < wallLevels.Count - 1; i++)
        {
            wallLevels[i].SetActive(false);
        }

        Invoke("StopParticleAnim", 0.6f);
    }

    public void StopParticleAnim()
    {
        ice.Pause();
        ice2.Pause();
        Invoke("StartParticleAnim", 8.75f);
    }

    public void StartParticleAnim()
    {
        ice.Play();
        ice2.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Damageable" && other.gameObject.layer == 7)
        {
            if (other.gameObject.TryGetComponent<Goblin>(out Goblin gob))
            {
                gob.StartCrowdControl(1, 3f, this.transform.position, false);
                gob.damageScript.TakeDamage(3, 1);
            }
            else if (other.gameObject.TryGetComponent<RangeGoblin>(out RangeGoblin rGob))
            {
                rGob.StartCrowdControl(1, 3f, this.transform.position, false);
                rGob.damageScript.TakeDamage(3, 1);
            }
            else if (other.gameObject.TryGetComponent<SpecialRangedGoblin>(out SpecialRangedGoblin srGob))
            {
                srGob.StartCrowdControl(1, 3f, this.transform.position, false);
                srGob.damageScript.TakeDamage(3, 1);
            }
        
        }
    }
}
