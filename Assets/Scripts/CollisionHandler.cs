using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float sequenceDelay = 2f;
    [SerializeField] AudioClip crashClip;
    [SerializeField] AudioClip victoryClip;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem victoryParticles;

    private int bonusPoints = 300;
    private int landingPoints = 500;
    private float bonusAngleMinimum = 340f;
    private float bonusAngleMaximum = 15f;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool isCollisionDebug = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

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
                if(transform.eulerAngles.z > bonusAngleMinimum || transform.eulerAngles.z < bonusAngleMaximum)
                {
                    EventManager.OnScoreChanged(bonusPoints + landingPoints);
                }
                else 
                {
                    EventManager.OnScoreChanged(landingPoints);
                }
                
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
        Invoke("LoadNextLevel", sequenceDelay * 2.5f);
        
    }

    void StartCrashSequence()
    {
        audioSource.Stop();//stop the thrust sound if it's still playing
        audioSource.PlayOneShot(crashClip);
        crashParticles.Play();
        GetComponent<PlayerMovementHandler>().enabled = false;
        isTransitioning = true;
        Invoke("ReloadCurrentLevel", sequenceDelay);
    }

    private void ReloadCurrentLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 1;
        }
        SceneManager.LoadScene(nextSceneIndex);
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
