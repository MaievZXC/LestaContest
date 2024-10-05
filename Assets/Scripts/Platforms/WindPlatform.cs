using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WindPlatform : MonoBehaviour
{
    [SerializeField] private float cooldawn;
    [SerializeField] private float windPower;
    private CharacterController controller;
    private Animator animator;
    private float currentCooldawn;

    private int direction;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        currentCooldawn = cooldawn;
        direction = Random.Range(1, 4);


    }

    // Update is called once per frame
    void Update()
    {
        if(currentCooldawn < 0)
        {
            direction = Random.Range(1, 5);
            currentCooldawn = cooldawn;

            switch (direction)
            {
                case 1:
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case 2:
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    break;
                case 3:
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    break;
                case 4:
                    transform.rotation = Quaternion.Euler(0, 270, 0);
                    break;
            }
        }


        currentCooldawn -= Time.deltaTime;
        
    }

    private void OnTriggerStay(Collider other)
    {
        controller = other.GetComponent<CharacterController>();

        switch (direction)
        {
            case 1:
                controller.Move(new Vector3(windPower * Time.deltaTime, 0, 0));
                break;
            case 2:
                controller.Move(new Vector3(0, 0, -windPower * Time.deltaTime));
                break;
            case 3:
                controller.Move(new Vector3(-windPower * Time.deltaTime, 0, 0));
                break;
            case 4:
                controller.Move(new Vector3(0, 0, windPower * Time.deltaTime));
                break;
        }
    }



    private bool IsWithinWindArea(Vector3 target)
    {
        return target.x > transform.position.x - transform.lossyScale.x / 1.5 &&
            target.x < transform.position.x + transform.lossyScale.x / 1.5 &&
            target.z > transform.position.z - transform.lossyScale.z / 1.5 &&
            target.z < transform.position.z + transform.lossyScale.z / 1.5;
    }

}
