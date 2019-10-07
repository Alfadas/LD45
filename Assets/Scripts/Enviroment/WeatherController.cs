using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeatherState
{
    dry,
    cloudy,
    rainy
}
public class WeatherController : MonoBehaviour
{
    [SerializeField] Transform cloudSpawn;
    [SerializeField] Cloud[] cloudTypes;
    [SerializeField] List<Cloud> clouds = new List<Cloud>();
    [SerializeField] Cloud currentCloud;
    [SerializeField] int nextCloudZ;
    [SerializeField] int currentCloudZ;
    [SerializeField] int destroyCloudZ;
    [SerializeField] int windToSpeedDivider;
    [SerializeField] int startWind = 500;
    [SerializeField] int minWind;
    [SerializeField] int maxWind;
    [SerializeField] int globalWind;
    [SerializeField] int localWind;
    [SerializeField] int windChange = 50;
    [Header("SunDim")]
    [SerializeField] float cloudySunDim = 0.8f;
    [SerializeField] float rainySunDim = 0.2f;
    [Header("WaterGain")]
    [SerializeField] int rainyWaterGain = 25;
    int rainCount;
    Cloud newCloud;
    Cloud decayingCloud;
    bool addedCloud;
    bool removedCloud;

    public int NextCloudZ { get { return nextCloudZ; } }
    public int CurrentCloudZ { get { return currentCloudZ; } }
    public int DestroyCloudZ { get { return destroyCloudZ; } }

    public WeatherState CurrentState
    {
        get { return currentCloud.State; }
    }

    public float WeatherSunDim
    {
        get
        {
            if(CurrentState == WeatherState.dry)
            {
                return 1;
            }
            else if (CurrentState == WeatherState.cloudy)
            {
                return cloudySunDim;
            }
            else if (CurrentState == WeatherState.rainy)
            {
                return rainySunDim;
            }
            Debug.LogError("missing WeatherState");
            return 1;
        }
    }

    public int WeatherWaterGain
    {
        get
        {
            if (CurrentState == WeatherState.rainy)
            {
                return rainyWaterGain;
            }
            else return 0;
        }
    }

    private void Start()
    {
        globalWind = startWind + (Random.Range(-2, 3) * windChange);
    }


    public void SpawnCloud()
    {
        if (rainCount == 2)
        {
            newCloud = Instantiate(cloudTypes[Random.Range(0, cloudTypes.Length-1)], cloudSpawn.position, Quaternion.identity, cloudSpawn);
        }
        else
        {
            newCloud = Instantiate(cloudTypes[Random.Range(0, cloudTypes.Length)], cloudSpawn.position, Quaternion.identity, cloudSpawn);
            if (newCloud.State == WeatherState.rainy)
            {
                rainCount++;
            }
        }
        newCloud.WeatherController = this;
        addedCloud = true;
    }
    public void SetCurrentCloud(Cloud cloud)
    {
        if (cloud.transform.position.z < currentCloud.transform.position.z)
        {
            currentCloud = cloud;
        }
    }
    public void DestroyCloud(Cloud cloud)
    {
        if (cloud.State == WeatherState.rainy)
        {
            rainCount--;
        }
        decayingCloud = cloud;
        removedCloud = true;
    }

    public void MoveClouds()
    {
        addedCloud = false;
        removedCloud = false;
        CalcGlobalWind();
        foreach (Cloud cloud in clouds)
        {
            cloud.Move(globalWind / windToSpeedDivider);
        }
        if (addedCloud)
        {
            newCloud.Move(globalWind / windToSpeedDivider);
            clouds.Add(newCloud);
            newCloud = null;
        }
        if (removedCloud)
        {
            clouds.Remove(decayingCloud);
            Destroy(decayingCloud.gameObject);
            decayingCloud = null;
        }
        
    }
    private void CalcGlobalWind()
    {
        globalWind += (Random.Range(-2, 3) * windChange);
        globalWind = Mathf.Clamp(globalWind, minWind, maxWind);
    }

    public int CalcLocalWind(int stablePlantSum)
    {
        if (stablePlantSum != 0)
        {
            localWind = globalWind / stablePlantSum;
        }
        else
        {
            localWind = globalWind;
        }
        return localWind;
    }
}
