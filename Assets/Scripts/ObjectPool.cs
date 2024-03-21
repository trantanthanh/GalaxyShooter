using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Laser Pool configuration")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] int laserPoolSize = 5;
    List<GameObject> poolLaser = new List<GameObject>();

    public GameObject GetActiveLaserInPool()
    {
        for (int i = 0; i < poolLaser.Count; i++)
        {
            if (!poolLaser[i].activeInHierarchy)
            {
                return poolLaser[i];
            }
        }

        if (poolLaser.Count < laserPoolSize)
        {
            GameObject laser = Instantiate(laserPrefab, transform);
            laser.SetActive(false);
            poolLaser.Add(laser);
            return laser;
        }

        return null;
    }
}
