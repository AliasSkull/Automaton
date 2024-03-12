using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public enum stage {

    intro,
    Controls,
    spells,
    combat1,


}
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

    public CinemachineVirtualCamera defaultCam;
    public Transform cameraReset;


    public GameObject targetPos;
    public GameObject clickPlane;

    public stage tutorialStage;

    // Start is called before the first frame update
    void Start()
    {
        defaultCam.Priority = 10;
        
        WASDControls.SetActive(false);
        if (tutorialOn)
        {
            //Start Tutorial
            tutorialStage = stage.intro;
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
        if (tutorialStage == stage.Controls)
        {
            DisplayWASDControls();
        }
    }

    public void TriggerStartingDialogue() 
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(startDialogue);
       
      
    }

    public void PanCamera() 
    {
        defaultCam.Follow = null;
        cameraReset = defaultCam.transform;
        defaultCam.transform.position = Vector3.Lerp(defaultCam.transform.position, targetPos.transform.position, 5 * Time.deltaTime);
    
    }

    public void ResetCamera() 
    {
        defaultCam.transform.position = Vector3.Lerp(defaultCam.transform.position, cameraReset.transform.position, 5 * Time.deltaTime);
    }

    public void DisplayWASDControls() 
    {
        PanCamera();
        StartCoroutine(ControllerCountdown());
    
       
    }

    IEnumerator ControllerCountdown() 
    {
        WASDControls.SetActive(true);
        yield return new WaitForSeconds(3);
        ResetCamera();
        WASDControls.SetActive(false);
        defaultCam.Follow = clickPlane.transform;
        ChangeStage();
    }

    public void TriggerWorkshopDialogue() 
    { 
        FindAnyObjectByType<DialogueManager>().StartDialogue(worktableDialogue);



    }

    public void ShowTrainingRoom() 
    { 
        
    
    }

    public void ChangeStage()
    {
        if (tutorialStage == stage.intro)
        {
            tutorialStage = stage.Controls;
        }
        else if (tutorialStage == stage.Controls)
        {
            tutorialStage = stage.spells;
        }
    
    }

}
