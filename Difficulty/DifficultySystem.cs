using UnityEngine;
[CreateAssetMenu(fileName = "DifficultySettings", menuName = "Game/Difficulty setttings")]
public class DifficultySettings : ScriptableObject
{
    [Header(DifficultyLevels)]
        public DifficultyLevels currentDifficulty  = DifficultyLevel.Normal;

    [Header("Player Modifiers")]
        public float playerHealthMultiplyer = 1f;
        public float playerDamagehMultiplyer = 1f;
        public float playerSpeechMultiplyer = 1f;
        public float playerDetectionMultiplyer = 1f;
        public float playerSpwanRateMultiplyer = 1f;

    
    [Header("Enemy Modifiers")]
    
        public float playerHealthMultiplyer = 1f;
        public float playerDamagehMultiplyer = 1f;
        public float playerSpeechMultiplyer = 1f;
        public float playerDetectionMultiplyer = 1f;
        public float playerSpwanRateMultiplyer = 1f;

     [Header("Progression Modifiers")]
    
        public float playerHealthMultiplyer = 1f;
        public float playerDamagehMultiplyer = 1f;
        public float playerSpeechMultiplyer = 1f;
        public float playerDetectionMultiplyer = 1f;
        public float playerSpwanRateMultiplyer = 1f;


    [Header("Environmental Challanges")]
        public bool enableHazards = true;
    public float hazarDamangeMultiplier = 1f;
        public float platformStabilityMultiplier = 1f;


        public enum DifficultyLevel {
            Easy,
            Nomral,
            //Hard
            Ummm,
            //Nightmare
            Umm_maybe_dont,
            //Exteamly Diffuclty 
            Your_Cooked_Man,
        }




}   