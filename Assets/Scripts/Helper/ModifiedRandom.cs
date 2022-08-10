using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModifiedRandom
{
    private List<int> values;
    private List<float> rangeStart;
    private float sumRandomSeeds;

    public ModifiedRandom()
    {
        values = new List<int>();
        rangeStart = new List<float>();
        sumRandomSeeds = 0;
    }

    public void Add(int value, float randomSeed)
    {
        float newSum = sumRandomSeeds + randomSeed;
        for (int i = 0; i < values.Count; i++)
        {
            rangeStart[i] *= sumRandomSeeds / newSum;
        }

        values.Add(value);
        rangeStart.Add(100.0f * sumRandomSeeds / newSum);
        sumRandomSeeds = newSum;
    }

    public int Get()
    {
        float seed = Random.Range(0.0f, 100.0f);
        for (int i = 0; i < values.Count - 1; i++)
        {
            if (seed >= rangeStart[i] && seed < rangeStart[i + 1])
            {
                return values[i];
            }
        }
        return values[values.Count - 1];
    }
}
