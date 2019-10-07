using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlantBreedingPanel : MonoBehaviour
{
    [SerializeField] private PlantBag plantBag;
    [SerializeField] private Dropdown firstPlantDropDown;
    [SerializeField] private Dropdown secondPlantDropDown;
    [SerializeField] private Button breedButton;
    [SerializeField] private PlantBreeder plantBreeder;
    [SerializeField] private Text resultText;

    private void Awake()
    {
        firstPlantDropDown.options.Clear();
        secondPlantDropDown.options.Clear();

        var options = plantBag.GetTypes()
            .Select(type => new Dropdown.OptionData(type.name)).ToList();

        firstPlantDropDown.options.AddRange(options);
        secondPlantDropDown.options.AddRange(options);

        breedButton.onClick.AddListener(OnBreedButtonClick);
    }

    private void OnBreedButtonClick()
    {
        var first = plantBag.GetTypes()[firstPlantDropDown.value];
        var second = plantBag.GetTypes()[secondPlantDropDown.value];

        Debug.Log($"First: {first} | Second: {second}");

        var result = plantBreeder.Breed(first, second);
        resultText.text = result == null ? "Nothing" : result.Name;
    }

    public void UpdateAvailablePlants()
    {
        Awake();
    }
}