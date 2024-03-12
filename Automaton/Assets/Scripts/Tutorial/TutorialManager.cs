using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;



public enum stage {

    intro,
    Controls,
    Spells,
    Combine,
    CombatIntro,
    PantoGym,
    Combat1,
    Combat

}
public class TutorialManager : MonoBehaviour
{

    [Header("Tutorial Settings")]
    //Bool determines if tutorial plays
    public bool tutorialOn;


    [Header("Dialogue Settings")]
    public Dialogue startDialogue;
    public Dialogue worktableDialogue;
    public Dialogue movetogymDialogue;
    public Dialogue testspellsDialogue;

    [Header("UI References")]
    public GameObject WASDControls;

    public CinemachineVirtualCamera defaultCam;
    public Vector3 cameraReset;


    public GameObject targetPos;
    public GameObject gymPos;
    public GameObject clickPlane;

    public stage tutorialStage;

    // Start is called before the first frame update
    void Start()
    {
        
        WASDControls.SetActive(false);
        if (tutorialOn)
        {
            //Start Tutorial
            tutorialStage = stage.intro;
            TriggerStartingDialogue();
            FindAnyObjectByType<DummySpawn>().SpawnDummies();
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

        if (tutorialStage == stage.CombatIntro)
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(movetogymDialogue);
        }

        if (tutorialStage == stage.PantoGym)
        {
            ShowTrainingRoom();
        }

        if (tutorialStage == stage.Combat1) 
        {
            CombatrTutOne();
        }
    }

    public void TriggerStartingDialogue() 
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(startDialogue);
       
      
    }

    public void PanCamera() 
    {
        defaultCam.Follow = null;
        cameraReset = defaultCam.transform.position;
        defaultCam.transform.position = Vector3.Lerp(defaultCam.transform.position, targetPos.transform.position, 5 * Time.deltaTime);
   

    }

    public void ResetCamera() 
    {
   
    }

    public void DisplayWASDControls() 
    {
        PanCamera();
        StartCoroutine(ControllerCountdown());

    }

    IEnumerator ControllerCountdown() 
    {
        WASDControls.SetActive(true);
        yield return new WaitForSeconds(4);
        WASDControls.SetActive(false);
        defaultCam.Follow = clickPlane.transform;

        tutorialStage = stage.Spells;
     
    }

    public void TriggerWorkshopDialogue() 
    { 
        FindAnyObjectByType<DialogueManager>().StartDialogue(worktableDialogue);



    }

    public void ShowTrainingRoom() 
    {
        gymPos.SetActive(true);
        defaultCam.Follow = null;
        cameraReset = defaultCam.transform.position;
        defaultCam.transform.position = Vector3.Lerp(defaultCam.transform.position,new Vector3(gymPos.transform.position.x, defaultCam.transform.position.y, gymPos.transform.position.z), 5 * Time.deltaTime);
        StartCoroutine(Timer());
        defaultCam.Follow = clickPlane.transform;
    }

    IEnumerator Timer() 
    {
        yield return new WaitForSeconds(3);
    }

    public void HideTraingingUI() 
    {
        gymPos.SetActive(false);
    }

    public void CombatrTutOne() 
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(testspellsDialogue);
       
    }

    public void ChangeStage()
    {
        tutorialStage++;

    }


}
