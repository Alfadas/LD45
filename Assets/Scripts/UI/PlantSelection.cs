using System;
using UnityEngine;
using UnityEngine.UI;

public class PlantSelection : MonoBehaviour
{
    [SerializeField] private Button plantSelectionButtonPrefab;
    public String[] plantTypes = {"Pumpkin", " Melon"};

    public string Selected { get; set; }

    private void Awake()
    {
        foreach (var type in plantTypes)
        {
            GameObject plantSelectionButton =
                Instantiate(plantSelectionButtonPrefab.gameObject, transform);
            plantSelectionButton.GetComponentInChildren<Text>().text = type;
            plantSelectionButton.GetComponent<Button>().onClick.AddListener(() => Selected = type);
        }
    }
}