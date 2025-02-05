// SkinCollectible.cs
using UnityEngine;

public class SkinCollectible : MonoBehaviour
{
    public string skinID;
    public ParticleSystem unlockEffect;
    public AudioClip collectSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectSkin();
        }
    }

    private void CollectSkin()
    {
        SkinManager.Instance.UnlockSkin(skinID);
        Instantiate(unlockEffect, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        Destroy(gameObject);
    }
}