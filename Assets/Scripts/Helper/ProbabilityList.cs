using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilityList<T>
{
    private List<int> probabilities;
    private List<T> values;
    private int sum;

    public ProbabilityList()
    {
        probabilities = new List<int>();
        values = new List<T>();
        sum = 0;
    }

    public void Add(T value, int probability)
    {
        if (probability == 0)
            Debug.LogWarning("a value has probability equal zero. It can't be got");
        probabilities.Add(sum);
        values.Add(value);
        sum += probability;
    }

    public T Get()
    {
        int rand = Random.Range(0, sum + 1);
        for (int i = probabilities.Count - 1; i >= 0; i--)
        {
            if (rand >= probabilities[i])
                return values[i];
        }
        return values[0];
    }
}
