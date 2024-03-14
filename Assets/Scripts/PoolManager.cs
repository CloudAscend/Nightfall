using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolObject
{
    public GameObject poolObject;
    public int maxAmount;
    public string key;
}

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance { get; private set; }

    public class ObjectPoolQueueInfo
    {
        public ObjectPoolQueueInfo(Transform parent, PoolObject info)
        {
            this.parent = parent;
            this.info = info;

            queue = new();
            for (int i = 0; i < info.maxAmount; i++)
            {
                var obj = Instantiate(info.poolObject);
                obj.transform.SetParent(parent);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
        }

        public Transform parent;
        public PoolObject info;
        public Queue<GameObject> queue = new();
    }

    public PoolObject[] poolInfo;
    private readonly Dictionary<string, ObjectPoolQueueInfo> pools = new();

    private void Awake()
    {
        instance = this;

        foreach (var info in poolInfo)
        {
            var pObj = new GameObject($"{info.key}");
            pObj.transform.SetParent(transform);
            pools.Add(info.key, new ObjectPoolQueueInfo(pObj.transform, info));
        }
    }

    //Getting Object(Spawn)
    public GameObject GetObject(string key, Vector3 position = default, Quaternion rotation = default)
    {
        if (pools[key].queue.Count == 0)
        {
            var newObj = Instantiate(pools[key].info.poolObject, position, rotation);
            newObj.SetActive(true);
            return newObj;
        }

        var target = pools[key].queue.Dequeue(); //Out from Queue
        target.transform.position = position;
        target.transform.rotation = rotation;
        target.transform.SetParent(null);
        target.SetActive(true);

        return target;
    }

    //Pooling Object(Go back to Pool)
    //Only the object self can Pool.
    public void PoolObject(string key, GameObject obj)
    {
        //if this object is over than maxAmound destroy it self.
        if (pools[key].queue.Count >= pools[key].info.maxAmount)
        {
            Destroy(obj);
        }
        else
        {
            pools[key].queue.Enqueue(obj);
            obj.SetActive(false);
            obj.transform.SetParent(pools[key].parent);
        }
    }
}
