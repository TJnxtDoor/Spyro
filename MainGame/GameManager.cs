using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GemProgressionSystem gemSystem;
    public DifficultySettings difficultySettings;
    public int score = 0;
    public int health = 100;
    public UnityEngine.UI.Text scoreText;
    public UnityEngine.UI.Text healthText;
    public int Addscore = 0;

    public event Action ScoreChanged;
    public event Action HealthChanged;

    void Awake()
    {
        Instance = this;
    }

    public void AddScore(int value)
    {
        score += value;
        if (scoreText != null) scoreText.text = "Gems: " + score;
        ScoreChanged?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (healthText != null) healthText.text = "Health: " + health;
        HealthChanged?.Invoke();
    }
}
