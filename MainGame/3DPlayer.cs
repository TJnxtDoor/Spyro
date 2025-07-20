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
         

    }
}