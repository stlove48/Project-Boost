using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip crashClip;
    [SerializeField] AudioClip victoryClip;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem victoryParticles;

    // If the rocket lands between 90 and 46 degrees off straight down
    private float badScoreAngleMin = 290f;
    private float badScoreAngleMax = 70f;

    // If the rocket lands between 45 and 16 degrees off straight down
    private float goodScoreAngleMin = 330f;
    private float goodScoreAngleMax = 30f;

    // If the rocket lands between 5 and 0 degrees off straight down
    private float perfectScoreAngleMin = 355f;
    private float perfectScoreAngleMax = 5f;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool isCollisionDebug = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Debugging
        if (Input.GetKeyDown(KeyCode.L))
        {
            EventManager.OnLevelCompleted(SceneManager.GetActiveScene().buildIndex);
        }

        // Debugging
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCollision();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || isCollisionDebug) { return; }
        
        DetermineCollisionType(other);
    }

    private void DetermineCollisionType(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Unfriendly":
                StartCrashSequence();
                break;
            case "Fuel":
                break;
            case "Finish":


                if(transform.eulerAngles.z > perfectScoreAngleMin || transform.eulerAngles.z < perfectScoreAngleMax)
                {
                    EventManager.OnShipLanded(landingRating.PERFECT);
                    Debug.Log("Perfect landing!");
                }
                else if (transform.eulerAngles.z > goodScoreAngleMin || transform.eulerAngles.z < goodScoreAngleMax)
                {
                    EventManager.OnShipLanded(landingRating.GOOD);
                    Debug.Log("Good landing!");
                }
                else if (transform.eulerAngles.z > badScoreAngleMin || transform.eulerAngles.x < badScoreAngleMax)
                {
                    EventManager.OnShipLanded(landingRating.BAD);
                    Debug.Log("Bad landing!");
                }
                else
                {
                    Debug.Log("Crash angle: " + transform.eulerAngles.z);
                    StartCrashSequence();
                    break;
                }

                Debug.Log("Landing angle: " + transform.eulerAngles.z);

                StartVictorySequence();
                break;
            default:
                break;
        }
    }

    void StartVictorySequence()
    {
        
        audioSource.Stop(); //stop the thrust sound if it's still playing
        audioSource.PlayOneShot(victoryClip);
        victoryParticles.Play();
        GetComponent<PlayerMovementHandler>().enabled = false;
        isTransitioning = true;
        EventManager.OnLevelCompleted(SceneManager.GetActiveScene().buildIndex);
        
    }

    void StartCrashSequence()
    {
        audioSource.Stop();//stop the thrust sound if it's still playing
        audioSource.PlayOneShot(crashClip);
        crashParticles.Play();
        GetComponent<PlayerMovementHandler>().enabled = false;
        isTransitioning = true;
        EventManager.OnLevelFailed(SceneManager.GetActiveScene().buildIndex);
    }

    void ToggleCollision()
    {

        isCollisionDebug = !isCollisionDebug; //toggle collision determination
        
        /*if (!isCollisionDebug)
        {
            GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Collisions are off");
            isCollisionDebug = true;
        }
        else
        {
            GetComponent<BoxCollider>().enabled = true;
            Debug.Log("Collisions are on");
            isCollisionDebug = false;
        }
        */
    }
}
