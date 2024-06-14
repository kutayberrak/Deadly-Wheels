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
    public bool isNitroPurchased;
    private bool isGrounded;
    public FuelBar fuelBar;
    public NitrousBar nitrousBar;
    private Collectible collectible;

    public ParticleSystem smokeEffect1;
    public ParticleSystem smokeEffect2;
    private Color originalSmokeColor;

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

    public float nitrousDuration = 10.0f;  
    public float remainingNitrous;
    private bool isUsingNitrous = false;
    public float nitrousBoost = 5.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentFuel = maxFuel;
        fuelBar.setMaxFuel(maxFuel);

        collectible = transform.GetComponent<Collectible>();

        originalSmokeColor = smokeEffect1.main.startColor.color;
        remainingNitrous = nitrousDuration;
        nitrousBar.SetMaxNitrous(nitrousDuration);
    }

    private void Update()
    {
        GetInput();
        AnimateWheels();
        transform.position = new Vector3(transform.position.x, transform.position.y, 225f);

        nitrousBar.SetNitrous(remainingNitrous);

        CheckGrounded();

        Debug.Log(GetComponent<Rigidbody>().velocity);
    }

    private void LateUpdate()
    {
        Move();
    }

    
    void GetInput()
    {
        moveInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space) && isNitroPurchased)
        {
            if (!isUsingNitrous && remainingNitrous > 0)
            {
                StartCoroutine(UseNitrous());
            }
        }
        else
        {
            isUsingNitrous = false;
        }
    }

    void Move()
    {
        if (CheckFuel() && !isWin)
        {
            collectible.enabled = true;
            foreach (var wheel in wheels)
            {
                float appliedAcceleration = isUsingNitrous ? maxAcceleration * nitrousBoost : maxAcceleration;
                if (gameObject.tag == "Vehicle")
                {
                    wheel.wheelCollider.motorTorque = -moveInput * 600 * appliedAcceleration * Time.deltaTime;
                }
                else
                {
                    wheel.wheelCollider.motorTorque = moveInput * 600 * appliedAcceleration * Time.deltaTime;
                }
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

            if (isGrounded)
            {
                rb.velocity = Vector3.MoveTowards(rb.velocity, Vector3.zero, deceleration * Time.deltaTime);
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
            
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.motorTorque = 0;
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

    void BrakeEffect()
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

    private bool IsAnyWheelOnGround()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.wheelCollider.isGrounded)
            {
                return true;
            }
        }
        return false;
    }

  

    private void CheckGrounded()
    {
        isGrounded = true; 
        

        foreach (var wheel in wheels)
        {
            if (!wheel.wheelCollider.isGrounded)
            {
                isGrounded = false;
                break; 
            }
        }

        if (!isGrounded)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float rotationSpeed = 50f; // Adjust the rotation speed as needed
            transform.Rotate(Vector3.left, horizontalInput * rotationSpeed * Time.deltaTime);

            if (IsAnyWheelOnGround())
            {
                rb.constraints &= ~RigidbodyConstraints.FreezeRotationX; 
            }
            else
            {
                rb.constraints |= RigidbodyConstraints.FreezeRotationX; 
            }

            StartCoroutine(FillNitrous());


            Vector3 localEulerAngles = transform.localEulerAngles;
            localEulerAngles.y = -90f;
            localEulerAngles.z = 0f;
            transform.localEulerAngles = localEulerAngles;
        }
        else
        { 
            BrakeEffect();
        }
    }
    IEnumerator FillNitrous()
    {
        float nitrousFillSpeed = 0.001f;
        while (remainingNitrous <= nitrousDuration)
        {
            if (!isGrounded)
            {
                remainingNitrous += nitrousFillSpeed * Time.deltaTime;
            }
            yield return null;
        }
    }


    IEnumerator UseNitrous()
    {
        isUsingNitrous = true;
        ChangeSmokeColor(Color.blue);
        while (isUsingNitrous && remainingNitrous > 0)
        {
            remainingNitrous -= Time.deltaTime;
            yield return null;
        }
        isUsingNitrous = false;
        ChangeSmokeColor(originalSmokeColor);  
    }

    

    private void ChangeSmokeColor(Color color)
    {
        var main1 = smokeEffect1.main;
        main1.startColor = color;
        var main2 = smokeEffect2.main;
        main2.startColor = color;
    }
}