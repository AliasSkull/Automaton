using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public enum InputType
    {
        MK,
        XBO,
    }

    public InputType currentInputType = InputType.MK;

    public bool mk;

    public LayerMask aimLayer;
    public GameObject aimCursor;
    public Quaternion rotationPlayerToCursor;

    private Camera mainCam;
    private Ray mouseAimRay;
    private RaycastHit hit;

    private bool leftAttackPressed;
    private bool rightAttackPressed;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        leftAttackPressed = false;
        rightAttackPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (mk)
        {
            currentInputType = InputType.MK;
        }
        else
        {
            currentInputType = InputType.XBO;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            mk = !mk;
        }
    }

    public Vector3 AimVector(Vector3 playerTransform)
    {
        Vector3 num = new Vector3(0,0,0);

        if(currentInputType == InputType.MK)
        {
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
        }
        else if(currentInputType == InputType.XBO)
        {
            Vector3 stuff = playerTransform + new Vector3(Input.GetAxis("AimJH") * 7, 0, -Input.GetAxis("AimJV") * 7);

            if (aimCursor == null)
            {
                Debug.LogWarning("No target cusor assigned");
            }
            else
            {
                if (hit.point != null)
                {
                    aimCursor.transform.position = playerTransform + new Vector3(Input.GetAxis("AimJH") * 7, 0, -Input.GetAxis("AimJV") * 7);
                }
            }

            num = new Vector3(playerTransform.x, playerTransform.y, playerTransform.z) - new Vector3(stuff.x, playerTransform.y, stuff.z);
        }

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
