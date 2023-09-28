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
    public float dashTime;

    [Header("Ground Layer")]
    public LayerMask groundMask;

    [Header("Movement Checks")]
    public bool isMoving;

    public Vector3 currentVelocity;
     


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        currentVelocity = _rb.velocity;
    }

    private void FixedUpdate()
    {

        Movement();
      
    }

 

    public void Movement() 
    {


    }


}
