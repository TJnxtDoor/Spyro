//Key 100% Completion Requirements:

public class WorldManager : MonoBehaviour

{
    public List<World> worlds = new List<World>();
    public int TotalGemsInGame { get; private set; }

    public struct World{
         public int worldID;
         public int totalGems;
         public int totalDragons;
         public int totalEggs;
         public int completed;
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        CalculateTotalGems();
    }

    public void CalculateTotalGems()
    {
        int total = 0;

        foreach(World world in worlds)
        {
            total += world.totalGems;
        }

        TotalGemsInGame = total;
    }
}