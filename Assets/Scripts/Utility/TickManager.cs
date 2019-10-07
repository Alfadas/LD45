using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    [SerializeField] GlobalInformation global;
    [SerializeField] int intervall = 1;

    bool pause;

    public bool Pause
    {
        get
        {
            return pause;
        }
        set
        {
            pause = value;
        }
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
<<<<<<< HEAD
            windResistanceSum += tile.PlantWindResistance;
        }
        global.weatherController.MoveClouds();
        int localWind = global.weatherController.CalcLocalWind(windResistanceSum);
        foreach (Tile tile in global.tiles)
        {
            TileTick(tile);
            if (tile.Plant != null)
=======
            //TileTick(tile);
            if (tile.HasPlant)
>>>>>>> f2a630f05a4d3f0b51b1a0770022a44786481685
            {
                PlantTick(tile.Plant, localWind);
            }
        }
    }

    private void PlantTick(Plant plant, int localWind)
    {
        plant.Grow();
        plant.ResistLocalWind(localWind);
    }

    private void TileTick(Tile tile)
    {
<<<<<<< HEAD
        tile.water += global.weatherController.WeatherWaterGain;
=======
        
>>>>>>> f2a630f05a4d3f0b51b1a0770022a44786481685
    }
}
