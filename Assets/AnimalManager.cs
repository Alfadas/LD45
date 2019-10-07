using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    [SerializeField] int foodNeedPerAnimal;
    [SerializeField] int animalCount;

    public int FeedAnimals(int foodCount)
    {
        return foodCount / foodNeedPerAnimal;
    }
}
