using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float speedMultiplier = 2f;
    [SerializeField] float fireRate = 0.5f;
    float nextFire = 0.0f;
    [SerializeField] Vector3 fireOffset;
    [SerializeField] int lives = 3;
    int currentLives = 3;

    [Header("Boundary")]
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    float horizontalInput;
    float verticalInput;
    bool isMovingByMouse = false;
    bool isFiring = false;

    bool isTripleShotActive = false;
    [SerializeField] float timerTripleShotEffect = 3f;
    public bool IsTripleShotActive
    {
        get
        {
            return isTripleShotActive;
        }
    }

    bool isSpeedUpActive = false;
    [SerializeField] float timerSpeedUpEffect = 4f;
    public bool IsSpeedUpActive
    {
        get
        {
            return isSpeedUpActive;
        }
    }

    bool isShieldActive = false;
    [SerializeField] float timerShieldEffect = 5f;
    public bool IsShieldActive
    {
        get
        {
            return isShieldActive;
        }
    }

    [SerializeField] GameObject shieldPrefab;
    [SerializeField] GameObject tripleLaserPrefab;

    ObjectPool pools;
    SpawnManager spawnManager;
    UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        currentLives = lives;
        shieldPrefab.SetActive(false);
        transform.position = Vector3.zero;
        pools = FindObjectOfType<ObjectPool>();
        spawnManager = FindObjectOfType<SpawnManager>();
        uiManager = FindObjectOfType<UIManager>();
        uiManager.UpdateLives(currentLives);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        Firing();
    }

    private void Firing()
    {
        if (pools != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isFiring = true;
            }

            if (isFiring && Time.time >= nextFire)
            {
                if (isTripleShotActive)
                {
                    for (int i = 0; i < tripleLaserPrefab.transform.childCount; ++i)
                    {
                        Transform child = tripleLaserPrefab.transform.GetChild(i);
                        GameObject laser = pools.GetActiveInPool(ObjectPool.PoolsName.LASER);
                        if (laser != null)
                        {
                            nextFire = Time.time + fireRate;
                            laser.transform.position = transform.position + child.position;
                            laser.SetActive(true);
                        }
                    }
                }
                else
                {
                    GameObject laser = pools.GetActiveInPool(ObjectPool.PoolsName.LASER);
                    if (laser != null)
                    {
                        nextFire = Time.time + fireRate;
                        laser.transform.position = transform.position + fireOffset;
                        laser.SetActive(true);
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isFiring = false;
            }
        }
    }

    private void CalculateMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMovingByMouse = true;
            isFiring = true;
        }
        else
        {
            //Moving by keyboard
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            //transform.Translate(Vector3.right * horizontalInput * speedMove * Time.deltaTime);
            //transform.Translate(Vector3.up * verticalInput * speedMove * Time.deltaTime);
            Vector3 direction = new Vector3(horizontalInput, verticalInput, transform.position.z);
            transform.Translate(direction * speed * Time.deltaTime);

        }

        Vector3 deltaMove;
        if (isMovingByMouse)
        {
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = transform.position.z;//Keep position z
            Vector3 moveDistance = targetPosition - transform.position;
            Vector3 moveDirection = moveDistance.normalized;
            deltaMove = moveDirection * speed * Time.deltaTime;
            if (deltaMove.magnitude > moveDistance.magnitude)
            {
                deltaMove = moveDistance;
            }
            transform.position = transform.position + deltaMove;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMovingByMouse = false;
            isFiring = false;
        }

        //Check boundary
        if (transform.position.x > paddingRight)
        {
            transform.position = new Vector3(paddingLeft, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < paddingLeft)
        {
            transform.position = new Vector3(paddingRight, transform.position.y, transform.position.z);
        }
        transform.position = new Vector3(transform.position.x,
                                        Mathf.Clamp(transform.position.y, paddingBottom, paddingTop),
                                        transform.position.z);
    }

    public void Damage()
    {
        if (isShieldActive) return;
        --currentLives;
        uiManager.UpdateLives(currentLives);
        if (currentLives < 1)
        {
            Death();
        }
    }

    private void Death()
    {
        spawnManager.OnPlayerDeath();
        Destroy(gameObject);
    }

    public void ActiveTripleShot()
    {
        isTripleShotActive = true;
        StartCoroutine(CountDownDeactiveTripleShot());
    }

    IEnumerator CountDownDeactiveTripleShot()
    {
        yield return new WaitForSeconds(timerTripleShotEffect);
        spawnManager.InitPowerUpTimeSpawn(PowerUp.PowerUpOptions.TRIPLE_SHOT);
        isTripleShotActive = false;
    }

    public void ActiveSpeedUp()
    {
        isSpeedUpActive = true;
        speed *= speedMultiplier;
        StartCoroutine(CountDownDeactiveSpeedUp());
    }

    IEnumerator CountDownDeactiveSpeedUp()
    {
        yield return new WaitForSeconds(timerSpeedUpEffect);
        spawnManager.InitPowerUpTimeSpawn(PowerUp.PowerUpOptions.SPEED_UP);
        isSpeedUpActive = false;
        speed /= speedMultiplier;
    }

    public void ActiveShield()
    {
        isShieldActive = true;
        shieldPrefab.SetActive(true);
        StartCoroutine(CountDownDeactiveShield());
    }

    IEnumerator CountDownDeactiveShield()
    {
        yield return new WaitForSeconds(timerShieldEffect);
        spawnManager.InitPowerUpTimeSpawn(PowerUp.PowerUpOptions.SHIELD);
        isShieldActive = false;
        shieldPrefab.SetActive(false);
    }
}
