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
    Combat1Intro,
    Combat1,
    Combat2Intro,
    Combat2,
    CombatEnd,
    MovetoLevel,
    Healing,
    End

}
public class TutorialManager : MonoBehaviour
{

    [Header("Tutorial Settings")]
    //Bool determines if tutorial plays
    public bool tutorialOn;


    [Header("Dialogue Settings")]
    public Dialogue startDialogue;
    public Dialogue worktableDialogue;
    public Dialogue gymDialogue;
    public Dialogue testspellsDialogue;
    public Dialogue rangedCombatDialogue;
    public Dialogue combatEnd;
    public Dialogue healDialogue;

    [Header("UI References")]
    public GameObject WASDControls;

    public CinemachineVirtualCamera defaultCam;
    public Vector3 cameraReset;

    [Header("Object Reference")]
    public PlayerController player;
    public GameObject targetPos;
    public GameObject doorPOS;
    public GameObject gymPos;
    public GameObject clickPlane;
    public Animator controllerDisplay;
    public DialogueManager dm;

  
 

    public stage tutorialStage;

    // Start is called before the first frame update
    void Start()
    {
        
        
        if (tutorialOn )
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

        if (tutorialStage == stage.PantoGym)
        {
            ShowTrainingRoom();
        }

        if (tutorialStage == stage.Combat2Intro)
        {
            Combat2Intro();
        }

       if (tutorialStage == stage.Combat2)
        {
            Combat2();
        }

        if (tutorialStage == stage.CombatEnd) 
        {
            StartEndCombatDialogue();
        }

        if (tutorialStage == stage.MovetoLevel)
        {
            PantoDoor();
        }

        
    }


    public void TriggerStartingDialogue() 
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(startDialogue);
       
      
    }

    public void PanCamera() 
    {
        player.canMove = false;
        defaultCam.Follow = null;
        cameraReset = defaultCam.transform.position;
        defaultCam.transform.position = Vector3.Lerp(defaultCam.transform.position, targetPos.transform.position, 5 * Time.deltaTime);
        StartCoroutine(CameraPanCoolDown());

   

    }

    public void ResetCamera() 
    {
        defaultCam.Follow = clickPlane.transform;
        defaultCam.transform.position = cameraReset;
        player.canMove = true;
    }

    public void DisplayWASDControls() 
    {
        PanCamera();
        StartCoroutine(ControllerCountdown());

    }

    IEnumerator CameraPanCoolDown()
    {
        yield return new WaitForSeconds(2);
        defaultCam.Follow = clickPlane.transform;
        player.canMove = true;
        tutorialStage = stage.Spells;
    }

    IEnumerator ControllerCountdown() 
    {
        controllerDisplay.SetBool("IsOpen", true);
        yield return new WaitForSeconds(4);
        controllerDisplay.SetBool("IsOpen", false);
       
     
    }

    public void TriggerWorkshopDialogue() 
    { 
        FindAnyObjectByType<DialogueManager>().StartDialogue(worktableDialogue);



    }

    public void ShowTrainingRoom() 
    {
        gymPos.SetActive(true);
        PantoGym();
        StartCoroutine(Timer());
  
    }

    public void PantoGym() 
    {
        player.canMove = false;
        defaultCam.Follow = null;
        cameraReset = defaultCam.transform.position;
        defaultCam.transform.position = Vector3.Lerp(defaultCam.transform.position, new Vector3(gymPos.transform.position.x, defaultCam.transform.position.y, gymPos.transform.position.z), 5 * Time.deltaTime);
    }

    IEnumerator Timer() 
    {
        yield return new WaitForSeconds(3);
        defaultCam.transform.position = Vector3.Lerp(defaultCam.transform.position, cameraReset, 5 * Time.deltaTime);
        defaultCam.Follow = clickPlane.transform;
        player.canMove = true;
        tutorialStage = stage.Combat1Intro;
        
    }

    public void ChangeStagetoCombatIntro() 
    {
        tutorialStage = stage.CombatIntro;
        TriggerCombatIntro();
    }


    public void TriggerCombatIntro() 
    {
 
        FindAnyObjectByType<DialogueManager>().StartDialogue(gymDialogue);
    }


    public void CombatTutOne() 
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(testspellsDialogue);
       
    }

    public void Combat2Intro() 
    {
        if (dm.isDialoguePlaying == false)
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(rangedCombatDialogue);
        }
    }

    public void Combat2()
    {
        //Spawn 
        if (FindAnyObjectByType<DummySpawn>().DummyList.Count == 0)
        {
            FindAnyObjectByType<DummySpawn>().SpawnRangedDummeis();
        }
    }


    public void StartEndCombatDialogue() 
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(combatEnd);
    }

    public void PantoDoor() 
    {
        defaultCam.Follow = null;
        cameraReset = defaultCam.transform.position;
        defaultCam.transform.position = Vector3.Lerp(defaultCam.transform.position, doorPOS.transform.position, 5 * Time.deltaTime);
    }

    public void PantoHeal() 
    {
        //Insert pan to workshop and shit
        TriggerHealDialogue();
    }

    public void TriggerHealDialogue() 
    { 
        //trigger healing dialogue
    
    }

    public void ResetTutorial()
    {
        tutorialOn = false;
        tutorialStage = stage.intro;
    }

    public void ChangeStage()
    {
        tutorialStage++;

    }


}
