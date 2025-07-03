using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreTMP;

    private void Start()
    {
        
        /* if (scoreTMP.text == "")
        {
            scoreTMP.text = "Your score: 0";
        } */
        
        UpdateScoreText(ScoreManager.smInstance.Score);
    }

    private void OnEnable()
    {
        EventManager.ScoreChanged += UpdateScoreText;
    }

    private void OnDisable()
    {
        EventManager.ScoreChanged -= UpdateScoreText;
    }

    void Update()
    {
        
    }

    void UpdateScoreText(int score)
    {
        scoreTMP.text = $"Your score: {ScoreManager.smInstance.Score + score}";
    }
}
