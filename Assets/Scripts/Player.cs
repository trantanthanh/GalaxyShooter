using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speedMove = 10f;
    [SerializeField] float fireRate = 0.5f;
    float nextFire = 0.0f;
    [SerializeField] Vector3 fireOffset;

    [Header("Boundary")]
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    float horizontalInput;
    float verticalInput;
    bool isMovingByMouse = false;
    bool isFiring = false;

    ObjectPool pool;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
        pool = FindObjectOfType<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        Firing();
    }

    private void Firing()
    {
        nextFire -= Time.deltaTime;
        if (pool != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isFiring = true;
            }

            if (isFiring)
            {
                GameObject laser = pool.GetActiveLaserInPool();
                if (laser != null && nextFire <= 0)
                {
                    nextFire = fireRate;
                    laser.transform.position = transform.position + fireOffset;
                    laser.SetActive(true);
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
        }
        else
        {
            //Moving by keyboard
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            //transform.Translate(Vector3.right * horizontalInput * speedMove * Time.deltaTime);
            //transform.Translate(Vector3.up * verticalInput * speedMove * Time.deltaTime);
            Vector3 direction = new Vector3(horizontalInput, verticalInput, transform.position.z);
            transform.Translate(direction * speedMove * Time.deltaTime);

        }

        Vector3 deltaMove;
        if (isMovingByMouse && Input.GetMouseButton(0))
        {
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = transform.position.z;//Keep position z
            Vector3 moveDistance = targetPosition - transform.position;
            Vector3 moveDirection = moveDistance.normalized;
            deltaMove = moveDirection * speedMove * Time.deltaTime;
            if (deltaMove.magnitude > moveDistance.magnitude)
            {
                deltaMove = moveDistance;
            }
            transform.position = transform.position + deltaMove;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMovingByMouse = false;
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
}
