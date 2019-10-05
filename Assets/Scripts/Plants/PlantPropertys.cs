using System;
using UnityEngine;

[Serializable]
public struct PlantPropertys
{
    [SerializeField] int eatable;
    [SerializeField] int stable;
<<<<<<< HEAD
=======

>>>>>>> e654c81d1cd84bdf8c1ab739cfde20684654a8ff
    public PlantPropertys(int eatable, int stable)
    {
        this.eatable = eatable;
        this.stable = stable;
    }

    public int Eatable
    {
        get { return eatable; }
    }

    public int Stable
    {
        get { return stable; }
    }
}