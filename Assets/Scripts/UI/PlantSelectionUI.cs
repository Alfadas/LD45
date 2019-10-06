using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantSelectionUI : MonoBehaviour
{
    [SerializeField] private Button plantSelectionButtonPrefab;

    [SerializeField] private List<Plant> plantTypes;
    [SerializeField] private Text selectionText;
    [SerializeField] PlantSelection plantSelection;

    private void Awake()
    {
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
        plantSelection.Selected = type;
    }
}