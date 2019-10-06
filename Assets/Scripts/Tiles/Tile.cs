using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] public int water = 1;
    [SerializeField] public int fertility = 1;
    private PlantBag plantBag;
    public Plant Plant { get; set; }
    public GlobalInformation global { get; set; }

    private void Awake()
    {
        global = GameObject.Find("GlobalInformation").GetComponent<GlobalInformation>();
        plantBag = GameObject.Find("PlantBag").GetComponent<PlantBag>();
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
        Plant type = global.plantSelection.Selected;
        plantBag.DecreaseSeedsOf(type);
        GameObject plant = Instantiate(type.gameObject, transform);
        Plant = plant.GetComponent<Plant>();
        Plant.SetTile(this);
    }

    private Boolean IsPlantable()
    {
        Plant type = global.plantSelection.Selected;
        return type != null && IsCurrentlyNothingPlanted() && plantBag.HasSeedsOf(type);
    }

    private Boolean IsCurrentlyNothingPlanted()
    {
        return Plant == null;
    }
}