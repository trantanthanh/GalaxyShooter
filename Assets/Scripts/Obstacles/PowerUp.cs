using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static PowerUp;

public class PowerUp : Enemy
{
    public enum PowerUpOptions
    {
        TRIPLE_SHOT,
        SPEED_UP,
        SHIELD
    }
    [SerializeField] PowerUpOptions powerUpOption;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);//Collected powerup
            switch (powerUpOption)
            {
                case PowerUpOptions.TRIPLE_SHOT:
                    {
                        player.ActiveTripleShot();
                        break;
                    }
                case PowerUpOptions.SPEED_UP:
                    {
                        player.ActiveSpeedUp();
                        break;
                    }
                case PowerUpOptions.SHIELD:
                    {
                        player.ActiveShield();
                        break;
                    }
            }
        }
    }
}
