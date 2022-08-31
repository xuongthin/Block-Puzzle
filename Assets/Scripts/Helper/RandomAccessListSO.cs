using System.Collections.Generic;
using UnityEngine;

public class RandomAccessListSO<T, X> : ScriptableObject
    where T : class
    where X : RALItem<T>
{
    [SerializeField] private List<X> list;

    public void Clear()
    {
        list.Clear();
    }

    public T Get()
    {
        var seed = Random.Range(0.0f, 100.0f);
        var currentSumProbability = 0.0d;

        foreach (var item in list)
        {
            currentSumProbability += item.probability;
            if (seed < currentSumProbability)
                return item.value;
        }

        return null;
    }

    private float GetSumProbabilities()
    {
        float sum = 0;

        foreach (var item in list)
        {
            sum += item.probability;
        }

        return sum;
    }
}

public class RALItem<T> where T : class
{
    public T value;
    [Range(0.0f, 100.0f)] public float probability;
}