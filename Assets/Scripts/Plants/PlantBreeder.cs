using System.Collections.Generic;
using UnityEngine;

public class PlantBreeder : MonoBehaviour
{
    [SerializeField] private List<BreedableCombination> combis;
    [SerializeField] private PlantBag plantBag;
    [SerializeField] private List<Plant> plantTypes;
    
    public Plant Breed(Plant first, Plant second)
    {
        combis = new List<BreedableCombination>
        {
            new BreedableCombination(plantTypes[0], plantTypes[1])
            {
                Result = plantTypes[2]
            }
        };

        if (first == null || second == null) return null;
        if (first == second) return null;

        var testCombi = new BreedableCombination(first, second);

        var result = combis.Find(combination => combination.Equals(testCombi));

        if (result != null && !plantBag.IsInPlantBag(result.Result))
        {
            plantBag.AddType(result.Result);
            return result.Result;
        }

        return null;
    }
}

class BreedableCombination
{
    private readonly Plant first;
    private readonly Plant second;
    public Plant Result;

    public BreedableCombination(Plant first, Plant second)
    {
        this.first = first;
        this.second = second;
        Result = null;
    }

    public bool Equals(BreedableCombination combination)
    {
        return (first.Name == combination.first.Name && second.Name == combination.second.Name) ||
               (first.Name == combination.second.Name && second.Name == combination.first.Name);
    }
}