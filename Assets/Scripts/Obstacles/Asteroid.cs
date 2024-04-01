using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Obstacle
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsDestroyed) return;
        if (other.CompareTag("Player"))
        {
            //gameObject.SetActive(false);
            Destroyed();
            if (player != null)
            {
                player.Damage();
            }
        }
        else if (other.gameObject.CompareTag("Laser"))
        {
            other.gameObject.SetActive(false);
            --CurrentHP;
            if (CurrentHP < 1)
            {
                Destroyed();
                uiManager.AddScore(Score);
            }
        }
    }

    public void Destroyed()
    {
        PlaySoundDestroyed();
        IsDestroyed = true;
        gameObject.SetActive(false);
        spawnManager.SpawnExplosionFx(transform.position);
    }
}
