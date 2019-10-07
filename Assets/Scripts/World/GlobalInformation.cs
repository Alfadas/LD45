﻿using UnityEngine;

public class GlobalInformation : MonoBehaviour
{
    [SerializeField] public int worldGridLength;
    [SerializeField] public PlantSelection plantSelection;
    [SerializeField] public Grid worldGrid;
    [SerializeField] public DaytimeController daytimeController;
    [SerializeField] public WeatherController weatherController;
    [SerializeField] public Tile[,] tiles;
}