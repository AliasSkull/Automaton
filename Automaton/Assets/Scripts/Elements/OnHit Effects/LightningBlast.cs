using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBlast : MonoBehaviour
{
    public List<GameObject> goblinsInLightning;
    public GameObject secondBlast;
    private bool canHurt;
    private GameObject player;
    private InputManager _inputM;


    private int mouseButton;

    // Start is called before the first frame update
    void Start()
    {
        canHurt = true;
        player = GameObject.Find("Player");
        _inputM = GameObject.Find("InputManager").GetComponent<InputManager>();
        transform.SetParent(player.transform);

        PlayerAimer _pa = player.transform.GetChild(0).gameObject.GetComponent<PlayerAimer>();

        if(_pa.element1.name == "Lightning Wave")
        {
            mouseButton = 0;
        }
        else if (_pa.element2.name == "Lightning Wave")
        {
            mouseButton = 1;
        }

        print(mouseButton + " " + _inputM);

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
        if (_inputM.LeftAttackUp())
        {
            print("bruh");
        }

        if(mouseButton == 0 && !_inputM.LeftAttack())
        {
            Destroy(this.gameObject);
        }
        else if (mouseButton == 1 && !_inputM.RightAttack())
        {
            Destroy(this.gameObject);
        }
        
        transform.position = transform.parent.position;

        if (goblinsInLightning.Count > 0 && canHurt)
        {
            for (int i = 0; i < goblinsInLightning.Count; i++)
            {
                if (goblinsInLightning[i] != null)
                {
                    Vector3 vel = goblinsInLightning[i].transform.parent.gameObject.GetComponent<Rigidbody>().velocity;

                    int damageTaken = 3;
                    if (vel.magnitude > 10)
                    {
                        damageTaken = 7;
                    }

                    goblinsInLightning[i].GetComponent<Damageable>().TakeDamage(damageTaken, "");
                }
                else
                {
                    goblinsInLightning.RemoveAt(i);
                }
            }
            Invoke("ResetAttack", 0.7f);
            canHurt = false;
        }
    }

    public void ResetAttack()
    {
        canHurt = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && other.transform.parent.gameObject.layer == 7)
        {
            goblinsInLightning.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9 && other.transform.parent.gameObject.layer == 7)
        {
            for (int i = 0; i < goblinsInLightning.Count; i++)
            {
                if (other.gameObject == goblinsInLightning[i])
                {
                    goblinsInLightning.RemoveAt(i);
                }
            }
        }
    }
}
