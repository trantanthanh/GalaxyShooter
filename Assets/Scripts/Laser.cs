using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float speed = 30f;
    [SerializeField] float topBound = 8f;
    [SerializeField] float bottomBound = -8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        CheckBound();
    }

    private void CheckBound()
    {
        if (transform.position.y > topBound || transform.position.y < bottomBound) 
        {
            gameObject.SetActive(false);
        }
    }

    private void Moving()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
    }
}
