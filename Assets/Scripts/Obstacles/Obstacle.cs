using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    [SerializeField] float leftBoundPos = -7f;
    [SerializeField] float rightBoundPos = 7f;
    [SerializeField] float topBoundPos = 8f;
    [SerializeField] int score = 0;
    public int Score { get { return score; } set { score = value; } }

    protected Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
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
            gameObject.SetActive(false);
        }
    }
}
