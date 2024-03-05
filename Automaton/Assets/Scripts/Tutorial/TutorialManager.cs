using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

    [Header("Tutorial Settings")]
    //Bool determines if tutorial plays
    public bool tutorialOn;


    [Header("Dialogue Settings")]
    public DialogueManager dm;
    public Dialogue startDialogue;

    [Header("UI References")]
    public GameObject WASDControls;

    // Start is called before the first frame update
    void Start()
    {
        WASDControls.SetActive(false);
        if (tutorialOn)
        {
            //Start Tutorial
            TriggerStartingDialogue();
        }
        else 
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerStartingDialogue() 
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(startDialogue);
        dm.activeDialogue = "StartDialogue";
      
    }

    public void DisplayWASDControls() 
    {
        WASDControls.SetActive(true);

    }
}
