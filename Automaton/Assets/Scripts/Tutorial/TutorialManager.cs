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
    public Dialogue movetogymDialogue;
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

    IEnumerator ControllerCountdown() 
    {
        WASDControls.SetActive(true);
        yield return new WaitForSeconds(4);
        WASDControls.SetActive(false);
        defaultCam.Follow = clickPlane.transform;
        player.canMove = true;
        tutorialStage = stage.Spells;
     
    }

    public void TriggerWorkshopDialogue() 
    { 
        FindAnyObjectByType<DialogueManager>().StartDialogue(worktableDialogue);



    }

    public void ShowTrainingRoom() 
    {
        gymPos.SetActive(true);
        player.canMove = false;
        defaultCam.Follow = null;
        cameraReset = defaultCam.transform.position;
        defaultCam.transform.position = Vector3.Lerp(defaultCam.transform.position,new Vector3(gymPos.transform.position.x, defaultCam.transform.position.y, gymPos.transform.position.z), 5 * Time.deltaTime);
        StartCoroutine(Timer());
        defaultCam.Follow = clickPlane.transform;
        player.canMove = true;
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

    public void Combat2Into() 
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(rangedCombatDialogue);
    }

    public void Combat2()
    {
        //Spawn 
        FindAnyObjectByType<DummySpawn>().SpawnRangedDummeis();
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
