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
        public bool isMastered;
        public int masteryLevel;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool AllSkillsMastered => skills.TrueForAll(s => s.isMastered);

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

            if (skill.masteryLevel >= 10)
            {
                skill.isMastered = true;
            }
        }
    }

    public bool IsSkillMastered(string skillName)
    {
        Skill skill = skills.Find(s => s.skillName == skillName);
        return skill != null && skill.isMastered;
    }

    public int GetMasteryLevel(string skillName)
    {
        Skill skill = skills.Find(s => s.skillName == skillName);
        return skill?.masteryLevel ?? 0;
    }
}