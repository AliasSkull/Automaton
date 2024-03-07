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
    public Dialogue startDialogue;
    public Dialogue worktableDialogue;

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
       
      
    }

    public void DisplayWASDControls() 
    {
        WASDControls.SetActive(true);
        StartCoroutine(ControllerCountdown());
        WASDControls.SetActive(false);
    }

    IEnumerator ControllerCountdown() 
    {

        yield return new WaitForSeconds(3);
    }

    public void TriggerWorkshopDialogue() 
    { 
        FindAnyObjectByType<DialogueManager>().StartDialogue(worktableDialogue);
       


    }
}
