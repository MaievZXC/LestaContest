using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform pivot;
    [SerializeField] private bool invertY;

    [Header ("Camera restrictions")]
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            // Если экземпляр не существует, назначаем текущий объект и не уничтожаем его при загрузке новой сцены
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Если экземпляр уже существует, уничтожаем текущий объект, чтобы сохранить единственность
            Destroy(gameObject);
        }


        transform.position = target.position - offset;

        pivot.position = target.position;
        //pivot.parent = target;
        pivot.parent = null;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        pivot.transform.position = target.transform.position;

        float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        pivot.Rotate(0, horizontalRotation, 0, Space.World);

        float verticalRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
        if (invertY)
            pivot.Rotate(-verticalRotation, 0, 0, Space.Self);
        else
            pivot.Rotate(verticalRotation, 0, 0, Space.Self);

        //Limit camera rotating
        if (pivot.rotation.eulerAngles.x > maxAngle && pivot.rotation.eulerAngles.x < 180)
        {
            pivot.rotation = Quaternion.Euler(maxAngle, pivot.rotation.eulerAngles.y, 0);
        }
        else if (pivot.rotation.eulerAngles.x < 360 - minAngle && pivot.rotation.eulerAngles.x > 180)
        {
            pivot.rotation = Quaternion.Euler(360 - minAngle, pivot.rotation.eulerAngles.y, 0);
        }

        float angleX = pivot.eulerAngles.x;
        float angleY = pivot.eulerAngles.y;

        Quaternion rotation = Quaternion.Euler(angleX, angleY, 0);
        transform.position = target.position + rotation * offset;

        if (transform.position.y < target.position.y)
            transform.position = new Vector3(transform.position.x, target.position.y , transform.position.z);

        transform.LookAt(target);
    }
}
