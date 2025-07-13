//Key 100% Completion Requirements:

public class WorldManager : MonoBehaviour

{
    public List<World> worlds = new List<World>();
    public List TotalGemnsInGame { get; private set; }

    public struct World
    {
        public int worldID;
        public int totalGems;
        public int totalDragons;
        public int totalEggs;
        public int completed;
        public int totalLevels;
        public int totalEggsCollected;
        public int totalDragonsCollected;
        public int totalEggsInWorld;
        public int totalDragonsInWorld;
        
    }

    public void CalculateTotalGems()
    {
        TotalGemnsInGame = 0;

        foreach(World world in worlds)
        {
            TotalGemnsInGame += world.totalGems;
        }
    }
}