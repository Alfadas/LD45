using Tiles;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    private GameObject[,] tiles;
    private int rowCount;
    private int colCount;

    private void Start()
    {
        Transform transform = GetComponent<Transform>();
        Grid grid = GetComponentInParent<Grid>();
        var localScale = transform.localScale;
        var position = grid.transform.position;
        var x = position.x + localScale.x / 2;
        var z = position.z + localScale.z / 2;
        transform.SetPositionAndRotation(new Vector3(x, transform.position.y, z), Quaternion.identity);

        rowCount = Mathf.CeilToInt(localScale.x / grid.cellSize.x);
        colCount = Mathf.CeilToInt(localScale.z / grid.cellSize.z);
        tiles = new GameObject[rowCount, colCount];
        Object original = GameObject.Find("Tile");
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                var pos = grid.GetCellCenterLocal(new Vector3Int(i, 0, j)); 
                tiles[i, j] = (GameObject) Instantiate(original, pos, Quaternion.identity, transform);
            }
        }
    }

    private void OnDrawGizmos()
    {
        var grid = GetComponentInParent<Grid>();
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                if (tiles[i, j] != null)
                {
                    var cellIdx = new Vector3Int(i, 0, j);
                    Gizmos.DrawSphere(grid.GetCellCenterLocal(cellIdx), 1);
                }
            }
        }
    }
}