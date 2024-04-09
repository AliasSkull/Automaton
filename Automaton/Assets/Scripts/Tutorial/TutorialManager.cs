using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public enum stage {

    GameStart,
    Intro,
    Controls,
    Spells,
    Combine,
    CombatIntro,
    PantoGym,
    Combat1Intro,
    Combat1,
    Combat2Intro,
    Combat2,
    Combat3,
    CombatEnd,
    MovetoLevel,
    Level,
    MovetoWorktable,
    Healing,
    End

}
public class TutorialManager : MonoBehaviour
{
    private TutorialManager instance;

    [Header("Tutorial Settings")]
    //Bool determines if tutorial plays
    public bool tutorialOn;


    [Header("Dialogue Settings")]
    public Dialogue introDialogue;
    public Dialogue startDialogue;
    public Dialogue worktableDialogue;
    public Dialogue gymDialogue;
    public Dialogue testspellsDialogue;
    public Dialogue rangedCombatDialogue;
    public Dialogue combatEnd;
    public Dialogue healDialogue;

    [Header("UI References")]
    public bool usingGamepad;
    public RawImage MovementWASD;
    public RawImage MovementGamepad;
    public RawImage SpellWASD;
    public RawImage SpellGamepad;
    public RawImage DashWASD;
    public RawImage DashGamepad;

    public CinemachineVirtualCamera defaultCam;
    public Vector3 cameraReset;

    [Header("Object Reference")]
    public PlayerController player;
    public PlayerAimer playerAimer;
    public GameObject targetPos;
    public GameObject doorPOS;
    public GameObject gymPos;
    public GameObject healPos;
    public GameObject clickPlane;
    public Animator controllerDisplay;
    public Animator spellControllerDisplay;
    public Animator dashControllerDisplay;
    public GameObject spellCooldownHighlight;
    public DialogueManager dm;
   

  
 

