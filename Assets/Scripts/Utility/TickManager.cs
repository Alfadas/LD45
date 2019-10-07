using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    [SerializeField] GlobalInformation global;
    [SerializeField] float intervall = 1;

    bool pause;

    public bool Pause
    {
        get { return pause; }
        set { pause = value; }
    }

    public void TogglePause()
    {
        Pause = !pause;
    }

    void Start()
    {
        StartCoroutine(Ticker());
    }

    protected IEnumerator Ticker() 
    {
        yield return new WaitForSeconds(intervall);
        while (true)
        {
            if (pause)
            {
                yield return new WaitWhile(() => pause);
            }

            Tick();
            yield return new WaitForSeconds(intervall);
        }
    }

    private void Tick()
    {
        global.daytimeController.ProgressDay();
        int windResistanceSum = 0;
        foreach (Tile tile in global.tiles)
        {
            windResistanceSum += tile.PlantWindResistance;
        }
        global.weatherController.MoveClouds();
        int localWind = global.weatherController.CalcLocalWind(windResistanceSum);

        int displayLayer = global.displaLayerManager.Selected;
        bool layerChanged = false;
        if (displayLayer != 4)
        {
            layerChanged = true;
        }
        List<Plant> eatables = new List<Plant>();
        foreach (Tile tile in global.tiles)
        {
            TileTick(tile, displayLayer, layerChanged);
            if (tile.HasPlant)
            {
                PlantTick(tile.Plant, localWind);
                if (tile.Plant.Eatable > 0 && tile.Plant.isGrownUp())
                {
                    eatables.Add(tile.Plant);
                }
            }
        }
        if (eatables.Count > 0)
        {
            int breedChances = global.animalManager.FeedAnimals(eatables.Sum(item => item.AnimalAttraction));

            for (int i = 0; i <= breedChances; i++)
            {
                GetRandomEmptyTile().PlantPlantByReproduction(eatables[Random.Range(0, eatables.Count)]);
            }
        }
    }

    private Tile GetRandomEmptyTile()
    {
        int maxIndex = global.worldGridLength - 1;
        int row = Random.Range(-1, maxIndex -1);
        int col = Random.Range(-1, maxIndex -1);
        if (global.tiles[row, col].HasPlant)
        {
            return GetRandomEmptyTile();
        }
        return global.tiles[row, col];
    }

    private Tile getRandomTileAround(Tile tile)
    {
        int rowDelta = Random.Range(-1, 2);
        int colDelta = Random.Range(-1, 2);

        int row = tile.Row + rowDelta;
        int col = tile.Col + colDelta;

        int maxIndex = global.worldGridLength - 1;
        
        if (row < 0 || row > maxIndex || col < 0 || col > maxIndex)
        {
            return getRandomTileAround(tile);
        }

        return global.tiles[row, col];
    }

    private void PlantTick(Plant plant, int localWind)
    {
        float prop = Random.Range(0f, 1f);
        if (plant.reproductionProp > prop && plant.isGrownUp())
        {
            getRandomTileAround(plant.Tile).PlantPlantByReproduction(plant);
        }
        plant.Live();
        plant.ResistLocalWind(localWind);
    }

    private void TileTick(Tile tile, int displayLayer, bool layerChanged)
    {
        if (layerChanged)
        {
            tile.ChangeLayer(displayLayer);
        }
        tile.RefreshLayer();
        tile.Water += global.weatherController.WeatherWaterGain;
    }
}