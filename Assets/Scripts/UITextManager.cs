using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreTMP;
    [SerializeField] TextMeshProUGUI landingTMP;

    [SerializeField] GameObject scoreAdditionGO;
    TextMeshProUGUI scoreAdditionTMP;

    [SerializeField] GameObject bonusScoreAdditionGO;
    TextMeshProUGUI bonusScoreAdditionTMP;

    [SerializeField] GameObject landingTextGO;

    int bonusScore;
    int baseScore;

    private void Start()
    {
        
        /* if (scoreTMP.text == "")
        {
            scoreTMP.text = "Your score: 0";
        } */
        
        UpdateScoreText(ScoreManager.smInstance.Score);
        ResetLandingText(true);

        scoreAdditionTMP = scoreAdditionGO.GetComponent<TextMeshProUGUI>();
        scoreAdditionTMP.text = "";

        bonusScoreAdditionTMP = bonusScoreAdditionGO.GetComponent<TextMeshProUGUI>();
        bonusScoreAdditionTMP.text = "";
    }

    private void OnEnable()
    {
        EventManager.ScoreChanged += ProcessScoreChange;
        EventManager.ShipLanded += ShowLandingRating;
        EventManager.FuelBonusEarned += UpdateBonusScoreText;
        EventManager.BaseScoreEarned += UpdateBaseScoreText;
        EventManager.LevelLoaded += ResetLandingText;
    }

    private void OnDisable()
    {
        EventManager.ScoreChanged -= ProcessScoreChange;
        EventManager.ShipLanded -= ShowLandingRating;
        EventManager.FuelBonusEarned -= UpdateBonusScoreText;
        EventManager.BaseScoreEarned -= UpdateBaseScoreText;
        EventManager.LevelLoaded -= ResetLandingText;
    }

    void ResetLandingText(bool levelLoaded)
    {
        landingTextGO.SetActive(false);
    }

    void UpdateScoreText(int score)
    {
        scoreTMP.text = $"Your score: {ScoreManager.smInstance.Score + score}";
    }

    void ShowLandingRating(landingRating landingRating)
    {
        if (!landingTextGO.activeSelf)
        {
            landingTextGO.SetActive(true);
            switch (landingRating)
            {
                case landingRating.BAD:
                    landingTMP.text = $"What a rough landing...";
                    break;
                case landingRating.GOOD:
                    landingTMP.text = $"That was a solid landing!";
                    break;
                case landingRating.PERFECT:
                    landingTMP.text = $"Absolutely textbook landing. Well done!";
                    break;
            }
        }
        
    }

    IEnumerator ShowFloatingScore(int score)
    {
        scoreAdditionGO.SetActive(true);
        scoreAdditionTMP.text = $"+{score}";
        yield return new WaitForSeconds(2f);
        scoreAdditionGO.SetActive(false);
    }

    IEnumerator ShowFloatingBonusScore(int score)
    {
        bonusScoreAdditionGO.SetActive(true);
        bonusScoreAdditionTMP.text = $"Bonus: +{score}";
        yield return new WaitForSeconds(2f);
        bonusScoreAdditionGO.SetActive(false);
    }

    // Scoring delegate
    void ProcessScoreChange(int score)
    {
        UpdateScoreText(score);
        StartCoroutine(ShowFloatingScore(baseScore));
        StartCoroutine(ShowFloatingBonusScore(bonusScore));
    }

    // Points coming from FuelBonus currently, calculated in ScoreManager/CalculateFuelBonus
    void UpdateBonusScoreText(int score)
    {
        bonusScore = score;
    }

    // Points coming from Base Score currently, calculated in ScoreManager/CalculateScore
    void UpdateBaseScoreText(int score)
    {
        baseScore = score;
    }
}