    public stage tutorialStage;

 

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }


        spellCooldownHighlight.SetActive(false);

        
        
        if (tutorialOn == true || Settings.tutorialOn == true)
        {
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
        tutorialOn = Settings.tutorialOn;

        if (tutorialOn == true && tutorialStage == stage.GameStart)
        {
            TriggerIntroDialogue();
        }
        
        if (tutorialOn == true)
        {
           
            if (tutorialStage == stage.Intro)
            {

                TriggerStartingDialogue();

            }

            if (tutorialStage == stage.Controls)
            {
                DisplayWASDControls();
            }

            if (tutorialStage == stage.CombatIntro)
            {
                TriggerCombatIntro();
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

            if (tutorialStage == stage.MovetoWorktable)
            {
                PantoHeal();
            }

            if (tutorialStage == stage.Healing)
            {
                TriggerHealDialogue();
            }

            if (tutorialStage == stage.End)
            {
                ResetTutorial();
            }
        }

        SetUpControlsUI();
    }

    public void SetUpControlsUI() 
    {
        switch (StaticValues.controller) 
        {
            case true:
                MovementWASD.enabled = false;
                MovementGamepad.enabled = true;
                SpellWASD.enabled = false;
                SpellGamepad.enabled = true;
                DashWASD.enabled = false;
                DashGamepad.enabled = true;
                break;
            case false:
                MovementWASD.enabled = true;
                MovementGamepad.enabled = false;
                SpellWASD.enabled = true;
                SpellGamepad.enabled = false;
                DashWASD.enabled = true;
                DashGamepad.enabled = false;
                break;
        }
    
    }

    public void TriggerIntroDialogue() 
    {
        if (dm.isDialoguePlaying == false)
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(introDialogue);
        }

    }


    public void TriggerStartingDialogue() 
    {
        if (dm.isDialoguePlaying == false)
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(startDialogue);
        }
       
      
    }

    public void TriggerWorkshopDialogue()
    {
        if (dm.isDialoguePlaying == false && worktableDialogue.beenPlayed == false)
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(worktableDialogue);
        }
    }

    public void PanCamera() 
    {
        //player.canMove = false;
        //playerAimer.canShoot = false;
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
        playerAimer.canShoot = true;
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
        playerAimer.canShoot = true;
        tutorialStage = stage.Spells;
    }

    IEnumerator ControllerCountdown() 
    {
        controllerDisplay.SetBool("IsOpen", true);
        yield return new WaitForSeconds(4);
        controllerDisplay.SetBool("IsOpen", false);
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
        playerAimer.canShoot = false;
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
        playerAimer.canShoot = true;
        tutorialStage = stage.Combat1Intro;
        
    }

    public void ChangeStagetoCombatIntro() 
    {
        tutorialStage = stage.CombatIntro;
        TriggerCombatIntro();
      
    }


    public void TriggerCombatIntro() 
    {
        if (dm.isDialoguePlaying == false)
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(gymDialogue);
        }
    }

   IEnumerator ShowSpellControls() 
    {
        spellControllerDisplay.SetBool("IsOpen", true);
        spellCooldownHighlight.SetActive(true);
        yield return new WaitForSeconds(4);
        spellControllerDisplay.SetBool("IsOpen", false);
        spellCooldownHighlight.SetActive(false);
    }


    public void CombatTutOne() 
    {
        if (dm.isDialoguePlaying == false && testspellsDialogue.beenPlayed == false)
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(testspellsDialogue);
        }
        StartCoroutine(ShowSpellControls());

    }

    public void Combat2Intro() 
    {
        if (dm.isDialoguePlaying == false)
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(rangedCombatDialogue);
        }
        StartCoroutine(DashCountDown());
    }

    IEnumerator DashCountDown() 
    {
        dashControllerDisplay.SetBool("IsOpen", true);
        yield return new WaitForSeconds(5);
        dashControllerDisplay.SetBool("IsOpen", false);

    }
    public void Combat2()
    {
        //Spawn 
        if (FindAnyObjectByType<DummySpawn>().DummyList.Count == 0)
        {
            FindAnyObjectByType<DummySpawn>().SpawnRangedDummeis();
        }

        tutorialStage = stage.Combat3;
    }


    public void StartEndCombatDialogue() 
    {
        if (dm.isDialoguePlaying == false)
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(combatEnd);
        }
    }

    public void PantoDoor() 
    {
        defaultCam.Follow = null;
        cameraReset = defaultCam.transform.position;
        defaultCam.transform.position = Vector3.Lerp(defaultCam.transform.position, doorPOS.transform.position, 5 * Time.deltaTime);
        //switchWASDText.enabled = true;
        playerAimer.canShoot = false;
        player.canMove = false;
        StartCoroutine(TimePantoDoor());
    }
    IEnumerator TimePantoDoor()
    {
        yield return new WaitForSeconds(2);
        defaultCam.Follow = clickPlane.transform;
        player.canMove = true;
        playerAimer.canShoot = true;
        tutorialStage = stage.Level;
    }
    public void PantoHeal() 
    {
        //Insert pan to workshop and shit
        player.canMove = false;
        playerAimer.canShoot = false;
        defaultCam.Follow = null;
        cameraReset = defaultCam.transform.position;
        defaultCam.transform.position = Vector3.Lerp(defaultCam.transform.position, new Vector3(healPos.transform.position.x, defaultCam.transform.position.y, healPos.transform.position.z), 5 * Time.deltaTime);
        StartCoroutine(TimePantoHeal());
        
    }

    IEnumerator TimePantoHeal() 
    {
        yield return new WaitForSeconds(2);
        defaultCam.Follow = clickPlane.transform;
        player.canMove = true;
        playerAimer.canShoot = true;

        tutorialStage = stage.Healing;
    }

    public void TriggerHealDialogue() 
    {
        //trigger healing dialogue
        if (dm.isDialoguePlaying == false)
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(healDialogue);
        }
    
    }

    public void ResetTutorial()
    {
        Settings.tutorialOn = false;
        tutorialStage = stage.Intro;
    }

    public void ChangeStage()
    {
        tutorialStage++;

    }


}
