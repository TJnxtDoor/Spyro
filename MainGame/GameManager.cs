using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GemProgressionSystem gemSystem;
    public int score = 0;
    public int health = 100;
    public UnityEngine.UI.Text scoreText;
    public UnityEngine.UI.Text healthText;
    public int Addscore = 0;
    

    void Awake()
    {
        Instance = this;
    }

    public void AddScore(int value)
    {
        score += value;
        if (scoreText != null) scoreText.text = "Gems: " + score;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (healthText != null) healthText.text = "Health: " + health;
    }
}