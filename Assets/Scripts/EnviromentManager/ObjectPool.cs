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

    [Header("Triple shot power up Pool configuration")]
    [SerializeField] GameObject tripleShotPowerUpPrefab;
    [SerializeField] int tripleShotPowerUpPoolSize = 1;

    [Header("Speed power up Pool configuration")]
    [SerializeField] GameObject speedPowerUpPrefab;
    [SerializeField] int speedPowerUpPoolSize = 1;

    [Header("Shield power up Pool configuration")]
    [SerializeField] GameObject shieldPowerUpPrefab;
    [SerializeField] int shieldPowerUpPoolSize = 1;

    public enum PoolsName
    {
        LASER,
        TRIPLE_SHOT_POWER_UP,
        SPEED_POWER_UP,
        SHIELD_POWER_UP,
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
        for (int i = 0; i < arrayNames.Length; i++)
        {
            pools.Add(arrayNames[i], new List<GameObject>());
        }
    }

    public GameObject GetActiveInPool(PoolsName name)
    {
        if (!pools.ContainsKey(name))
        {
            return null;
        }
        int poolLimit = 0;
        GameObject prefab = null;
        switch (name)
        {
            case PoolsName.LASER:
                {
                    poolLimit = laserPoolSize;
                    prefab = laserPrefab;
                    break;
                }
            case PoolsName.TRIPLE_SHOT_POWER_UP:
                {
                    poolLimit = tripleShotPowerUpPoolSize;
                    prefab = tripleShotPowerUpPrefab;
                    break;
                }
            case PoolsName.SPEED_POWER_UP:
                {
                    poolLimit = speedPowerUpPoolSize;
                    prefab = speedPowerUpPrefab;
                    break;
                }
            case PoolsName.SHIELD_POWER_UP:
                {
                    poolLimit = shieldPowerUpPoolSize;
                    prefab = shieldPowerUpPrefab;
                    break;
                }
            case PoolsName.ENEMY:
                {
                    poolLimit = enemyPoolSize;
                    prefab = enemyPrefab;
                    break;
                }
        }

        for (int i = 0; i < pools[name].Count; i++)
        {
            if (!pools[name][i].activeInHierarchy)
            {
                return pools[name][i];
            }
        }

        if (pools[name].Count < poolLimit)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            pools[name].Add(obj);
            return obj;
        }

        return null;
    }
}
