using UnityEngine;

namespace Tiles
{
    public class Tile : MonoBehaviour
    {
        public TileProperties Properties { get; }
        public Plant Plant { get; set; }

        public Tile(TileProperties properties, Plant plant)
        {
            Properties = properties;
            Plant = plant;
        }
    }
}