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
            --CurrentHP;
            if (CurrentHP < 1)
            {
                other.gameObject.SetActive(false);
                Destroyed();
                uiManager.AddScore(Score);
            }
        }
    }

    public void Destroyed()
    {
        IsDestroyed = true;
        gameObject.SetActive(false);
    }
}
