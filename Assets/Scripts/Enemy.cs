using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    [SerializeField] float leftBoundPos = -7f;
    [SerializeField] float rightBoundPos = 7f;
    [SerializeField] float topBoundPos = 8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        RandomPosSpawn();
    }

    private void RandomPosSpawn()
    {
        transform.position = new Vector3(Random.Range(leftBoundPos, rightBoundPos), topBoundPos, 0);
    }

    // Update is called once per frame
    void Update()
    {
        MovingDown();
    }

    private void MovingDown()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -4f)
        {
            RandomPosSpawn();
            //gameObject.SetActive(false);
        }
    }
}
