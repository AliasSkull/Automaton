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

    public ElementManager eid;

    public List<GameObject> workBenches;

    public float workBenchInteractionRange;
    public LayerMask playerLayerMask;
<<<<<<< HEAD
    //public RectTransform interactionTextUI;
=======
>>>>>>> parent of b7c3fe7 (Revert "Merge branch 'Aidan's-Programming-Branch-2'")
    public LevelManager _lm;

    public bool alreadyOpened;

    public float interactButton;
    public float closeButton;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (combinationUI.activeSelf)
        {
            CheckCloseInput();
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
<<<<<<< HEAD
                            //interactionTextUI.position = new Vector3(workbench.transform.position.x, workbench.transform.position.y + 3, workbench.transform.position.z + 3);
=======
>>>>>>> parent of b7c3fe7 (Revert "Merge branch 'Aidan's-Programming-Branch-2'")
                            if (text != null)
                            {
                                //text.SetActive(true);
                            }
                            CheckOpenInput();
                        }
                    }
                    else if (hitColls.Length == 0)
                    {
<<<<<<< HEAD
                        //interactionTextUI.position = new Vector3(10000, 10000, 10000);
=======
>>>>>>> parent of b7c3fe7 (Revert "Merge branch 'Aidan's-Programming-Branch-2'")

                        if(text != null)
                        {
                            //text.SetActive(false);
                        }
                    }
                }
            }
        }
        
        if (CheatCodes.CheatsOn)
        {
            RuneDebugChange();
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

                _lm.CheckDoorOpen();
                alreadyOpened = true;
            }
        }
    }

    public void ChangeRune(int gunIndex,int rune)
    {
        playerAimScript.SetElement(gunIndex, rune);
    }

    public void RuneDebugChange()
    {
        /*int element = 1;
        
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
        }*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(workBenches[0].transform.position, workBenchInteractionRange);
    }
}
