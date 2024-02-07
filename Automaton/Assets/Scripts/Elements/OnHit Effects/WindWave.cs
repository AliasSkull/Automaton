using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindWave : MonoBehaviour
{
    public GameObject secondBlast;
    private GameObject player;

    private int mouseButton;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        transform.SetParent(player.transform);

        PlayerAimer _pa = player.transform.GetChild(0).gameObject.GetComponent<PlayerAimer>();

        if (_pa.element1.name == "Wind Wave")
        {
            mouseButton = 0;
        }
        else if (_pa.element2.name == "Wind Wave")
        {
            mouseButton = 1;
        }

        transform.position = transform.parent.position;
    }

    private void OnDestroy()
    {
        GameObject secBlast = Instantiate(secondBlast, this.transform.position, this.transform.rotation);
        secBlast.transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Damageable" && other.gameObject.layer == 7)
        {
            if (other.gameObject.TryGetComponent<Goblin>(out Goblin gob))
            {
                gob.StartCrowdControl(2, 0, this.transform.position, true);
                gob.damageScript.TakeDamage(0, "");
            }
            else if (other.gameObject.TryGetComponent<RangeGoblin>(out RangeGoblin rGob))
            {
                rGob.StartCrowdControl(2, 0, this.transform.position, true);
                rGob.damageScript.TakeDamage(0, "");
            }
        }
    }
}
