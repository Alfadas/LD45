using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] public int water = 1;
    [SerializeField] public int fertility = 1;
    [SerializeField] private TileView tileViewPrefab;
    private Plant Plant { get; set; }
    private GlobalInformation global { get; set; }

    private void Awake()
    {
        global = GameObject.Find("GlobalInformation").GetComponent<GlobalInformation>();

        Instantiate(tileViewPrefab, transform);
    }

    private void OnMouseDown()
    {
        if (global.plantSelection.Selected != null)
        {
            GameObject plant = Instantiate(global.plantSelection.Selected.gameObject, transform);
            Plant = plant.GetComponent<Plant>();
            Plant.SetTile(this);
        }
    }
}