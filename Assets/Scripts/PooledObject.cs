using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private ObjectPool pool;

    public virtual void OnCreated(ObjectPool pool)
    {
        this.pool = pool;
        gameObject.SetActive(false);
    }

    public virtual void OnRelease()
    {
        gameObject.SetActive(true);
    }

    public virtual void Return()
    {
        pool.Add(this);
        gameObject.SetActive(false);
    }
}
