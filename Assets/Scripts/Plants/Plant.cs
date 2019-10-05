﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int growth;
    [SerializeField] int growthPerStage;
    [SerializeField] float growingFactor = 1;
    [SerializeField] int minWater = 1;
    [SerializeField] int minSun = 1;
    [SerializeField] int minNutrition = 1;
    [SerializeField] PlantPropertys plantPropertys;
    [SerializeField] GameObject[] growthStages;
    GameObject currentStage;

    int water = 1;
    int nutriton = 1;
    int sun = 1;

    public void Awake()
    {
        currentStage = growthStages[0];
        currentStage.SetActive(true);
    }
    private void Update()
    {
        Grow();
    }

    private void Grow()
    {
        if (water < minWater)
        {
            ChangeHealth(-1);
        }
        if (nutriton < minNutrition)
        {
            ChangeHealth(-1);
        }
        if (sun < minSun)
        {
            ChangeHealth(-1);
        }
        growth += Mathf.RoundToInt(growingFactor * (water * sun * nutriton));
        int nextStage = Mathf.FloorToInt(growth / growthPerStage);
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
