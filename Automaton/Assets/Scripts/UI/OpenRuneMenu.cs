using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenRuneMenu : MonoBehaviour
{
    public GameObject RuneUI;
    public PlayerAimer playerAimScript;
    private GameObject currentUI;

    public int primaryElement;
    public int secondaryElement;
    private string primaryID;
    private string secondaryID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            if(currentUI == null)
            {
                OpenMenu();
            }
            else
            {
                CloseAndSet();
            }
            
        }
    }

    public void OpenMenu()
    {
        currentUI = Instantiate(RuneUI);
        ElementIndexStorage eis = currentUI.GetComponent<ElementIndexStorage>();

        if(primaryID != null)
        {
            eis.primaryIcon.iconIndex = primaryID;
            eis.primaryIcon.SetIcon();
        }

        if (secondaryID != null)
        {
            eis.secondaryIcon.iconIndex = secondaryID;
            eis.secondaryIcon.SetIcon();
        }
    }

    public void CloseAndSet()
    {
        primaryID = currentUI.GetComponent<ElementIndexStorage>().primaryIcon.iconIndex;
        int.TryParse(primaryID[2].ToString() + primaryID[3].ToString(), out int pID);
        primaryElement = pID;
        secondaryID = currentUI.GetComponent<ElementIndexStorage>().secondaryIcon.iconIndex;
        int.TryParse(secondaryID[2].ToString() + secondaryID[3].ToString(), out int sID);
        secondaryElement = sID;

        playerAimScript.SetElement(primaryElement, secondaryElement);

        Destroy(currentUI);
    }
}
