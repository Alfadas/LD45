using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlantPropertyConst
{
    //tileNeigbours
    public static readonly Vector2Int[] directNeigbour = { new Vector2Int(0, 1), new Vector2Int(1, 0), new Vector2Int(0, -1), new Vector2Int(-1, 0) };
    public static readonly Vector2Int[] indirectNeigbour = { new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, -1), new Vector2Int(-1, 1) };
    //degrading
    public const float dying_FertilityReturn_Multi = 0.05f;
    public const float degrading_FertilityReturn_Multi = 0.03f;
    public const int degradingTime = 5;
    //Stable Multis
        //needs
        public const float nutritionNeed_Stable_Multi = 0.15f;
        public const float energyNeed_Stable_Multi = 0.05f;
        public const float waterNeed_Stable_Multi = 0.05f;
        public const float minLight_Stable_Multi = 0.1f;
        //secondary Propertys
        public const int maxHealth_Stable_Multi = 4;
        public const int growthPerStage_Stable_Multi = 5;
        public const int startEnergy_Stable_Multi = 2;
    //Eatable Multis
        //needs
        public const float nutritionNeed_Eatable_Multi = 0.05f;
        public const float energyNeed_Eatable_Multi = 0.05f;
        public const float waterNeed_Eatable_Multi = 0.15f;
        public const float minLight_Eatable_Multi = 0.1f;
        //secondary Propertys
        public const int maxHealth_Eatable_Multi = -1;
        public const int growthPerStage_Eatable_Multi = 3;
        public const int startEnergy_Eatable_Multi = 2;
        public const int lastStageBoni_Eatable_Multi = 4;
        public const int animalAttraction_Eatable_Multi = 4;

}
