using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

public class CarController : MonoBehaviour
{
    public int maxFuel = 20;
    public int currentFuel = 0;
    public FuelBar fuelBar;


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
        currentFuel = maxFuel;
        fuelBar.setMaxFuel(maxFuel);

        StartCoroutine(DecreaseFuelOverTime());
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
        if(currentFuel > 0)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.motorTorque = -moveInput * 600 * maxAcceleration * Time.deltaTime;
            }
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

    private IEnumerator DecreaseFuelOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            currentFuel = Mathf.Max(0, currentFuel - 1);
            fuelBar.setFuel(currentFuel);
            if (currentFuel <= 0)
            {
                break;
            }
        }
    }
}