// SkinManager.cs
using UnityEngine;
using System.Collections.Generic;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance;

    [System.Serializable]
    public class DragonSkin
    {
        public string skinID;
        public string skinName;
        public Material skinMaterial;
        public Texture skinTexture;
        public bool isUnlocked;
        public bool isEquipped;
        [TextArea] public string unlockDescription;
    }

    public List<DragonSkin> allSkins = new List<DragonSkin>();
    public Renderer playerRenderer;
    private string currentSkinID = "default";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSkinProgress();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ApplySkin(currentSkinID);
    }

    public void UnlockSkin(string skinID)
    {
        DragonSkin skin = allSkins.Find(s => s.skinID == skinID);
        if (skin != null && !skin.isUnlocked)
        {
            skin.isUnlocked = true;
            SaveSkinProgress();
            SkinNotificationUI.Instance.ShowNewSkinUnlocked(skin);
        }
    }

    public void EquipSkin(string skinID)
    {
        DragonSkin skin = allSkins.Find(s => s.skinID == skinID);
        if (skin != null && skin.isUnlocked)
        {
            // Unequip current skin
            allSkins.ForEach(s => s.isEquipped = false);
            
            skin.isEquipped = true;
            currentSkinID = skinID;
            ApplySkin(skinID);
        }
    }

    private void ApplySkin(string skinID)
    {
        DragonSkin skin = allSkins.Find(s => s.skinID == skinID);
        if (skin != null)
        {
            playerRenderer.material = skin.skinMaterial;
            playerRenderer.material.mainTexture = skin.skinTexture;
        }
    }

    private void SaveSkinProgress()
    {
        foreach (DragonSkin skin in allSkins)
        {
            PlayerPrefs.SetInt(skin.skinID + "_unlocked", skin.isUnlocked ? 1 : 0);
        }
        PlayerPrefs.SetString("equipped_skin", currentSkinID);
        PlayerPrefs.Save();
    }

    private void LoadSkinProgress()
    {
        foreach (DragonSkin skin in allSkins)
        {
            skin.isUnlocked = PlayerPrefs.GetInt(skin.skinID + "_unlocked", 0) == 1;
        }
        currentSkinID = PlayerPrefs.GetString("equipped_skin", "default");
    }

    public List<DragonSkin> GetUnlockedSkins()
    {
        return allSkins.FindAll(s => s.isUnlocked);
    }
}