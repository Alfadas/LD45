using UnityEngine;
using UnityEngine.UI;

public class PlantSelectionUI : MonoBehaviour
{
    [SerializeField] private Button plantSelectionButtonPrefab;
    private readonly string[] plantTypes = {"Carrot"};

    public string Selecteded { get; set; }

    private void Awake()
    {
        foreach (var type in plantTypes)
        {
            GameObject plantSelectionButton =
                Instantiate(plantSelectionButtonPrefab.gameObject, transform);
            plantSelectionButton.GetComponentInChildren<Text>().text = type;
            plantSelectionButton.GetComponent<Button>().onClick
                .AddListener(() => OnPlantSelectionButtonClick(type));
        }
    }

    private void OnPlantSelectionButtonClick(string type)
    {
        GameObject.Find("PlantSelectionText").GetComponent<Text>().text = type;
        Selecteded = type;
    }
}