using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

[Serializable]
public class GameData
{
    public Vector3 playerPosition;
    
    public float playerHealth;
    public List<string> inventoryItems;
    public int currentLevel;
    public float ProgressPercentage;
    public string saveName;
    public DateTime saveTime;
    public string gameVersion;
}

public static class SaveSystem
{
    private static readonly string saveDirectory = Application.persistentDataPath + "/saves/";

    public static void SaveGame(GameData data, string saveName = "quicksave")
    {
        // Create directory if it doesn't exist
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }

        string sanitizedName = SanitizeFileName(saveName);
        string savePath = Path.Combine(saveDirectory, $"{sanitizedName}.save");

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(savePath, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log($"Game saved as: {sanitizedName}");
    }

    public static GameData LoadGame(string saveName)
    {
        string sanitizedName = SanitizeFileName(saveName);
        string savePath = Path.Combine(saveDirectory, $"{sanitizedName}.save");

        if (!File.Exists(savePath))
        {
            Debug.LogError($"Save file {sanitizedName} not found!");
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(savePath, FileMode.Open);

        GameData data = formatter.Deserialize(stream) as GameData;
        stream.Close();

        return data;
    }

    public static void DeleteSave(string saveName)
    {
        string sanitizedName = SanitizeFileName(saveName);
        string savePath = Path.Combine(saveDirectory, $"{sanitizedName}.save");

        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log($"Deleted save: {sanitizedName}");
        }
    }

    private static string SanitizeFileName(string name)
    {
        // Remove invalid characters
        string invalidChars = new string(Path.GetInvalidFileNameChars());
        Regex removeInvalidChars = new Regex($"[{Regex.Escape(invalidChars)}]");
        return removeInvalidChars.Replace(name, "");
    }

    // Get all existing save files
    public static List<string> GetSaveFiles()
    {
        if (!Directory.Exists(saveDirectory)) return new List<string>();

        return Directory.GetFiles(saveDirectory, "*.save")
            .Select(Path.GetFileNameWithoutExtension)
            .ToList();
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform player;

    private GameData currentGameData;
    private AutoSaveSystem autoSaveSystem;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            autoSaveSystem = gameObject.AddComponent<AutoSaveSystem>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame(string saveName = "quicksave")
    {
        if (!CanSave()) return;

        currentGameData = new GameData
        {
            playerPosition = player.position,
            playerHealth = PlayerHealth.CurrentHealth,
            inventoryItems = Inventory.Items,
            currentLevel = SceneManager.GetActiveScene().buildIndex,
            saveName = saveName,
            saveTime = DateTime.Now,
            gameVersion = Application.version
        };

        SaveSystem.SaveGame(currentGameData, saveName);
        Debug.Log("Game saved!");
    }

    public void LoadGame(string saveName = "quicksave")
    {
        currentGameData = SaveSystem.LoadGame(saveName);

        if (currentGameData != null)
        {
            player.position = currentGameData.playerPosition;
            PlayerHealth.CurrentHealth = currentGameData.playerHealth;
            Inventory.Items = currentGameData.inventoryItems;
            SceneManager.LoadScene(currentGameData.currentLevel);
            Debug.Log("Game loaded!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
    }

    // Prevents saving during gameplay moments 
    public bool CanSave()
    {
        return !IsInCombat() && !IsInCutscene() && !IsGamePaused();
    }

    private bool IsInCombat()
    {
        // Implement your combat check logic here
        return false;
    }

    private bool IsInCutscene()
    {
        // Implement your cutscene check logic here
        return false;
    }

    private bool IsGamePaused()
    {
        // Implement your pause check logic here
        return false;
    }
}

public class AutoSaveSystem : MonoBehaviour
{
    [Header("Auto-Save Settings")]
    [SerializeField] private float autoSaveInterval = 300f; // 5 minutes
    [SerializeField] private bool enableAutoSave = true;
    [SerializeField] private bool saveOnExit = true;
    [SerializeField] private int maxAutoSaves = 5;

    private Coroutine autoSaveCoroutine;

    private void Start()
    {
        LoadSettings();
        
        if (enableAutoSave)
        {
            StartAutoSave();
        }
    }

    public void StartAutoSave()
    {
        if (autoSaveCoroutine == null)
        {
            autoSaveCoroutine = StartCoroutine(AutoSaveRoutine());
        }
    }

    public void StopAutoSave()
    {
        if (autoSaveCoroutine != null)
        {
            StopCoroutine(autoSaveCoroutine);
            autoSaveCoroutine = null;
        }
    }

    private IEnumerator AutoSaveRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveInterval);
            SaveGame();
            Debug.Log("Game auto-saved at: " + System.DateTime.Now);
        }
    }

