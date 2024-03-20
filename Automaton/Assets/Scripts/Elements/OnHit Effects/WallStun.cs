using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class WallStun : MonoBehaviour
{
    public float stunTime;

    public ParticleSystem ice;
    public ParticleSystem ice2;

    public float lifeTime;
    private int extraDamage;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerAimer pa = GameObject.Find("PlayerAimer").GetComponent<PlayerAimer>();

        if (pa.element1.name == "Ice Wall")
        {
            lifeTime = pa.element1Lifetime;
            extraDamage = (int)StaticValues.lDamageBuildup;
        }
        else if (pa.element2.name == "Ice Wall")
        {
            lifeTime = pa.element2Lifetime;
            extraDamage = (int)StaticValues.rDamageBuildup;
        }

        print(lifeTime);

        Invoke("StopParticleAnim", 0.6f);
    }

    public void StopParticleAnim()
    {
        ice.Pause();
        ice2.Pause();
        Invoke("StartParticleAnim", lifeTime - 1.25f);
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
            if(other.gameObject.TryGetComponent<Goblin>(out Goblin gob))
            {
                gob.StartCrowdControl(1, 3f, this.transform.position, false);
                gob.damageScript.TakeDamage(3 + extraDamage, 1);
            }
            else if (other.gameObject.TryGetComponent<RangeGoblin>(out RangeGoblin rGob))
            {
                rGob.StartCrowdControl(1, 3f, this.transform.position, false);
                rGob.damageScript.TakeDamage(3 + extraDamage, 1);
            }
            else if (other.gameObject.TryGetComponent<SpecialRangedGoblin>(out SpecialRangedGoblin srGob))
            {
                srGob.StartCrowdControl(1, 3f, this.transform.position, false);
                srGob.damageScript.TakeDamage(3 + extraDamage, 1);
            }
        }
    }
}
