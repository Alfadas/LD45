using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] public int water = 1;
    [SerializeField] public int fertility = 1;
    private PlantBag plantBag;
    private Plant plant;
    private bool hasPlant = false;

    public int Row { get; set; }
    public int Col { get; set; }

    public bool HasPlant => hasPlant;

    public Plant Plant
    {
        get { return plant; }
        set
        {
            hasPlant = value != null;
            plant = value;
        }
    }

    public GlobalInformation Global { get; set; }

    public int PlantWindResistance
    {
        get
        {
            if (Plant == null)
            {
                return 0;
            }
            return Plant.WindResistance;
        }
    }

    private void Awake()
    {
        Global = GameObject.Find("GlobalInformation").GetComponent<GlobalInformation>();
        plantBag = GameObject.Find("PlantBag").GetComponent<PlantBag>();
    }

    private void OnMouseDown()
    {
        Debug.Log($"Row: {Row} Col: {Col}");
        if (IsPlantable())
        {
            PlantPlant();
        }
    }

    private void PlantPlant()
    {
        Plant type = Global.plantSelection.Selected;
        plantBag.DecreaseSeedsOf(type);
        GameObject plantObj = Instantiate(type.gameObject, transform);
        Plant = plantObj.GetComponent<Plant>();
        Plant.SetTile(this);
    }

    private Boolean IsPlantable()
    {
        Plant type = Global.plantSelection.Selected;
        return type != null && !hasPlant && plantBag.HasSeedsOf(type);
    }
}