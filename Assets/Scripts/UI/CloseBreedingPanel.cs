using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseBreedingPanel : MonoBehaviour
{
    [SerializeField] private PlantBreedingPanel plantBreedPanel;

    private void Awake()
    {
        GetComponent<Button>().onClick
            .AddListener(() => plantBreedPanel.gameObject.SetActive(false));
    }
}