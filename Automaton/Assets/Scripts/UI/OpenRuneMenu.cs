using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;

public class OpenRuneMenu : MonoBehaviour
{
    public PlayerAimer playerAimScript;
    public GameObject combinationUI;
    public TextMeshProUGUI spellText;
    public TextMeshProUGUI spellText2;
    public Controller input;

    public GameObject text;
    public GameObject sameSpellText;
    public GameObject skillpoints;

    public ElementManager eid;

    public List<GameObject> workBenches;

    public float workBenchInteractionRange;
    public LayerMask playerLayerMask;
    public LevelManager _lm;

    public bool alreadyOpened;

    public float interactButton;
    public float closeButton;

    private TutorialManager tm;
   

    private void Awake()
    {
        input = new Controller();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Interact.performed += OnInteractPerformed;
        input.Player.Interact.canceled += OnInteractCancelled;
        input.Player.CloseMenu.performed += OnCloseMenuPerformed;
        input.Player.CloseMenu.canceled += OnCloseMenuCancelled;
     
    }

    private void OnDisable()
    {

        input.Disable();
        input.Player.Interact.performed -= OnInteractPerformed;
        input.Player.Interact.canceled -= OnInteractCancelled;
        input.Player.CloseMenu.performed -= OnCloseMenuPerformed;
        input.Player.CloseMenu.canceled -= OnCloseMenuCancelled;

    }
    // Start is called before the first frame update
    void Start()
    {
        alreadyOpened = false;
        tm = FindAnyObjectByType<TutorialManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (combinationUI.activeSelf)
        {
            CheckCloseInput();
            tm.TriggerWorkshopDialogue();
        }
        else
        {
            foreach (GameObject workbench in workBenches)
            {
                if (workbench.activeSelf && !combinationUI.activeSelf)
                {
                    Collider[] hitColls = Physics.OverlapSphere(workbench.transform.position, workBenchInteractionRange, playerLayerMask);
                    if (hitColls.Length > 0)
                    {
                        foreach (Collider coll in hitColls)
                        {
                            if (text != null)
                            {
                                //text.SetActive(true);
                            }
                            CheckOpenInput();
                        }
                    }
                    else if (hitColls.Length == 0)
                    {
                        if(text != null)
                        {
                            //text.SetActive(false);
                        }
                    }
                }
            }
        }

       
    }

    public void OnInteractPerformed(InputAction.CallbackContext value)
    {
        interactButton = value.ReadValue<float>();
    }

    public void OnInteractCancelled(InputAction.CallbackContext value)
    {
        interactButton = value.ReadValue<float>();
    }

    public void OnCloseMenuPerformed(InputAction.CallbackContext value)
    {

        closeButton = value.ReadValue<float>();
    }

    public void OnCloseMenuCancelled(InputAction.CallbackContext value)
    {
        closeButton = value.ReadValue<float>();
    }



    public void CheckOpenInput()
    {
        if (interactButton == 1 )
        {
            combinationUI.SetActive(true);

            if (sameSpellText.activeSelf)
            {
                sameSpellText.SetActive(false);
            }

            if(_lm.currentLevel >= 2)
            {
                skillpoints.SetActive(true);
            }
            else
            {
                skillpoints.SetActive(false);
            }

            playerAimScript.menuOpen = true;
            Cursor.visible = true;
            alreadyOpened = true;
        }
    }

    public void CheckCloseInput()
    {
        if (closeButton == 1)
        {
            int runeCombo1 = combinationUI.transform.Find("CombinationLeft").GetComponent<RuneChoser>().runeCombo;
            int runeCombo2 = combinationUI.transform.Find("CombinationRight").GetComponent<RuneChoser>().runeCombo;

            if (runeCombo1 == runeCombo2)
            {
                sameSpellText.SetActive(true);
            }
            else
            {
                playerAimScript.menuOpen = false;
                Cursor.visible = false;

                ChangeRune(1, runeCombo1);
                ChangeRune(2, runeCombo2);

                spellText.text = eid.publicAccessElementDatabase.elements[runeCombo1].name;
                spellText2.text = eid.publicAccessElementDatabase.elements[runeCombo2].name;

                combinationUI.SetActive(false);

                if (tm.tutorialStage == stage.Combine)
                {
                    tm.tutorialStage = stage.CombatIntro;
                }

                //_lm.CheckDoorOpen();
                alreadyOpened = true;
            }
        }
    }

    public void ChangeRune(int gunIndex,int rune)
    {
        playerAimScript.SetElement(gunIndex, rune);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(workBenches[0].transform.position, workBenchInteractionRange);
    }
}
