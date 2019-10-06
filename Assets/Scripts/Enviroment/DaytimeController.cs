using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaytimeController : MonoBehaviour
{
    [SerializeField] float normalSpeed = 1;
    [SerializeField] int nightMulti = 3;
    [SerializeField] Transform sunCycle;
    void Update()
    {
        Vector3 currentRotation = sunCycle.rotation.eulerAngles;
        Vector3 newRotation = currentRotation;
        if (sunCycle.rotation.w > 0)
        {
            newRotation = Rotate(currentRotation, normalSpeed * nightMulti);
        }
        else
        {
            newRotation = Rotate(currentRotation, normalSpeed);
        }
        sunCycle.rotation = Quaternion.Euler(newRotation);

    }

    private Vector3 Rotate(Vector3 currentRotation, float speed)
    {
        return currentRotation + new Vector3(0, 0, -speed);
    }
}
