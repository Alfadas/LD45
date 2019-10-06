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
    [SerializeField] int minLight = 1;
    [SerializeField] int nutritionNeed = 1;
    [SerializeField] PlantPropertys plantPropertys;
    [SerializeField] MeshRenderer[] growthStages;
    int energy;
    int health;
    MeshRenderer currentStage;
    int currentIndex;
    Tile tile;

    int water = 1;
    int nutriton = 1;

    public string Name
    {
        get { return name; }
    }

    public void Awake()
    {
        currentStage = growthStages[0];
        currentIndex = 0;
        currentStage.gameObject.SetActive(true);
    }

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        Grow();
    }

    private void Grow()
    {
        float light = tile.global.daytimeController.Sunlight - minLight;
        if (light < 0)
        {
            light = 0;
        }
        float nutrition = 1;
        if (tile.fertility < nutritionNeed)
        {
            nutrition = tile.fertility / nutritionNeed;
            tile.fertility = 0;
        }
        else
        {
            tile.fertility -= nutritionNeed;
        }
        float water = 1;
        if (tile.water < waterNeed)
        {
            water = tile.water / waterNeed;
            tile.water = 0;
        }
        else
        {
            tile.water -= waterNeed;
        }
        int energyGain = Mathf.RoundToInt(water * light * nutriton * currentIndex);
        energy += energyGain - energyNeed;
        if(energy < 0)
        {
            energy = 0;
            ChangeHealth(energy);
            return;
        }
        if (energyGain == 0)
        {
            return;
        }
        growth += Mathf.RoundToInt(growingFactor * (water * light * nutriton));
        int nextStage = Mathf.FloorToInt(growth / growthPerStage);
        if (nextStage >= growthStages.Length)
        {
            ChangeHealth(-1);
        }
        else if (growthStages[nextStage] != currentStage)
        {
            currentStage.gameObject.SetActive(false);
            currentStage = growthStages[nextStage];
            currentIndex = nextStage;
            currentStage.gameObject.SetActive(true);
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
            tile.Plant = null;
            Destroy(gameObject);
            return;
        }
        float perc = health / maxHealth;
        foreach (MeshRenderer renderer in growthStages)
        {
            renderer.material.color = new Color(perc, perc, perc);
        }
    }

    public void SetTile(Tile tile)
    {
        this.tile = tile;
    }
}