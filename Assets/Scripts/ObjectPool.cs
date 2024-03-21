using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Laser Pool configuration")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] int laserPoolSize = 5;

    [Header("Enemy Pool configuration")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int enemyPoolSize = 20;

    public enum PoolsName { 
        LASER,
        ENEMY
    }

    Dictionary<PoolsName, List<GameObject>> pools = new Dictionary<PoolsName, List<GameObject>>();

    void Start()
    {
        CreatPools();
    }

    private void CreatPools()
    {
        PoolsName[] arrayNames = (PoolsName[])Enum.GetValues(typeof(PoolsName));
        for (int i = 0; i < arrayNames.Length; ++i)
        {
            pools.Add(arrayNames[i], new List<GameObject>());
        }
    }

    public GameObject GetActiveInPool(PoolsName name)
    {
        if (!pools.ContainsKey(name)) return null;
        int poolLimit = 0;
        List<GameObject> pool = pools[name];
        GameObject prefab = null;
        switch (name)
        {
            case PoolsName.LASER:
                {
                    poolLimit = laserPoolSize;
                    prefab = laserPrefab;
                    break;
                }
            case PoolsName.ENEMY:
                {
                    poolLimit = enemyPoolSize;
                    prefab = enemyPrefab;
                    break;
                }
        }

        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }

        if (pool.Count < poolLimit)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            pool.Add(obj);
            return obj;
        }

        return null;
    }
}
