using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveFX : MonoBehaviour
{
    [SerializeField] float timeToDeactive = 2.38f;
    float nextTimeToDeactive = 0f;
    // Start is called before the first frame update
    void OnEnable()
    {
        nextTimeToDeactive = Time.time + timeToDeactive;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextTimeToDeactive)
        {
            gameObject.SetActive(false);
        }
    }
}
