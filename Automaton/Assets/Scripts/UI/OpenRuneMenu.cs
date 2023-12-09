using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenRuneMenu : MonoBehaviour
{
    public PlayerAimer playerAimScript;
    public ElementInfoDatabase EID;
    public TextMeshProUGUI spellText;
    public TextMeshProUGUI spellText2;

    public List<GameObject> workBenches;

    public float workBenchInteractionRange;
    public LayerMask playerLayerMask;
    public RectTransform interactionTextUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CheatCodes.CheatsOn)
        {
            RuneDebugChange();
            print("bruh");
        }

        foreach(GameObject workbench in workBenches)
        {
            if (workbench.activeSelf)
            {
                Collider[] hitColls = Physics.OverlapSphere(workbench.transform.position, workBenchInteractionRange, playerLayerMask);
                if(hitColls.Length > 0)
                {
                    foreach (Collider coll in hitColls)
                    {
                        interactionTextUI.position = new Vector3(workbench.transform.position.x, workbench.transform.position.y + 3, workbench.transform.position.z + 3);
                    }
                }
                else if(hitColls.Length == 0)
                {
                    interactionTextUI.position = new Vector3(10000, 10000, 10000);
                }
            }
        }
    }

    public void ChangeRune(int element,int rune)
    {
        playerAimScript.SetElement(element, rune);
    }

    public void RuneDebugChange()
    {
        int element = 1;
        
        if(Input.GetKey("left shift"))
        {
            element = 2;
        }
        
        if (Input.GetKeyDown("1"))
        {
            ChangeRune(element, 0);

        }
        else if (Input.GetKeyDown("2"))
        {
            ChangeRune(element, 1);

        }
        else if (Input.GetKeyDown("3"))
        {
            ChangeRune(element, 2);

        }
        else if (Input.GetKeyDown("4"))
        {
            ChangeRune(element, 3);

        }
        else if (Input.GetKeyDown("5"))
        {
            ChangeRune(element, 4);

        }
        else if (Input.GetKeyDown("6"))
        {
            ChangeRune(element, 5);

        }
        else if (Input.GetKeyDown("7"))
        {
            ChangeRune(element, 6);

        }
        else if (Input.GetKeyDown("8"))
        {
            ChangeRune(element, 7);

        }
        else if (Input.GetKeyDown("9"))
        {
            ChangeRune(element, 8);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(workBenches[0].transform.position, workBenchInteractionRange);
    }
}
