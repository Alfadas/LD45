using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private GlobalInformation global;

    private Tile[,] tiles;
    private int rowCount;
    private int colCount;

    private void Start()
    {
        rowCount = colCount = global.worldGridLength;
        tiles = new Tile[rowCount, colCount];
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                CreateTileInCell(i, j);
            }
        }
        global.tiles = tiles;
    }

    private void CreateTileInCell(int i, int j)
    {
        var cellIdx = new Vector3Int(i, 0, j);
        var pos = global.worldGrid.GetCellCenterLocal(cellIdx);
        var tileObj = Instantiate(tilePrefab.gameObject, pos, Quaternion.identity, transform);
        var tile = tileObj.GetComponent<Tile>();
        tile.Row = i;
        tile.Col = j;
        tiles[i, j] = tile;
    }
}