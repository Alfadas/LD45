using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int growth;
    [SerializeField] int growthPerStage;
    [SerializeField] PlantPropertys plantPropertys;
    [SerializeField] GameObject[] growthStages;
    GameObject currentStage;

    public void Awake()
    {
        currentStage = growthStages[0];
        currentStage.SetActive(true);
    }
    private void Update()
    {
        growth += 2;
        int nextStage = Mathf.FloorToInt(growth/growthPerStage);
        if (nextStage >= growthStages.Length)
        {
            ChangeHealth(-1);
        }
        else if (growthStages[nextStage] != currentStage)
        {
            currentStage.SetActive(false);
            currentStage = growthStages[nextStage];
            currentStage.SetActive(true);
        }
    }

    void ChangeHealth(int change)
    {
        health += change;
        if (health <= 0)
        {
            Object.Destroy(gameObject);
        }
    }
}
