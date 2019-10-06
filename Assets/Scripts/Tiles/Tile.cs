using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] public int water = 1;
    [SerializeField] public int fertility = 1;
<<<<<<< HEAD
    [SerializeField] private TileView tileViewPrefab;
    public Plant Plant { get; set; }
    public GlobalInformation global { get; set; }
=======
    private Plant Plant { get; set; }
    private GlobalInformation global { get; set; }
>>>>>>> 9280cd38ce5e5e50be329cf7f6bafc66b5273000

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