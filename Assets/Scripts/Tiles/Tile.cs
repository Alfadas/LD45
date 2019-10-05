using UnityEngine;

namespace Tiles
{
    public class Tile : MonoBehaviour
    {
        public int Water { get; set; }
        public int Fertility { get; set; }
        public Plant Plant { get; set; }

        public Tile(Plant plant, int fertility, int water)
        {
            Plant = plant;
            Fertility = fertility;
            Water = water;
        }
    }
}