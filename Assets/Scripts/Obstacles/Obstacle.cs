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

    [SerializeField] float timeDelayDeactive = 2.30f;
    bool isDestroyed = false;
    public bool IsDestroyed { get { return isDestroyed; } set { isDestroyed = value; } }

    static protected Player player;
    static protected UIManager uiManager;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
            //Debug.Log("Find player object");
        }

        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();
        }

        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        isDestroyed = false;
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

    public void Destroyed()
    {
        //animator.ResetTrigger("OnEnemyDeath");
        animator.SetTrigger("OnEnemyDeath");
        isDestroyed = true;
        Invoke("DelayDeactive", timeDelayDeactive);
    }

    private void DelayDeactive()
    {
        gameObject.SetActive(false);
    }
}
