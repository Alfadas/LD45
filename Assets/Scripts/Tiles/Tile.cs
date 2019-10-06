using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] public int water = 1;
    [SerializeField] public int fertility = 1;
    [SerializeField] private TileView tileViewPrefab;
    public Plant Plant { get; set; }
    public GlobalInformation global { get; set; }


    private void Awake()
    {
        global = GameObject.Find("GlobalInformation").GetComponent<GlobalInformation>();
    }

    private void OnMouseDown()
    {
        if (IsPlantable())
        {
            PlantPlant();
        }
    }

    private void PlantPlant()
    {
        GameObject plant = Instantiate(global.plantSelection.Selected.gameObject, transform);
        Plant = plant.GetComponent<Plant>();
        Plant.SetTile(this);
    }

    private Boolean IsPlantable()
    {
        return global.plantSelection.Selected != null && Plant == null;
    }
}