using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;

    [Header("Movement Settings")]

    public float accelerationRate;
    public float deaccelerationRate;
    public float jumpForce;
    public float rotateSpeed;
    public float dashSpeed;
    public float currentDashTime;
    public float dashCoolDownTime;
    public GameObject capsule;


    [Header("Ground Layer")]
    public LayerMask groundMask;

    [Header("Movement Checks")]
    public bool isMoving;
    public bool canDash;

    public Vector3 currentVelocity;

    private Vector3 moveDir;
     


    // Start is called before the first frame update
    void Start()
    {
       
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        currentVelocity = _rb.velocity;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        moveDir = new Vector3(x, 0, z);
        moveDir.Normalize();
        _rb.velocity = moveDir * accelerationRate;

        if (moveDir != Vector3.zero)
        {
            capsule.transform.forward = -moveDir;
        }

        if (Input.GetKeyUp(KeyCode.Space) && canDash)
        {
            
            Dash();
           
        }

        if (currentDashTime > 0)
        {
            currentDashTime -= Time.deltaTime;
        }
        else if (currentDashTime <= 0)
        {
            canDash = true;
        }
       
    }

    private void FixedUpdate()
    {

      
      
    }

  

    public void Movement() 
    {


    }

    public void Dash() 
    {
        _rb.AddForce(moveDir * dashSpeed, ForceMode.Impulse);
        canDash = false;
        currentDashTime = dashCoolDownTime;
    }



}
