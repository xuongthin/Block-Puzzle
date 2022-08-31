using System.Collections.Generic;
using System;

public class RandomAccessList<T> where T : class
{
    private List<T> values;
    private List<double> probabilities;
    private Random random;
    private double sumProbability;

    public RandomAccessList()
    {
        values = new List<T>();
        probabilities = new List<double>();
        random = new Random();
        sumProbability = 0;
    }

    public void Clear()
    {
        values.Clear();
        sumProbability = 0;
    }

    public void Add(T value, double probability)
    {
        if (probability <= 0)
        {
            return;
        }

        values.Add(value);
        probabilities.Add(probability);

        sumProbability += probability;
    }

    public T Get()
    {
        var seed = random.NextDouble() * sumProbability;
        var currentSumProbability = 0.0d;

        for (int i = 0; i < values.Count; i++)
        {
            currentSumProbability += probabilities[i];
            if (seed <= currentSumProbability)
            {
                return values[i];
            }
        }

        return null;
    }
}
