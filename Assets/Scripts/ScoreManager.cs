using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum landingRating { BAD, GOOD, PERFECT};

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager smInstance;

    private float scoreMultiplier = 1.0f; // Default value
    private int baseScore = 500; // Base score for a successful landing

    int score = 0;
    public int Score { get { return score; } }

    int fuelBonus = 0;

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
        EventManager.ShipLanded += CalculateScore;
        EventManager.ScoreChanged += AddScore;
    }

    private void OnDisable()
    {
        EventManager.ShipLanded -= CalculateScore;
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

    void CalculateScore (landingRating landingRating)
    {

        switch (landingRating)
        {
            case landingRating.PERFECT:
                scoreMultiplier = 1.5f;
                break;
            case landingRating.GOOD:
                scoreMultiplier = 1.0f;
                break;
            case landingRating.BAD:
                scoreMultiplier = 0.2f;
                break;
        }

        CalculateFuelBonus();

        EventManager.OnBaseScoreEarned(Mathf.FloorToInt(baseScore * scoreMultiplier));
        EventManager.OnFuelBonusEarned(Mathf.FloorToInt(fuelBonus * scoreMultiplier));
        EventManager.OnScoreChanged(Mathf.FloorToInt((baseScore + fuelBonus) * scoreMultiplier));
        
        fuelBonus = 0;
    }

    void CalculateFuelBonus()
    {
        float remainingFuel = FindObjectOfType<FuelManager>().DetermineRemainingFuel();
        fuelBonus = Mathf.RoundToInt(remainingFuel / 20) * 50;



        Debug.Log($"Remaining Fuel: {remainingFuel}\nFuel bonus: {fuelBonus}");
    }
}
