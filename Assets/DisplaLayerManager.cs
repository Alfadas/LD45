using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaLayerManager : MonoBehaviour
{
    [SerializeField] Toggle normal;
    [SerializeField] Toggle water;
    [SerializeField] Toggle nutrition;

    Toggle lastSelected;

    public int Selected
    {
        get
        {
            Toggle newSelected = null;
            int layerIndex = 0;
            if (normal.isOn)
            {
                newSelected = normal;
                layerIndex = 0;
            }
            else if (water.isOn)
            {
                newSelected = water;
                layerIndex = 1;
            }
            else if (nutrition.isOn)
            {
                newSelected = nutrition;
                layerIndex = 2;
            }
            if (newSelected == lastSelected)
            {
                layerIndex = 4;
            }
            lastSelected = newSelected;
            return layerIndex;
        }
    }

}
