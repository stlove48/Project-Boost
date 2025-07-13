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

    private void Start()
    {
        
        /* if (scoreTMP.text == "")
        {
            scoreTMP.text = "Your score: 0";
        } */
        
        UpdateScoreText(ScoreManager.smInstance.Score);
        landingTMP.text = "";

        scoreAdditionTMP = scoreAdditionGO.GetComponent<TextMeshProUGUI>();
        scoreAdditionTMP.text = "";
    }

    private void OnEnable()
    {
        EventManager.ScoreChanged += ProcessScoreChange;
        EventManager.ShipLanded += ShowLandingRating;
    }

    private void OnDisable()
    {
        EventManager.ScoreChanged -= ProcessScoreChange;
        EventManager.ShipLanded -= ShowLandingRating;
    }

    void Update()
    {
        
    }

    void UpdateScoreText(int score)
    {
        scoreTMP.text = $"Your score: {ScoreManager.smInstance.Score + score}";
    }

    void ShowLandingRating(landingRating landingRating)
    {
        switch (landingRating)
        {
            case landingRating.BAD:
                landingTMP.text = "What a rough landing...";
                break;
            case landingRating.GOOD:
                landingTMP.text = "That was a solid landing!";
                break;
            case landingRating.PERFECT:
                landingTMP.text = "Absolutely textbook landing. Well done!";
                break;
        }
    }

    IEnumerator ShowFloatingScore(int score)
    {
        scoreAdditionGO.SetActive(true);
        scoreAdditionTMP.text = $"+{score}";
        yield return new WaitForSeconds(2f);
        scoreAdditionGO.SetActive(false);
    }

    // Scoring delegate
    void ProcessScoreChange(int score)
    {
        UpdateScoreText(score);
        StartCoroutine(ShowFloatingScore(score));
    }
}
