using System;
using System.Collections.Generic;
using UnityEngine;

public class PlantBag : MonoBehaviour
{
    [SerializeField] private List<Plant> availailablePlantTypes;
    [SerializeField] private PlantSelectionUI ui;
    private Dictionary<Plant, int> seedCounts;
    private const int DEFAULT_MIN_SEED_COUNT = 50;
    private void Awake()
    {
        seedCounts = new Dictionary<Plant, int>();
        foreach (var type in availailablePlantTypes)
        {
            seedCounts.Add(type, DEFAULT_MIN_SEED_COUNT);
        }
    }

    public List<Plant> GetTypes()
    {
        return availailablePlantTypes;
    }

    public Boolean HasSeedsOf(Plant type)
    {
        return seedCounts[type] > 0;
    }

    private void ChangeSeedCoundOf(Plant type, int delta)
    {
        seedCounts[type] += delta;
        if (delta != 0)
        {
            ui.UpdateSeedCounts(GetSeedCounts());
        }   
    }

    public Dictionary<Plant, int> GetSeedCounts()
    {
        return new Dictionary<Plant, int>(seedCounts);
    }
    
    public void DecreaseSeedsOf(Plant type, int delta = 1)
    {
        ChangeSeedCoundOf(type, -delta);
    }

    public void IncreaseSeedsOf(Plant type, int delta = 1)
    {
        ChangeSeedCoundOf(type, delta);
    }

    public int GetSeedCountOf(Plant type)
    {
        return seedCounts[type];
    }

}