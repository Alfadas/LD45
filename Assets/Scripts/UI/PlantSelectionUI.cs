using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantSelectionUI : MonoBehaviour
{
    [SerializeField] private Button plantSelectionButtonPrefab;

    [SerializeField] private PlantBag plantBag;
    [SerializeField] private Text selectionText;
    [SerializeField] private GlobalInformation global;

    private readonly Dictionary<Plant, Button> plantSelectionButtons = new Dictionary<Plant, Button>();

    private void Awake()
    {
        global.plantSelection.Selected = plantBag.GetTypes()[0];
        selectionText.text = global.plantSelection.Selected.Name;
        foreach (var type in plantBag.GetTypes())
        {
            CreateInitialPlantSelectionButton(type);
        }

        UpdateSeedCounts(plantBag.GetSeedCounts());
    }

    private void CreateInitialPlantSelectionButton(Plant type)
    {
        GameObject plantSelectionButton =
            Instantiate(plantSelectionButtonPrefab.gameObject, transform);
        Button button = plantSelectionButton.GetComponent<Button>();
        button.onClick.AddListener(() => OnPlantSelectionButtonClick(type));
        plantSelectionButtons.Add(type, button);
    }

    private void OnPlantSelectionButtonClick(Plant type)
    {
        selectionText.text = type.Name;
        global.plantSelection.Selected = type;
    }

    public void UpdateSeedCounts(Dictionary<Plant, int> seedCounts)
    {
        foreach (var plant in plantSelectionButtons.Keys)
        {
            var text = plantSelectionButtons[plant].GetComponentInChildren<Text>();
            text.text = $"{plant.Name}: {seedCounts[plant]}";
        }
    }
}