using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] string name;
    [SerializeField] int maxHealth;
    [SerializeField] int energyNeed = 1;
    [SerializeField] int growth;
    [SerializeField] int growthPerStage;
    [SerializeField] float growingFactor = 1;
    [SerializeField] int waterNeed = 1;
    [SerializeField] float minLight = 0.1f;
    [SerializeField] int nutritionNeed = 1;
    [SerializeField] PlantPropertys plantPropertys;
    [SerializeField] MeshRenderer[] growthStages;
    [SerializeField] bool degrading = false;

    public int energy = 150; 
    public int health;

    public Color current;
    public Color next;
    MeshRenderer currentStage;
    int currentStageIndex;
    Tile tile;

    public int WindResistance
    {
        get
        {
            return Mathf.FloorToInt(plantPropertys.Stable * currentStageIndex * 0.25f);
        }
    }

    public string Name
    {
        get { return name; }
    }

    public void Awake()
    {
        currentStage = growthStages[0];
        currentStageIndex = 0;
        currentStage.gameObject.SetActive(true);
    }

    private void Start()
    {
        energyNeed = Mathf.RoundToInt(PlantPropertyConst.energyNeed_Stable_Multi * plantPropertys.Stable);
        waterNeed = Mathf.RoundToInt(PlantPropertyConst.waterNeed_Stable_Multi * plantPropertys.Stable);
        nutritionNeed = Mathf.RoundToInt(PlantPropertyConst.nutritionNeed_Stable_Multi * plantPropertys.Stable);
        minLight = PlantPropertyConst.minLight_Stable_Multi * plantPropertys.Stable;
        energy = PlantPropertyConst.startEnergy_Stable_Multi * plantPropertys.Stable;
        maxHealth = PlantPropertyConst.maxHealth_Stable_Multi * plantPropertys.Stable;
        growthPerStage = PlantPropertyConst.growthPerStage_Stable_Multi * plantPropertys.Stable;
        health = maxHealth;
    }

    public void Grow()
    {
        if (degrading)
        {
            tile.fertility += Mathf.RoundToInt(growth * PlantPropertyConst.degrading_FertilityReturn_Multi);
            foreach(Vector2Int vector2Int in PlantPropertyConst.directNeigbour)
            {
                //global.Tiles[tile.Row + vector2Int.x]
            }
            ChangeHealth(-maxHealth / PlantPropertyConst.degradingTime);
        }
        float nutrition = 0;
        float water = 0;
        float light = tile.Global.daytimeController.Sunlight - minLight;
        if (light <= 0)
        {
            light = 0;
        }
        else
        {
            nutrition = 1;
            if (tile.fertility < nutritionNeed)
            {
                nutrition = tile.fertility / nutritionNeed;
                tile.fertility = 0;
            }
            else
            {
                tile.fertility -= nutritionNeed;
            }
            water = 1;
            if (tile.water < waterNeed)
            {
                water = tile.water / waterNeed;
                tile.water = 0;
            }
            else
            {
                tile.water -= waterNeed;
            }
        }
        int energyGain = Mathf.RoundToInt(water * light * nutrition * (currentStageIndex + 1));
        energy += energyGain - energyNeed;
        if(energy < 0)
        {
            ChangeHealth(energy);
            energy = 0;
            return;
        }
        if (energyGain == 0)
        {
            return;
        }
        growth += Mathf.RoundToInt(growingFactor * (water * light * nutrition));
        int nextStage = Mathf.FloorToInt(growth / growthPerStage);
        if (nextStage >= growthStages.Length)
        {
            degrading = true;
        }
        else if (growthStages[nextStage] != currentStage)
        {
            currentStage.gameObject.SetActive(false);
            currentStage = growthStages[nextStage];
            currentStageIndex = nextStage;
            currentStage.gameObject.SetActive(true);
        }
    }

    public void ResistLocalWind(int localWind)
    {
        Debug.Log(WindResistance * 2 - localWind * currentStageIndex * 0.5f);
    }
    void ChangeHealth(int change)
    {
        health += change;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health <= 0)
        {
            tile.fertility += Mathf.RoundToInt(growth * PlantPropertyConst.dying_FertilityReturn_Multi);
            tile.Plant = null;
            Destroy(gameObject);
            return;
        }
        float perc = (float)health / (float)maxHealth;
        foreach (MeshRenderer renderer in growthStages)
        {
            renderer.material.SetColor("_BaseColor", new Color(perc, perc, perc));
        }
    }

    public void SetTile(Tile tile)
    {
        this.tile = tile;
    }
}