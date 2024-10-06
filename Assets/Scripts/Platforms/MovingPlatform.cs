using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector3 startPoint;
    [SerializeField] Vector3 endPoint;
    [SerializeField] float time;

    bool status;

    Vector3 velocity;

    void Start()
    {
        velocity = Vector3.zero;
        status = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == startPoint)
        {
            status = false;
        }else if (transform.position == endPoint)
        {
            status = true;
        }
        if (status)
        {
            Vector3.SmoothDamp(transform.position, endPoint, ref velocity, time);
        }
        else
        {
            Vector3.SmoothDamp(transform.position, startPoint, ref velocity, time);
        }
    }
}
