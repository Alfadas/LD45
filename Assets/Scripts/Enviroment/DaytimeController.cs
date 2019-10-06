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
            float rawLight = sunCycle.rotation.z - 0.5f;
            if (rawLight < 0)
            {
                rawLight *= -1;
            }
            return rawLight * lightStrengthMulti;
        }
    }
    public bool IsNight
    {
        get { return isNight; }
    }
    void Update()
    {
        Vector3 currentRotation = sunCycle.rotation.eulerAngles;
        Vector3 newRotation = currentRotation;
        float speed = normalSpeed * Time.deltaTime;
        if (sunCycle.rotation.w > 0)
        {
            newRotation = Rotate(currentRotation, speed * nightMulti);
            isNight = true;
        }
        else
        {
            newRotation = Rotate(currentRotation, speed);
            isNight = false;
        }
        sunCycle.rotation = Quaternion.Euler(newRotation);

    }

    private Vector3 Rotate(Vector3 currentRotation, float speed)
    {
        return currentRotation + new Vector3(0, 0, speed);
    }
}
