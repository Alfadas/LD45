using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    [SerializeField] int sideLength;
    private GameObject[,] tiles;
    private int rowCount;
    private int colCount;
    
    private void Start()
    {
        Grid grid = GetComponentInParent<Grid>();
        
        rowCount = sideLength;
        colCount = sideLength;
        tiles = new GameObject[rowCount, colCount];
        Object original = GameObject.Find("Tile");
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                var cellIdx = new Vector3Int(i, 0, j);
                var pos = grid.GetCellCenterLocal(cellIdx);
                var tile = (GameObject) Instantiate(original, pos, Quaternion.identity, transform);
                tiles[i, j] = tile;
            }
        }
    }

}