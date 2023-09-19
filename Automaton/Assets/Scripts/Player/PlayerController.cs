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

    [Header("Ground Layer")]
    public LayerMask groundMask;

    [Header("Movement Checks")]
    public bool isGrounded;
    public bool isJumping;
     


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = this.transform.rotation;
        //Check for player jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {

        Movement();


        //Check if player is grounded
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.4f, groundMask))
        {
            Debug.DrawRay(transform.position, Vector3.down * 0.4f, Color.green);
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isJumping)
        {
            Jump();
        }
        
    }

    public void Movement() 
    {
       
        //Player movement and input

        if (Input.GetKey(KeyCode.A) && isGrounded)
        {
            _rb.AddForce(Vector3.left * accelerationRate * Time.deltaTime, ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.D) && isGrounded)
        {
            _rb.AddForce(Vector3.right * accelerationRate * Time.deltaTime, ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.W) && isGrounded)
        {
            _rb.AddForce(Vector3.forward * accelerationRate * Time.deltaTime, ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.S) && isGrounded)
        {
            _rb.AddForce(-Vector3.forward * accelerationRate * Time.deltaTime, ForceMode.Impulse);
        }
        else 
        {
            if (_rb.velocity.x != 0)
            {
                _rb.AddForce(new Vector3(-_rb.velocity.x, 0, 0) * deaccelerationRate * Time.deltaTime, ForceMode.Impulse);
            }
            else if (_rb.velocity.z != 0)
            {
                _rb.AddForce(new Vector3(0, 0, -_rb.velocity.z) * deaccelerationRate * Time.deltaTime, ForceMode.Impulse);
            }
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(this.transform.position, transform.up, rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(this.transform.position, transform.up, -rotateSpeed * Time.deltaTime);
        }

    }

    public void Jump() 
    {
        _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isJumping = false;
    }
}
