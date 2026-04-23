using UnityEngine;
using UnityEngine.UI;
using System;

public class Player3DController : MonoBehaviour
{
    [Header("Model Settings")]
    [SerializeField] private GameObject playerModelPrefab;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip walkClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip attackClip;
    [SerializeField] private AudioClip takeDamageClip;

    [Header("UI References")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text healthText;

    private AudioSource walkAudioSource;
    private GameObject instantiatedModel;


    private void Awake()
    {
        InitializeAudio();
    }

    private void Start()
    {
        InstantiatePlayerModel();
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
        CleanupModel();
    }



    private void InitializeAudio()
    {
        walkAudioSource = gameObject.AddComponent<AudioSource>();
        walkAudioSource.clip = walkClip;
        walkAudioSource.spatialBlend = 1.0f;
        walkAudioSource.loop = true;
        walkAudioSource.playOnAwake = false;
    }

    private void InstantiatePlayerModel()
    {
        if (playerModelPrefab == null) return;

        instantiatedModel = Instantiate(playerModelPrefab, transform.position, transform.rotation, transform);
        instantiatedModel.SetActive(true);
        
        ResetModelTransform();
        AddPlayerComponents();
    }

    private void ResetModelTransform()
    {
        if (instantiatedModel == null) return;

        instantiatedModel.transform.localScale = Vector3.one;
        instantiatedModel.transform.localPosition = Vector3.zero;
        instantiatedModel.transform.localRotation = Quaternion.identity;
    }

    private void AddPlayerComponents()
    {
        if (instantiatedModel == null) return;

        Type[] components = new Type[]
        {
            typeof(PlayerMovement),
            typeof(PlayerHealth),
            typeof(PlayerScore),
            typeof(PlayerAbilities),
            typeof(PlayerInventory),
            typeof(PlayerCombat),
            typeof(PlayerAnimations),
            typeof(PlayerAudio),
            typeof(PlayerCamera),
            typeof(PlayerInput),
            typeof(PlayerSaveLoad),
            typeof(PlayerDebug),
            typeof(PlayerController),
            typeof(PlayerTalking)
        };

        foreach (Type component in components)
        {
            if (instantiatedModel.GetComponent(component) == null)
            {
                instantiatedModel.AddComponent(component);
            }
        }
    }



    private void SubscribeToEvents()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.OnScoreChanged += HandleScoreChanged;
        GameManager.Instance.OnHealthChanged += HandleHealthChanged;
        GameManager.Instance.OnGamePaused += HandleGamePaused;
        GameManager.Instance.OnGameResumed += HandleGameResumed;
        GameManager.Instance.OnGameQuit += HandleGameQuit;
    }

    private void UnsubscribeFromEvents()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.OnScoreChanged -= HandleScoreChanged;
        GameManager.Instance.OnHealthChanged -= HandleHealthChanged;
        GameManager.Instance.OnGamePaused -= HandleGamePaused;
        GameManager.Instance.OnGameResumed -= HandleGameResumed;
        GameManager.Instance.OnGameQuit -= HandleGameQuit;
    }

    private void HandleScoreChanged()
    {
        UpdateScoreUI();
    }

    private void HandleHealthChanged()
    {
        UpdateHealthUI();
    }

    private void HandleGamePaused()
    {
        // Handle pause state
    }

    private void HandleGameResumed()
    {
        // Handle resume state
    }

    private void HandleGameQuit()
    {
        // Handle quit cleanup
    }



    private void UpdateScoreUI()
    {
        if (scoreText != null && GameManager.Instance != null)
        {
            scoreText.text = "Score: " + GameManager.Instance.Score;
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null && GameManager.Instance != null)
        {
            healthText.text = "Health: " + GameManager.Instance.Health;
        }
    }




    public void PlayWalkSound()
    {
        if (!walkAudioSource.isPlaying && walkClip != null)
        {
            walkAudioSource.Play();
        }
    }

    public void StopWalkSound()
    {
        if (walkAudioSource.isPlaying)
        {
            walkAudioSource.Stop();
        }
    }

    public void PlayJumpSound()
    {
        PlaySound(jumpClip);
    }

    public void PlayAttackSound()
    {
        PlaySound(attackClip);
    }

    public void PlayTakeDamageSound()
    {
        PlaySound(takeDamageClip);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
    }



    private void CleanupModel()
    {
        if (instantiatedModel != null)
        {
            Destroy(instantiatedModel);
        }
    }

};