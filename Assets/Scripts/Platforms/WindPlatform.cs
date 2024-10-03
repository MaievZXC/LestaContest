using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WindPlatform : MonoBehaviour
{
    [SerializeField] private float cooldawn;
    [SerializeField] private PlayerController controller;
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
            direction = Random.Range(1, 4);
        }
        currentCooldawn -= Time.deltaTime;

        
    }



    private bool IsWithinWindArea(Vector3 target)
    {
        return target.x > transform.position.x - transform.lossyScale.x / 2 &&
            target.x < transform.position.x + transform.lossyScale.x / 2 &&
            target.z > transform.position.z - transform.lossyScale.z / 2 &&
            target.z < transform.position.z + transform.lossyScale.z / 2;
    }

}