    private void SaveGame()
    {
        if (GameManager.Instance.CanSave())
        {
            GameData currentData = new GameData
            {
                playerPosition = GameManager.Instance.player.position,
                playerHealth = PlayerHealth.CurrentHealth,
                inventoryItems = Inventory.Items,
                currentLevel = SceneManager.GetActiveScene().buildIndex,
                saveName = $"autosave_{DateTime.Now:yyyyMMdd_HHmmss}",
                saveTime = DateTime.Now,
                gameVersion = Application.version
            };

            SaveSystem.SaveGame(currentData, currentData.saveName);
            RotateAutoSaves(maxAutoSaves);
        }
    }

    public void RotateAutoSaves(int maxAutoSaves = 5)
    {
        var saves = SaveSystem.GetSaveFiles()
            .Where(name => name.StartsWith("autosave"))
            .OrderByDescending(name => name)
            .ToList();

        while (saves.Count > maxAutoSaves)
        {
            string oldest = saves.Last();
            SaveSystem.DeleteSave(oldest);
            saves.Remove(oldest);
        }
    }

    private void OnApplicationQuit()
    {
        if (saveOnExit)
        {
            SaveGame();
            Debug.Log("Game saved on exit");
        }

        SaveSettings();
        Console.WriteLine("Game Is Saved!");
        Console.ReadLine();

    
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (enableAutoSave)
        {
            SaveGame();
            Debug.Log("Auto-saved after scene load");
        }
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("AutoSaveInterval", autoSaveInterval);
        PlayerPrefs.SetInt("AutoSaveEnabled", enableAutoSave ? 1 : 0);
        PlayerPrefs.SetInt("SaveOnExit", saveOnExit ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SaveGame(int slotId, GameSaveData data)
    {
        string filePath = Path.Combine(Application.persistentDataPath, $"SaveData_Slot{slotId}.bin");
        string json = JsonConvert.SerializeObject(data); // Using Newtonsoft.Json for serialization
        File.WriteAllText(filePath, json);
        FileWtittenAllText(filePath, json);
    }

    public GameSaveData LoadGame(int slotId)
{
    string filePath = Path.Combine(Application.persistentDataPath, $"SaveData_Slot{slotId}.json");
    if (File.Exists(filePath))
    {
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<GameSaveData>(json); // Using Newtonsoft.Json
    }
    return null; // Return null if the file doesn't exist
}

    private void LoadSettings()
    {
        autoSaveInterval = PlayerPrefs.GetFloat("AutoSaveInterval", 300f);
        enableAutoSave = PlayerPrefs.GetInt("AutoSaveEnabled", 1) == 1;
        saveOnExit = PlayerPrefs.GetInt("SaveOnExit", 1) == 1;
    }
}

public class SaveSlotManager : MonoBehaviour
{
    private const int MAX_SLOTS = 3;

    public void SaveToSlot(int slotIndex)
    {
        if (slotIndex < 1 || slotIndex > MAX_SLOTS) return;

        string slotName = $"slot{slotIndex}";
        GameManager.Instance.SaveGame(slotName);
    }

    public void LoadFromSlot(int slotIndex)
    {
        if (slotIndex < 1 || slotIndex > MAX_SLOTS) return;

        string slotName = $"slot{slotIndex}";
        GameManager.Instance.LoadGame(slotName);
    }
}

// Placeholder classes for reference
public static class PlayerHealth
{
    public static float CurrentHealth { get; set; }
}

public static class Inventory
{
    public static List<string> Items { get; set; } = new List<string>();
}

