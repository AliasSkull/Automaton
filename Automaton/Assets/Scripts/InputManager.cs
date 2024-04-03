using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Controller input;
    
    public enum InputType
    {
        MK,
        XBO,
    }

    public InputType currentInputType = InputType.XBO;

    public bool mk;

    public LayerMask aimLayer;
    public GameObject aimCursor;
    public Quaternion rotationPlayerToCursor;

    private Camera mainCam;
    private Ray mouseAimRay;
    private RaycastHit hit;

    private bool leftAttackPressed;
    private bool rightAttackPressed;

    public GameObject combinationUI;
    public Button[] buttons;

    public Button currentlySelected;

    public float valueB;

    private void Awake()
    {
        input = new Controller();
    }

    // Start is called before the first frame update
    void Start()
    {
        StaticValues.controller = true;
        mk = false;
        mainCam = Camera.main;
        leftAttackPressed = false;
        rightAttackPressed = false;

        buttons = Object.FindObjectsOfType<Button>();
        
        foreach(Button butt in buttons)
        {
            ControllerButtonChecks cButtCheeks = butt.gameObject.AddComponent<ControllerButtonChecks>();
            cButtCheeks._im = this;
            cButtCheeks.butt = butt;
        }

        combinationUI.SetActive(false);
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Interact.performed += OnInteractPerformed;
        input.Player.Interact.canceled += OnInteractCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Interact.performed -= OnInteractPerformed;
        input.Player.Interact.canceled -= OnInteractCancelled;
    }

    // Update is called once per frame
    void Update()
    {
        if (mk)
        {
            currentInputType = InputType.MK;
            StaticValues.controller = false;
        }
        else
        {
            currentInputType = InputType.XBO;
            StaticValues.controller = true;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            mk = !mk;
        }
    }

    public void OnInteractPerformed(InputAction.CallbackContext value)
    {
        valueB = value.ReadValue<float>();
        if (currentlySelected != null)
        {
            currentlySelected.onClick.Invoke();
        }
    }

    public void OnInteractCancelled(InputAction.CallbackContext value)
    {
        valueB = value.ReadValue<float>();
    }

    public Vector3 AimVector(Vector3 playerTransform)
    {
        Vector3 num = new Vector3(0,0,0);

            mouseAimRay = mainCam.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(mouseAimRay, out hit, Mathf.Infinity, aimLayer.value);

            if (aimCursor == null)
            {
                Debug.LogWarning("No target cusor assigned");
            }
            else
            {
                if (hit.point != null)
                {
                    aimCursor.transform.position = hit.point;
                }
            }

            num = new Vector3(playerTransform.x, playerTransform.y, playerTransform.z) - new Vector3(hit.point.x, playerTransform.y, hit.point.z);

        return num;
    }

    public bool LeftAttack()
    {
        bool num = false;

        if (currentInputType == InputType.MK)
        {
            num = Input.GetMouseButton(0);
        }
        else if (currentInputType == InputType.XBO)
        {
            if(Input.GetAxis("Fire1") != 0)
            {
                num = true;
                print("left attack");
            }
            else
            {
                num = false;
            }
        }

        return num;
    }

    public bool LeftAttackUp()
    {
        bool num = false;

        if (currentInputType == InputType.MK)
        {
            num = Input.GetMouseButtonUp(0);
        }
        else if (currentInputType == InputType.XBO)
        {
            if (Input.GetAxis("Fire1") == 0 && leftAttackPressed)
            {
                num = true;
                leftAttackPressed = false;
            }
        }

        return num;
    }

    public bool LeftAttackDown()
    {
        bool num = false;

        if (currentInputType == InputType.MK)
        {
            num = Input.GetMouseButtonDown(0);
        }
        else if (currentInputType == InputType.XBO)
        {
            if (Input.GetAxis("Fire1") != 0 && !leftAttackPressed)
            {
                num = true;
                leftAttackPressed = true;
                print("left attack");
            }
        }

        return num;
    }

    public bool RightAttack()
    {
        bool num = false;

        if (currentInputType == InputType.MK)
        {
            num = Input.GetMouseButton(1);
        }
        else if (currentInputType == InputType.XBO)
        {
            if (Input.GetAxis("Fire2") != 0)
            {
                num = true;
            }
            else
            {
                num = false;
            }
        }

        return num;
    }

    public bool RightAttackUp()
    {
        bool num = false;

        if (currentInputType == InputType.MK)
        {
            num = Input.GetMouseButtonUp(1);
        }
        else if (currentInputType == InputType.XBO)
        {
            if (Input.GetAxis("Fire2") == 0 && rightAttackPressed)
            {
                num = true;
                rightAttackPressed = false;
            }
        }


        return num;
    }

    public bool RightAttackDown()
    {
        bool num = false;

        if (currentInputType == InputType.MK)
        {
            num = Input.GetMouseButtonDown(1);
        }
        else if (currentInputType == InputType.XBO)
        {
            if (Input.GetAxis("Fire2") != 0 && !rightAttackPressed)
            {
                num = true;
                rightAttackPressed = true;
            }
        }

        return num;
    }
}
