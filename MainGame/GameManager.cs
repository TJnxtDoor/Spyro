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
        AddScore += value;
        scoreText = "Gems" + AddScore;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthText = "Health" + health;
    }

}