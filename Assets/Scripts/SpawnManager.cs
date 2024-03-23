using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float spawnInterval = 3f;
    bool _stopSpawning = false;

    [SerializeField] float timeSpentToSpawnPowerUp = 10f;
    float nextSpawnPowerUp = 0f;

    Player player;
    ObjectPool pools;
    // Start is called before the first frame update
    void Start()
    {
        InitPowerUpTimeSpawn();
        pools = FindObjectOfType<ObjectPool>();
        player = FindObjectOfType<Player>();
        StartCoroutine(SpawnEnemy());
    }

    public void InitPowerUpTimeSpawn()
    {
        nextSpawnPowerUp = Time.time + timeSpentToSpawnPowerUp;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpawnPowerUp();
    }

    private void CheckSpawnPowerUp()
    {
        if (!player.IsTripleShotActive && Time.time >= nextSpawnPowerUp)
        {
            nextSpawnPowerUp = Time.time + timeSpentToSpawnPowerUp;
            SpawnPowerUp();
        }
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

    void SpawnPowerUp()
    {
        GameObject powerUp = pools.GetActiveInPool(ObjectPool.PoolsName.POWER_UP);
        if (powerUp != null)
        {
            powerUp.SetActive(true);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
