using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float spawnInterval = 3f;
    bool _stopSpawning = false;
    ObjectPool pools;
    // Start is called before the first frame update
    void Start()
    {
        pools = FindObjectOfType<ObjectPool>();
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemy()
    {
        while (!_stopSpawning)
        {
            GameObject enemy = pools.GetActiveInPool(ObjectPool.PoolsName.ENEMY);
            if (enemy != null)
            {
                enemy.SetActive(true);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
