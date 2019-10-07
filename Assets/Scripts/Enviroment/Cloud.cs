using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] WeatherController weatherController;
    [SerializeField] WeatherState state;
    bool wasCurrent = false;
    bool hasTriggeredNext = false;

    public WeatherController WeatherController
    {
        set
        {
            weatherController = value;
        }
    }

    public WeatherState State
    {
        get { return state; }
    }

    public void Move(int moveDistance)
    {
        transform.position += new Vector3(0, 0, moveDistance);
        if (!hasTriggeredNext && transform.position.z > weatherController.NextCloudZ)
        {
            hasTriggeredNext = true;
            weatherController.SpawnCloud();
        }
        else if (!wasCurrent && transform.position.z > weatherController.CurrentCloudZ)
        {
            wasCurrent = true;
            weatherController.SetCurrentCloud(this);
        }
        else if (transform.position.z > weatherController.DestroyCloudZ)
        {
            weatherController.DestroyCloud(this);
        }
    }
}
