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

    [SerializeField] protected float timeDelayDeactive = 2.30f;
    bool isDestroyed = false;
    public bool IsDestroyed { get { return isDestroyed; } set { isDestroyed = value; } }

    static protected Player player;
    static protected UIManager uiManager;

    protected Animator animator;

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
        if (animator != null)
        {
            animator.Play("Idle");
        }
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
        if (isDestroyed)
        {
            return;
        }
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -8f)
        {
            gameObject.SetActive(false);
        }
    }
}
