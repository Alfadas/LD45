using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Tile : MonoBehaviour
{
    public PlantSelection PlantSelection { get; set; }
    [SerializeField] public int water = 1;
    [SerializeField] public int fertility = 1;
    [SerializeField] private TileView tileViewPrefab;
    private Plant Plant { get; set; }

    private void Awake()
    {
        Instantiate(tileViewPrefab, transform);
    }

    private void OnMouseDown()
    {
        if (PlantSelection != null)
        {
            GameObject plant = Instantiate(PlantSelection.Selected.gameObject, transform);
            Plant = plant.GetComponent<Plant>();
            Plant.SetTile(this);
        }
    }
}