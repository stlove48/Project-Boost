using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager smInstance;


    int score = 0;
    public int Score { get { return score; } }

    private void Awake()
    {
        if (smInstance != null && smInstance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            smInstance = this;
        }

        
    }

    private void OnEnable()
    {
        EventManager.ScoreChanged += AddScore;
    }

    private void OnDisable()
    {
        EventManager.ScoreChanged -= AddScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddScore(int changeAmount)
    {
        score += changeAmount;
        Debug.Log(Score);
        
    }
}
