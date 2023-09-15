using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float rotateSpeed;
    public Quaternion rotation;
   
    // Start is called before the first frame update
    void Start()
    {

        rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
        
    }

    private void FixedUpdate()
    {
        
    }

    private void LateUpdate()
    {
      
       
    }

    public void RotateCamera() 
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(target.position, transform.up, rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(target.position, transform.up, rotateSpeed * Time.deltaTime);
        }
    }
}
