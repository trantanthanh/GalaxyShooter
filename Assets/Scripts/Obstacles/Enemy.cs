using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : Obstacle
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsDestroyed) return;
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            if (player != null)
            {
                player.Damage();
            }
        }
        else if (other.gameObject.CompareTag("Laser"))
        {
            other.gameObject.SetActive(false);
            Destroyed();
            uiManager.AddScore(Score);
        }
    }

    public void Destroyed()
    {
        animator.ResetTrigger("OnEnemyDeath");
        animator.SetTrigger("OnEnemyDeath");
        IsDestroyed = true;
        Invoke("DelayDeactive", timeDelayDeactive);
    }

    private void DelayDeactive()
    {
        gameObject.SetActive(false);
    }
}
