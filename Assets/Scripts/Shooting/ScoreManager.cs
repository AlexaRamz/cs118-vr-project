using System;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int score { get; private set; }
    [SerializeField] private TMP_Text scoreText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void IncrementScore()
    {
        score++;
        Debug.Log("Score is now: " + score);
        scoreText.text = "Score: " + score;
    }
}
