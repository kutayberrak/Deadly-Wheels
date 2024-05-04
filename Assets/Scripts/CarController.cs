using UnityEngine;
using System;
using System.Collections.Generic;
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

    private float moveInput;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentFuel = maxFuel;
        fuelBar.setMaxFuel(maxFuel);

        StartCoroutine(DecreaseFuelOverTime());
    }

    private void Update()
    {
        GetInput();
        AnimateWheels();
    }

    private void LateUpdate()
    {
        Move();
    }

    void GetInput()
    {
        moveInput = - Input.GetAxis("Vertical");
    }

    void Move()
    {
        if (currentFuel > 0)
        {

            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * Time.deltaTime;
            }
        }
    }

    void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
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