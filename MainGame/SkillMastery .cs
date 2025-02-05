public class SkillMastery : MonoBehaviour
{
    public static SkillMastery Instance;
    public List<Skill> skills = mew List<Skill>();

    [System.Serializable]
    public class Skill {
        public string skillName;
        public string IsMastered;
        public string masteryLevel;
    }


     public bool AllChallengesComplete => skills.TrueForAll(s => s.isMastered);
     public int TotalSkills => skills.count;
}