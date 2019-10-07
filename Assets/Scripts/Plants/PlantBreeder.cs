using System;
using System.Collections.Generic;
using UnityEngine;

public class PlantBreeder : MonoBehaviour
{
    [SerializeField] private List<BreedableCombination> combis;
    [SerializeField] private PlantBag plantBag;
    [SerializeField] private List<Plant> plantTypes;

    private void Awake()
    {
        combis = new List<BreedableCombination>
        {
            Combi("Grass1", "Grass1", "Grass2"),
            Combi("Grass1", "Grass3", "Bush1"),
            Combi("Bush1", "Bush1", "Bush2"),
            Combi("Grass2", "Grass3", "Tomato"),
            Combi("Tomato", "Bush2", "BerryBush"),
            Combi("Grass3", "Grass3", "Onion"),
            Combi("Grass2", "Grass2", "Grass3"),
            Combi("Bush2", "Bush2", "Tree"),
            Combi("Onion", "Grass3", "Carrot")
        };
    }

    public Plant Breed(Plant first, Plant second)
    {
        if (first == null || second == null) return null;

        var testCombi = new BreedableCombination(first, second, null);

        var result = combis.Find(combination => combination.Equals(testCombi));

        if (result != null && !plantBag.IsInPlantBag(result.result))
        {
            plantBag.AddType(result.result);
            return result.result;
        }

        return null;
    }

    public Plant FindByName(string name)
    {
        return plantTypes.Find(type => type.Name.Equals(name));
    }

    private BreedableCombination Combi(string first, string second, string result)
    {
        return new BreedableCombination(FindByName(first), FindByName(second), FindByName(result));
    }
}

class BreedableCombination
{
    private readonly Plant first;
    private readonly Plant second;
    public Plant result;

    public BreedableCombination(Plant first, Plant second, Plant result
    )
    {
        this.first = first;
        this.second = second;
        this.result = result;
    }

    public bool Equals(BreedableCombination combination)
    {
        return (first.Name == combination.first.Name && second.Name == combination.second.Name) ||
               (first.Name == combination.second.Name && second.Name == combination.first.Name);
    }
}