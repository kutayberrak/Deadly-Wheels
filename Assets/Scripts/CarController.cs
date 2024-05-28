using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class CarController : MonoBehaviour
{
    public int maxFuel = 20;
    public int currentFuel = 0;
    public bool isLost;
    public bool isWin;
    public FuelBar fuelBar;
    private Collectible collectible;

    public ParticleSystem smokeEffect1;
    public ParticleSystem smokeEffect2;

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public GameObject wheelEffect;
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

        //StartCoroutine(DecreaseFuelOverTime());
        collectible = transform.GetComponent<Collectible>();
    }

    private void Update()
    {
        GetInput();
        AnimateWheels();
        WheelEffects();
        transform.position = new Vector3(transform.position.x, transform.position.y, 225f);
    }

    private void LateUpdate()
    {
        Move();
    }

    void GetInput()
    {
        moveInput = Input.GetAxis("Vertical");
    }

    void Move()
    {
        if (CheckFuel() && !isWin)
        {
            collectible.enabled = true;
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.motorTorque = -moveInput * 600 * maxAcceleration * Time.deltaTime;
            }
            if(moveInput > 0 && rb.velocity.magnitude > 0)
            {
                if (!smokeEffect1.isPlaying)
                {
                    smokeEffect1.Play();
                    smokeEffect2.Play();
                }
            }
            else
            {
                if (smokeEffect1.isPlaying)
                {
                    smokeEffect1.Stop();
                    smokeEffect2.Stop();
                }

            }
        }
        else
        {
            smokeEffect1.Stop();
            smokeEffect2.Stop();
            collectible.enabled = false;

            float deceleration = 7f; 
            rb.velocity = Vector3.MoveTowards(rb.velocity, Vector3.zero, deceleration * Time.deltaTime);
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.motorTorque = 0 * 600 * maxAcceleration * Time.deltaTime;
            }

            if (CheckVelocity() && !isWin)
            {
                isLost = true;
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

    void WheelEffects()
    {
        foreach (var wheel in wheels)
        {
            var trailRenderer = wheel.wheelEffect?.GetComponentInChildren<TrailRenderer>();
                if (trailRenderer != null)
                {
                    if (rb.velocity.magnitude > 1 && moveInput < 0)
                    {
                        trailRenderer.emitting = true;
                    }
                    else
                    {
                        trailRenderer.emitting = false;
                    }
                }
        }
    }

    public IEnumerator DecreaseFuelOverTime()
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

    private bool CheckFuel()
    {
        return currentFuel != 0;
    }

    private bool CheckVelocity()
    {
        return rb.velocity.magnitude == 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            rb.velocity = Vector3.zero;
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.motorTorque = 0 * 600 * maxAcceleration * Time.deltaTime;
            }
            isWin = true;
        }
    }
}