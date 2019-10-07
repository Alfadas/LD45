using System;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] string name;
    [SerializeField] int maxHealth;
    [SerializeField] int energyNeed = 1;
    [SerializeField] int growth;
    [SerializeField] int growthPerStage;
    [SerializeField] float energyGainFactor = 4;
    [SerializeField] int waterNeed = 1;
    [SerializeField] float minLight = 0.1f;
    [SerializeField] int nutritionNeed = 1;
    [SerializeField] PlantPropertys plantPropertys;
    [SerializeField] MeshRenderer[] growthStages;
    [SerializeField] int lastStageGrowthBoni;
    [SerializeField] int animalAttraction;
    bool degrading = false;
    [SerializeField] public float reproductionProp;

    [SerializeField] int energy = 150;
    [SerializeField] int health;

    MeshRenderer currentStage;

    public Tile Tile { get; set; } = null;
    int currentStageIndex;


    public int Stability
    {
        get
        {
            return plantPropertys.Stable;
        }
    }
    public int Eatable
    {
        get
        {
            return plantPropertys.Eatable;
        }
    }
    public int WindResistance
    {
        get
        {
            return Mathf.FloorToInt(Stability * currentStageIndex * 0.25f);
        }
    }

    public int AnimalAttraction
    {
        get
        {
            return animalAttraction;
        }
    }

    public string Name
    {
        get { return name; }
    }
    private void Start()
    {
        CalcPropertys();
    }

    public void CalcPropertys()
    {
        foreach (MeshRenderer render in growthStages)
        {
            render.gameObject.SetActive(false);
        }
        currentStage = growthStages[0];
        currentStageIndex = 0;
        currentStage.gameObject.SetActive(true);
        growth = 0;
        degrading = false;
        energyNeed = Mathf.RoundToInt(PlantPropertyConst.energyNeed_Stable_Multi * plantPropertys.Stable) + Mathf.RoundToInt(PlantPropertyConst.energyNeed_Eatable_Multi * plantPropertys.Eatable);
        waterNeed = Mathf.RoundToInt(PlantPropertyConst.waterNeed_Stable_Multi * plantPropertys.Stable) + Mathf.RoundToInt(PlantPropertyConst.waterNeed_Eatable_Multi * plantPropertys.Eatable);
        nutritionNeed = Mathf.RoundToInt(PlantPropertyConst.nutritionNeed_Stable_Multi * plantPropertys.Stable) + Mathf.RoundToInt(PlantPropertyConst.nutritionNeed_Eatable_Multi * plantPropertys.Eatable);
        minLight = (PlantPropertyConst.minLight_Stable_Multi * plantPropertys.Stable + PlantPropertyConst.minLight_Eatable_Multi * plantPropertys.Eatable) / 2;
        energy = PlantPropertyConst.startEnergy_Stable_Multi * plantPropertys.Stable + PlantPropertyConst.startEnergy_Eatable_Multi * plantPropertys.Eatable;
        maxHealth = PlantPropertyConst.maxHealth_Stable_Multi * plantPropertys.Stable + PlantPropertyConst.maxHealth_Eatable_Multi * plantPropertys.Eatable;
        growthPerStage = PlantPropertyConst.growthPerStage_Stable_Multi * plantPropertys.Stable + PlantPropertyConst.growthPerStage_Eatable_Multi * plantPropertys.Eatable;
        lastStageGrowthBoni = PlantPropertyConst.lastStageBoni_Eatable_Multi * plantPropertys.Eatable;
        animalAttraction = PlantPropertyConst.animalAttraction_Eatable_Multi * plantPropertys.Eatable;
        energyGainFactor = (plantPropertys.Stable + plantPropertys.Eatable) / 10f;
        ChangeHealth(maxHealth);
    }

    public void Live()
    {
        if (degrading)
        {
            Degrade();
        }
        float nutrition = 0;
        float water = 0;
        float light = Tile.Global.daytimeController.Sunlight - minLight;
        if (light <= 0)
        {
            light = 0;
        }
        else
        {
            bool dubleEnergy = false;
            if (light > 2)
            {
                light = 2;
                dubleEnergy = true;
            }
            else if (light > 1)
            {
                light = 1;
            }
            CollectRecurces(out nutrition, out water, ref dubleEnergy);
            if (light == 2 && dubleEnergy)
            {
                light = 2.1f;
            }
            else if (light == 2 && !dubleEnergy)
            {
                light = 1;
            }
        }
        float currentEnergyGainFactor = energyGainFactor;
        if (water * light * nutrition == 1)
        {
            currentEnergyGainFactor *= 2;
        }
        int energyGain = Mathf.RoundToInt(currentEnergyGainFactor * water * light * nutrition);
        energyGain -= Mathf.RoundToInt(energyNeed * ((currentStageIndex +1) * 0.5f));
        if (energyGain <= 0)
        {
            energy += energyGain;
        }
        else
        {
            energy += energyGain / 2;
        }
        if (energy < 0)
        {
            ChangeHealth(energy);
            energy = 0;
            return;
        }
        if (energyGain <= 0)
        {
            return;
        }
        if (energy > energyNeed * 160)
        {
            Grow(energyGain + (energy - energyNeed * 160));
            energy = energyNeed * 160;
        }
        Grow(energyGain);
    }

    private void Grow(int energyGain)
    {
        growth += Mathf.RoundToInt(energyGain);
        int nextStage = 0;
        if (currentStageIndex < growthStages.Length - 1)
        {
            nextStage = Mathf.FloorToInt(growth / growthPerStage);
        }
        else
        {
            if (growth > growthStages.Length - 1 * growthPerStage + lastStageGrowthBoni)
            {
                nextStage = currentStageIndex + 1;
            }
            else
            {
                nextStage = currentStageIndex;
            }
        }

        if (nextStage >= growthStages.Length)
        {
            degrading = true;
        }
        else if (growthStages[nextStage] != currentStage)
        {
            
            if (currentStage == null)
            {
                Debug.Log(currentStage + " " + currentStageIndex);
            }
            currentStage.gameObject.SetActive(false);
            currentStage = growthStages[nextStage];
            currentStageIndex = nextStage;
            currentStage.gameObject.SetActive(true);
        }
    }

    private void CollectRecurces(out float nutrition, out float water, ref bool doubleEnergy)
    {
        nutrition = 1;
        water = 1;
        if (doubleEnergy && Tile.fertility > nutritionNeed * 2 && Tile.Water < waterNeed * 2)
        {
            Tile.fertility -= nutritionNeed * 2;
            Tile.Water -= waterNeed * 2;
            return;
        }
        if (Tile.fertility > nutritionNeed && Tile.Water < waterNeed)
        {
            Tile.fertility -= nutritionNeed;
            Tile.Water -= waterNeed;
            return;
        }
        if (Tile.fertility < nutritionNeed)
        {
            nutrition = Tile.fertility / nutritionNeed;
            Tile.fertility = 0;
            doubleEnergy = false;
        }
        else
        {
            Tile.fertility -= nutritionNeed;
        }
        if (Tile.Water < waterNeed)
        {
            water = Tile.Water / waterNeed;
            Tile.Water = 0;
            doubleEnergy = false;
        }
        else
        {
            Tile.Water -= waterNeed;
        }
    }

    private void Degrade()
    {
        int maxIndex = Tile.Global.worldGridLength - 1;
        int fertilityGain = Mathf.RoundToInt(growth * PlantPropertyConst.degrading_FertilityReturn_Multi);
        Tile.fertility += fertilityGain;
        foreach (Vector2Int vector2Int in PlantPropertyConst.directNeigbour)
        {
            int row = Tile.Row + vector2Int.x;
            int col = Tile.Col + vector2Int.y;
            if (row < 0 || row > maxIndex || col < 0 || col > maxIndex)
            {
                continue;
            }
            Tile.Global.tiles[row, col].fertility += fertilityGain;
        }
        foreach (Vector2Int vector2Int in PlantPropertyConst.indirectNeigbour)
        {
            int row = Tile.Row + vector2Int.x;
            int col = Tile.Col + vector2Int.y;
            if (row < 0 || row > maxIndex || col < 0 || col > maxIndex)
            {
                continue;
            }
            Tile.Global.tiles[row, col].fertility += fertilityGain / 2;
        }
        ChangeHealth(-maxHealth / PlantPropertyConst.degradingTime);
    }

    public void ResistLocalWind(int localWind)
    {
        if (WindResistance * 2 - localWind * currentStageIndex * 0.5f < 0)
        {
            ChangeHealth(-1);
        }
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
            Tile.fertility += Mathf.RoundToInt(growth * PlantPropertyConst.dying_FertilityReturn_Multi);
            Tile.Plant = null;
            Destroy(gameObject);
            return;
        }
        float perc = (float)health / (float)maxHealth;
        foreach (MeshRenderer renderer in growthStages)
        {
            renderer.material.SetColor("_BaseColor", new Color(perc, perc, perc));
        }
    }

    public Boolean IsGrownUp()
    {
        return currentStageIndex == growthStages.Length - 1;
    }

    public override bool Equals(object other)
    {
        Plant p = (Plant) other;
        return p != null && p.Name.Equals(Name);
    }
}