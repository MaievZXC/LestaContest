using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float startPosition;

    private BoxCollider trigger;

    private Vector3 start;

    void Start()
    {
        //stupidest thing i have ever done
        trigger = GetComponent<BoxCollider>();
        trigger.size = new Vector3(trigger.size.x, trigger.size.y + 1, trigger.size.z);
        //end of nightmare
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = start + direction * (float) Math.Sin((Time.time + startPosition) * speed) * distance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            other.transform.parent = transform;
               
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            other.transform.parent = null;
    }
}
