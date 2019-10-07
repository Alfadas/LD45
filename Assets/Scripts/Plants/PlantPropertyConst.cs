using UnityEngine;

public static class PlantPropertyConst
{
    public static readonly Vector2Int[] directNeigbour = { new Vector2Int(0, 1), new Vector2Int(1, 0), new Vector2Int(0, -1), new Vector2Int(-1, 0) };
    public static readonly Vector2Int[] indirectNeigbour = { new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, -1), new Vector2Int(-1, 1) };
    public const float dying_FertilityReturn_Multi = 0.05f;
    public const float degrading_FertilityReturn_Multi = 0.03f;
    public const int degradingTime = 5;
    //Stable Multis
        //needs
        public const float nutritionNeed_Stable_Multi = 0.2f;
        public const float energyNeed_Stable_Multi = 0.1f;
        public const float waterNeed_Stable_Multi = 0.05f;
        public const float minLight_Stable_Multi = 0.1f;
        //secondary Propertys
        public const int maxHealth_Stable_Multi = 4;
        public const int growthPerStage_Stable_Multi = 8;
        public const int startEnergy_Stable_Multi = 2;

}
