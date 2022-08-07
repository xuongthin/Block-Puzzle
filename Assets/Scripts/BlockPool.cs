using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPool : MonoBehaviour
{
    public static BlockPool Instance;
    void Awake()
    {
        Instance = this;
    }

    public List<GameObject> pooledBlocks;
    public GameObject prefabBlock;
    public int amountToPool;

    private void Start()
    {
        pooledBlocks = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(prefabBlock);
            tmp.SetActive(false);
            pooledBlocks.Add(tmp);
        }
    }

    public GameObject GetInactiveBlock()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledBlocks[i].activeInHierarchy)
            {
                return pooledBlocks[i];
            }
        }

        GameObject newObject = Instantiate(prefabBlock);
        newObject.SetActive(false);
        pooledBlocks.Add(newObject);
        return newObject;
    }
}
