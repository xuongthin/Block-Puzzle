using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private Stack<PooledObject> stack;
    private PooledObject prefab;
    private int amountToPool;
    private Transform transform;

    public ObjectPool(PooledObject prefab, int amountToPool, Transform transform)
    {
        stack = new Stack<PooledObject>();
        this.prefab = prefab;
        this.amountToPool = amountToPool;
        this.transform = transform;

        for (int i = 0; i < amountToPool; i++)
        {
            CreateObject();
        }
    }

    public void Add(PooledObject obj)
    {
        stack.Push(obj);
        obj.transform.parent = transform;
    }

    public PooledObject Release()
    {
        PooledObject obj;
        if (stack.Count > 0)
            obj = stack.Pop();
        else
            obj = CreateObject();

        obj.OnRelease();
        return obj;
    }

    private PooledObject CreateObject()
    {
        PooledObject obj = Object.Instantiate(prefab, transform);
        obj.OnCreated(this);
        Add(obj);

        return obj;
    }
}
