using Tiles;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    [SerializeField] private int sideLength;
    [SerializeField] private Tile tilePrefab;


    private GameObject[,] tiles;
    private int rowCount;
    private int colCount;

    private void Start()
    {
        Grid grid = GetComponentInParent<Grid>();

        rowCount = sideLength;
        colCount = sideLength;
        tiles = new GameObject[rowCount, colCount];
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                var cellIdx = new Vector3Int(i, 0, j);
                var pos = grid.GetCellCenterLocal(cellIdx);
                var tile = Instantiate(tilePrefab.gameObject, pos, Quaternion.identity, transform);
                tiles[i, j] = tile;
            }
        }
    }
}