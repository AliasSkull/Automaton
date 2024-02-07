using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningGen : MonoBehaviour
{
    public GameObject Explosion;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Esploud()
    {
        GameObject lightning = Instantiate(Explosion, this.transform);
        StartCoroutine(SplodeDelete(lightning));
    }

    public IEnumerator SplodeDelete(GameObject lightning)
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(lightning);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
