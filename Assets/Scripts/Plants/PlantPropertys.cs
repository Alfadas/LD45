using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlantPropertys
{
    [SerializeField] int eatable;
    [SerializeField] int stable;
    public PlantPropertys(int eatable, int stable)
    {
        this.eatable = eatable;
        this.stable = stable;
    }
    public int Eatable
    {
        get
        {
            return eatable;
        }
    }
    public int Stable
    {
        get
        {
            return stable;
        }
    }
}
