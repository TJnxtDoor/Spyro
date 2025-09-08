public class SkillMastery : MonoBehaviour
{
    public static SkillMastery Instance;

    }

[System.Serializable]
public class Skill
{
    public string skillName;
    public string IsMastered;
    public string masteryLevel;
    public int  challengesCompleted;



    public bool AllChallengesComplete => skills.TrueForAll(s => s.isMastered);
    public int TotalSkills => skills.count;
}