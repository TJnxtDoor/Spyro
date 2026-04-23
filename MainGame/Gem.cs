// GemCollectible.cs (updated)
using UnityEngine;

public class GemCollectible : MonoBehaviour
{
    public static System.Action<int> OnGemCollected;

    [SerializeField] private ParticleSystem collectEffect;
    private int gemValue;

    public void SetValue(int value)
    
    {
        gemValue = value;
        GetComponent<Renderer>().material.color = Color.HSVToRGB(
            Mathf.InverseLerp(100, 100000, value), 1, 1
        );
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(collectEffect, transform.position, Quaternion.identity);
            GemProgressionSystem.Instance.AddGems(gemValue);
            OnGemCollected?.Invoke(gemValue);

            Destroy(gameObject);
        }
    }
}