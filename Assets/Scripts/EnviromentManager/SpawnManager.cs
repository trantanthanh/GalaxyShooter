using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float spawnInterval = 3f;
    bool _stopSpawning = false;

    [SerializeField] float timeSpentToSpawnTripleShotPowerUp = 6f;
    float nextSpawnTripleShotPowerUp = 0f;

    [SerializeField] float timeSpentToSpawnSpeedPowerUp = 8f;
    float nextSpawnSpeedPowerUp = 0f;

    [SerializeField] float timeSpentToSpawnShieldPowerUp = 10f;
    float nextSpawnShieldPowerUp = 0f;

    Player player;
    ObjectPool pools;
    // Start is called before the first frame update
    void Start()
    {
        InitTimerPowerUp();
        pools = FindObjectOfType<ObjectPool>();
        player = FindObjectOfType<Player>();
        StartCoroutine(SpawnEnemy());
    }

    void InitTimerPowerUp()
    {
        InitPowerUpTimeSpawn(PowerUp.PowerUpOptions.TRIPLE_SHOT);
        InitPowerUpTimeSpawn(PowerUp.PowerUpOptions.SPEED_UP);
        InitPowerUpTimeSpawn(PowerUp.PowerUpOptions.SHIELD);
    }

    public void InitPowerUpTimeSpawn(PowerUp.PowerUpOptions powerUpType)
    {
        switch (powerUpType)
        {
            case PowerUp.PowerUpOptions.TRIPLE_SHOT:
                {
                    nextSpawnTripleShotPowerUp = Time.time + timeSpentToSpawnTripleShotPowerUp;
                    break;
                }
            case PowerUp.PowerUpOptions.SPEED_UP:
                {
                    nextSpawnSpeedPowerUp = Time.time + timeSpentToSpawnSpeedPowerUp;
                    break;
                }
            case PowerUp.PowerUpOptions.SHIELD:
                {
                    nextSpawnShieldPowerUp = Time.time + timeSpentToSpawnShieldPowerUp;
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpawnPowerUp(PowerUp.PowerUpOptions.TRIPLE_SHOT);
        CheckSpawnPowerUp(PowerUp.PowerUpOptions.SPEED_UP);
        CheckSpawnPowerUp(PowerUp.PowerUpOptions.SHIELD);
    }

    private void CheckSpawnPowerUp(PowerUp.PowerUpOptions powerType)
    {
        if (_stopSpawning) return;

        bool needSpawn = false;

        switch (powerType)
        {
            case PowerUp.PowerUpOptions.TRIPLE_SHOT:
                {
                    if (!player.IsTripleShotActive && Time.time >= nextSpawnTripleShotPowerUp)
                    {
                        needSpawn = true;
                    }
                    break;
                }
            case PowerUp.PowerUpOptions.SPEED_UP:
                {
                    if (!player.IsSpeedUpActive && Time.time >= nextSpawnSpeedPowerUp)
                    {
                        needSpawn = true;
                    }
                    break;
                }
            case PowerUp.PowerUpOptions.SHIELD:
                {
                    if (!player.IsShieldActive && Time.time >= nextSpawnShieldPowerUp)
                    {
                        needSpawn = true;
                    }
                    break;
                }
        }

        if (needSpawn)
        {
            InitPowerUpTimeSpawn(powerType);
            SpawnPowerUp(powerType);
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

    void SpawnPowerUp(PowerUp.PowerUpOptions powerUpType)
    {
        GameObject powerUp = null;
        switch (powerUpType)
        {
            case PowerUp.PowerUpOptions.TRIPLE_SHOT:
                {
                    powerUp = pools.GetActiveInPool(ObjectPool.PoolsName.TRIPLE_SHOT_POWER_UP);
                    break;
                }
            case PowerUp.PowerUpOptions.SPEED_UP:
                {
                    powerUp = pools.GetActiveInPool(ObjectPool.PoolsName.SPEED_POWER_UP);
                    break;
                }
            case PowerUp.PowerUpOptions.SHIELD:
                {
                    powerUp = pools.GetActiveInPool(ObjectPool.PoolsName.SHIELD_POWER_UP);
                    break;
                }
        }

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
