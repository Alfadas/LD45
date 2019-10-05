using UnityEngine;

namespace Tiles
{
    public class Tile : MonoBehaviour
    {
        public int Water { get; set; }
        public int Fertility { get; set; }
        public Plant Plant { get; set; }

<<<<<<< HEAD
        public Tile(Plant plant)
=======
        public Tile(Plant plant, int fertility, int water)
>>>>>>> e654c81d1cd84bdf8c1ab739cfde20684654a8ff
        {
            Plant = plant;
            Fertility = fertility;
            Water = water;
        }
    }
}