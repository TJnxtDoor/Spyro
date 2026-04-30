//Key 100% Completion Requirements:
using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour

{
    public static WorldManager Instance { get; private set; }
    public static event Action<int> OnWorldCompleted;

    public List<World> worlds = new List<World>();
    public int TotalGemsInGame { get; private set; }
    public int TotalWorlds => worlds.Count;

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

    public void CompleteWorld(int worldID)
    {
        OnWorldCompleted?.Invoke(worldID);
    }
}
