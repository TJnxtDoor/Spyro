using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text scoreText;
    public Text healthText;

    private void UpdateUI()
    {
        scoreText.text = "Score: " + GameManager.Instance.Score;
        healthText.text = "Health: " + GameManager.Instance.Health;
    }
}

public class PlayerModel : MonoBehaviour
{
    public GameObject player3DModel;

    private void Start()
    {
        if (player3DModel != null)
        {
            Instantiate(player3DModel, transform.position, transform.rotation, transform);
            player3DModel.SetActive(true);
            player3DModel.transform.localScale = Vector3.one;
            player3DModel.transform.localPosition = Vector3.zero;
            player3DModel.transform.localRotation = Quaternion.identity;
            player3DModel.AddComponent<PlayerMovement>();
            player3DModel.AddComponent<PlayerUI>();
            player3DModel.AddComponent<PlayerHealth>();
            player3DModel.AddComponent<PlayerScore>();
            player3DModel.AddComponent<PlayerAbilities>();
            player3DModel.AddComponent<PlayerInventory>();
            player3DModel.AddComponent<PlayerCombat>();
            player3DModel.AddComponent<PlayerAnimations>();
            player3DModel.AddComponent<PlayerAudio>();
            player3DModel.AddComponent<PlayerCamera>();
            player3DModel.AddComponent<PlayerInput>();
            player3DModel.AddComponent<PlayerSaveLoad>();
            player3DModel.AddComponent<PlayerDebug>();
            player3DModel.AddComponent<PlayerUI>();
            player3DModel.AddComponent<PlayerModel>();
            player3DModel.AddComponent<PlayerController>();
            player3DModel.AddComponent<PlayerTalking>();
            player3DModel.transform.SetParent(transform);
        }
        UpdateUI();

        GameManager.Instance.OnScoreChanged += UpdateUI;
        GameManager.Instance.OnHealthChanged += UpdateUI;
        GameManager.Instance.OnGamePaused += UpdateUI;
        GameManager.Instance.OnGameResumed += UpdateUI;
        GameManager.Instance.OnGameQuit += UpdateUI;
    }

    private AudioSource walkAudioSource;
    public AudioClip walkClip;
    public audioClip jumpClip;
    public audioClip attackClip;

    public audioClip takeDamageClip;

    private void Awake()
    {
        walkAudioSource = gameObject.AddComponent<AudioSource>();
        walkAudioSource.clip = walkClip;
        walkAudioSource.spatialBlend = 1.0f;
        walkAudioSource.loop = true;
        walkAudioSource.playOnAwake = false;
    }

    public void PlayWalkAudio()
    {
        if (!walkAudioSource.isPlaying && walkClip != null)
        {
            walkAudioSource.Play();
        }
    }

    public void StopWalkAudio()
    {
        if (walkAudioSource.isPlaying)
        {
            walkAudioSource.Stop();
        }
    }
}