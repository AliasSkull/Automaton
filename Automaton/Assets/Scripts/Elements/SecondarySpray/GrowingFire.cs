using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingFire : MonoBehaviour
{
    public float growAmount;
    public int growLevel;
    
    // Start is called before the first frame update
    void Start()
    {
        Grow();
        Invoke("TimedErase", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TimedErase()
    {
        Destroy(this.gameObject);
    }

    public void Grow()
    {
        CancelInvoke();
        Invoke("TimedErase", 2);
        this.transform.localScale = new Vector3(this.transform.localScale.x * growAmount, this.transform.localScale.y, this.transform.localScale.z * growAmount);
        growLevel++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 12)
        {
            if(growLevel < 5)
              Grow();

            other.gameObject.SetActive(false);
            Destroy(other);
        }

        
    }
}
