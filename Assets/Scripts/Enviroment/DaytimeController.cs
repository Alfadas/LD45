using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaytimeController : MonoBehaviour
{
    [SerializeField] float normalSpeed = 1;
    [SerializeField] float lightStrengthMulti;
    [SerializeField] int nightMulti = 3;
    [SerializeField] Transform sunCycle;
    bool isNight;

    public float Sunlight
    {
        get
        {
            if (isNight)
            {
                return 0;
            }
            float rawLight = sunCycle.rotation.z;
            return (-2*(rawLight*rawLight) + 2* rawLight) * lightStrengthMulti;
        }
    }
    public bool IsNight
    {
        get { return isNight; }
    }
    public void ProgressDay()
    {
        Vector3 currentRotation = sunCycle.rotation.eulerAngles;
        Vector3 newRotation = currentRotation;
        if (sunCycle.rotation.w > 0)
        {
            newRotation = Rotate(currentRotation, normalSpeed * nightMulti);
            isNight = true;
        }
        else
        {
            newRotation = Rotate(currentRotation, normalSpeed);
            isNight = false;
        }
        sunCycle.rotation = Quaternion.Euler(newRotation);
    }

    private Vector3 Rotate(Vector3 currentRotation, float speed)
    {
        return currentRotation + new Vector3(0, 0, speed);
    }
}
