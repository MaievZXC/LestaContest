using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform pivot;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = target.position - offset;

        pivot.position = target.position;
        pivot.parent = target;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        

        float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        target.Rotate(0, horizontalRotation, 0);

        float verticalRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
        pivot.Rotate(-verticalRotation, 0, 0);


        float angleY = target.eulerAngles.y;
        float angleX = pivot.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(angleX, angleY, 0);
        transform.position = target.position + rotation * offset;

        if (transform.position.y < target.position.y)
            transform.position = new Vector3(transform.position.x, target.position.y , transform.position.z);

        transform.LookAt(target);
    }
}
