using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    [SerializeField] public int water = 1;
    [SerializeField] public int fertility = 1;
    [SerializeField] MeshRenderer[] displayLayers;
    int currentLayerIndex = 0;
    private PlantBag plantBag;
    private Plant plant;
    private bool hasPlant = false;

    public int Row { get; set; }
    public int Col { get; set; }

    public bool HasPlant => hasPlant;

    public int Water
    {
        get { return water;}
        set
        {
            if (value <= 1000 || value <= WaterCapacity)
            {
                water = value;
            }
            else 
            {
                water = WaterCapacity;
            }
        }
    }

    public Plant Plant
    {
        get { return plant; }
        set
        {
            hasPlant = value != null;
            GetComponentInChildren<Renderer>().enabled = !hasPlant;
            plant = value;
        }
    }

    private int WaterCapacity
    {
        get
        {
            int maxIndex = Global.worldGridLength - 1;
            int stability = 0;
            if (HasPlant)
            {
                stability = Plant.Stability;
            }
            foreach (Vector2Int vector2Int in PlantPropertyConst.directNeigbour)
            {
                int row = Row + vector2Int.x;
                int col = Col + vector2Int.y;
                if (row < 0 || row > maxIndex || col < 0 || col > maxIndex)
                {
                    continue;
                }
                Tile neigbour = Global.tiles[row, col];
                if (neigbour.hasPlant)
                {
                    stability += neigbour.Plant.Stability;
                }
            }
            return 1000 + stability * 2; 
        }
    }

    public GlobalInformation Global { get; set; }

    public int PlantWindResistance
    {
        get
        {
            if (!hasPlant)
            {
                return 0;
            }

            return Plant.WindResistance;
        }
    }

    public void ChangeLayer (int layerIndex)
    {
        displayLayers[currentLayerIndex].gameObject.SetActive(false);
        currentLayerIndex = layerIndex;
        displayLayers[currentLayerIndex].gameObject.SetActive(true);
    }

    public void RefreshLayer()
    {
        if (currentLayerIndex == 1)
        {
            float perc = 0;
            if (Water < 2000)
            {
                perc = (((float)Water / (float)2000) - 1) * -1;
            }
            displayLayers[currentLayerIndex].material.SetColor("_BaseColor", new Color(perc, perc, 1));
        }
        else if (currentLayerIndex == 2)
        {
            float perc = 0;
            if (fertility < 2000)
            {
                perc = (((float)fertility / (float)2000) - 1) * -1;
            }
            displayLayers[currentLayerIndex].material.SetColor("_BaseColor", new Color(perc, 1, perc));
        }
    }

    private void Awake()
    {
        Global = GameObject.Find("GlobalInformation").GetComponent<GlobalInformation>();
        plantBag = GameObject.Find("PlantBag").GetComponent<PlantBag>();
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log($"Row: {Row} Col: {Col}");
            if (IsPlantableByPlayer())
            {
                PlantPlantByPlayer();
            }
        }
    }

    private void PlantPlantByPlayer()
    {
        Plant type = Global.plantSelection.Selected;
        plantBag.DecreaseSeedsOf(type);
        PlantPlant(type);
    }

    private Boolean IsPlantableByPlayer()
    {
        Plant type = Global.plantSelection.Selected;
        return type != null && !hasPlant && plantBag.HasSeedsOf(type);
    }

    private Boolean IsPlantableByReproduction(Plant type)
    {
        return !hasPlant;
    }

    public void PlantPlantByReproduction(Plant type)
    {
        if (IsPlantableByReproduction(type))
        {
            PlantPlant(type);
        }
    }

    private void PlantPlant(Plant type)
    {
        GameObject plantObj = Instantiate(type.gameObject, transform);
        Plant = plantObj.GetComponent<Plant>();
        Plant.Tile = this;
    }
}