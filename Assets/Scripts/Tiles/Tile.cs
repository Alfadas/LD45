using UnityEngine;

public class Tile : MonoBehaviour
{
    public PlantSelection PlantSelection { get; set; }
    [SerializeField] public int water = 1;
    [SerializeField] public int fertility = 1;
    private Plant Plant { get; set; }

    private void OnMouseDown()
    {
        Debug.Log($"Tile CLicked with Selection: {PlantSelection.Selected.Name}");
        if (PlantSelection != null)
        {
            GameObject plant = Instantiate(PlantSelection.Selected.gameObject, transform);
            Plant = plant.GetComponent<Plant>();
            Plant.SetTile(this);
        }
    }
}