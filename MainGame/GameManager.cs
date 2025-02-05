using UnityEngine;
public class GameManager  : MonoBehaviour
{
    public static GameManager = Instance;
    public GemProgressionSystem gemSystem;
    public int score = 0;
    public health = 100;
    public scoreText;
    public healtText;
}

void Awake()
{
    Instance = this
}

public void AddScore(int value)
{
    AddScore += value;
    scoreText = "Gems" + AddScore;
}

public void TakeDamage (int damage)
{
    health  -= damage;
    healthText = "Health" + health
}