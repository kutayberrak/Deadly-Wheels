using System.Collections.Generic;
using UnityEngine;
using System;

public class CarController : MonoBehaviour
{
    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
    }

    public float maxAcceleration = 30.0f;
   
    public List<Wheel> wheels;

    float moveInput;

    private Rigidbody carRb;

    private void Start()
    {
        carRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        getInput();
        wheelAnimation();
    }
    private void LateUpdate()
    {
        Move();
    }
    private void getInput()
    {
        moveInput = Input.GetAxis("Vertical");
    }
    
    private void Move()
    {
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = -moveInput * 600 * maxAcceleration * Time.deltaTime;
        }
    }
    private void wheelAnimation()
    {
        foreach(var wheel in wheels)
        {
            Quaternion rotation;
            Vector3 position;
            wheel.wheelCollider.GetWorldPose(out position, out rotation);
            wheel.wheelModel.transform.position = position;
            wheel.wheelModel.transform.rotation = rotation;
        }
    }
}