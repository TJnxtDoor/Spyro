using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SkillMastery : MonoBehaviour
{
    public static SkillMastery Instance { get; private set; }

    public List<Skill> skills = new List<Skill>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool AllChallengesComplete => skills.All(s => s.IsMastered);
    public int TotalSkills => skills.Count;

    public void AddSkill(Skill skill)
    {
        skills.Add(skill);
    }

    public void MasterSkill(string skillName)
    {
        var skill = skills.FirstOrDefault(s => s.skillName == skillName);
        if (skill != null)
        {
            skill.IsMastered = true;
            skill.masteryLevel = "Mastered";
        }
    }
}

[System.Serializable]
public class Skill
{
    public string skillName;
    public bool IsMastered;
    public string masteryLevel;
    public int challengesCompleted;
    public int totalChallenges;

    public Skill(string name, int total)
    {
        skillName = name;
        IsMastered = false;
        masteryLevel = "Novice";
        challengesCompleted = 0;
        totalChallenges = total;
    }

    public void CompleteChallenge()
    {
        challengesCompleted++;
        if (challengesCompleted >= totalChallenges)
        {
            IsMastered = true;
            masteryLevel = "Mastered";
        }
    }
}
