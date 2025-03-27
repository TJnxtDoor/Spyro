using UnityEngine;
using System.Collections.Generic;

public class SkillMastery : MonoBehaviour
{
    public static SkillMastery Instance;
    public List<Skill> skills = new List<Skill>();

    [System.Serializable]
    public class Skill 
    {
        public string skillName;
        public bool isMastered;  // Changed from string to bool
        public int masteryLevel; // Changed from string to int
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: if you want this to persist between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Property to check if all skills are mastered
    public bool AllSkillsMastered => skills.TrueForAll(s => s.isMastered);

    // Property to get total number of skills
    public int TotalSkills => skills.Count;

    // Property to get number of mastered skills
    public int MasteredSkillsCount => skills.FindAll(s => s.isMastered).Count;

    // Method to master a skill by name
    public void MasterSkill(string skillName)
    {
        Skill skill = skills.Find(s => s.skillName == skillName);
        if (skill != null)
        {
            skill.isMastered = true;
        }
    }

    // Method to increase mastery level of a skill
    public void IncreaseMasteryLevel(string skillName, int amount = 1)
    {
        Skill skill = skills.Find(s => s.skillName == skillName);
        if (skill != null)
        {
            skill.masteryLevel += amount;
            
            // Optional: Auto-master if reaching certain level
            if (skill.masteryLevel >= 10) // Example threshold
            {
                skill.isMastered = true;
            }
        }
    }

    // Method to check if a specific skill is mastered
    public bool IsSkillMastered(string skillName)
    {
        Skill skill = skills.Find(s => s.skillName == skillName);
        return skill != null && skill.isMastered;
    }

    // Method to get mastery level of a specific skill
    public int GetMasteryLevel(string skillName)
    {
        Skill skill = skills.Find(s => s.skillName == skillName);
        return skill?.masteryLevel ?? 0;
    }
}