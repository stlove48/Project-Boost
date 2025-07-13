using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    [SerializeField] private float verticalThrust = 1000f;
    [SerializeField] private float rotationThrust = 200f;
    [SerializeField] AudioClip thrustClip;

    [SerializeField] ParticleSystem mainThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;

    [SerializeField] FuelManager fuelManager;

    
    Rigidbody rb;
    AudioSource audioSource;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        
    }

    
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!fuelManager.OutOfFuel)
            {
                //Thrusting
                StartThrusting();
                fuelManager.ConsumeFuel();
            }
            else
            {
                StopThrusting();
            }
            
        }
        else
        {
            StopThrusting();
        }

    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //Rotate left
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //Rotate right
            RotateRight();
        }
        else
        {
            leftThrusterParticles.Stop();
            rightThrusterParticles.Stop();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * verticalThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustClip);
        }

        if (!mainThrusterParticles.isPlaying)
        {
            mainThrusterParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainThrusterParticles.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);

        leftThrusterParticles.Stop();
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);

        rightThrusterParticles.Stop();
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }

    void ApplyRotation(float rotationDirection)
    {
        
        //Freeze rotation to manually change the rotation via keypress instead of using physics system.
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationDirection * Time.deltaTime);
        rb.freezeRotation = false;
    }

    
}
