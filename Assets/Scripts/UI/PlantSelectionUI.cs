using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantSelectionUI : MonoBehaviour
{
    [SerializeField] private Button plantSelectionButtonPrefab;

    [SerializeField] private List<Plant> plantTypes;
    [SerializeField] private Text selectionText;
    [SerializeField] private GlobalInformation global;

    private void Awake()
    {
        global.plantSelection.Selected = plantTypes[0];
        selectionText.text = global.plantSelection.Selected.Name;
        foreach (var type in plantTypes)
        {
            GameObject plantSelectionButton =
                Instantiate(plantSelectionButtonPrefab.gameObject, transform);
            plantSelectionButton.GetComponentInChildren<Text>().text = type.Name;
            plantSelectionButton.GetComponent<Button>().onClick
                .AddListener(() => OnPlantSelectionButtonClick(type));
        }
    }

    private void OnPlantSelectionButtonClick(Plant type)
    {
        selectionText.text = type.Name;
        global.plantSelection.Selected = type;
    }
}