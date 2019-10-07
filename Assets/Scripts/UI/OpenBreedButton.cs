using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class OpenBreedButton : MonoBehaviour
{
    [SerializeField] private PlantBreedingPanel plantBreedPanel;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnOpenBreedMenuButtonClick);
    }

    private void OnOpenBreedMenuButtonClick()
    {
        plantBreedPanel.gameObject.SetActive(true);
    }
}
