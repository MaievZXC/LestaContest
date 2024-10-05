using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CatapultePlatform : MonoBehaviour
{
    [SerializeField] private float activationTime;
    [SerializeField] private float cooldawn;
    [SerializeField] private float power;
    private float currentPower;
    private Animator animator;
    private float currentCooldawn;
    private Material material;
    private BoxCollider trigger;
    private bool inActivated;
    


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentPower = power;
        currentCooldawn = 0;

        //stupidest thing i have ever done
        trigger = GetComponent<BoxCollider>();
        trigger.size = new Vector3(trigger.size.x, trigger.size.y + 1, trigger.size.z);
        //end of nightmare

        
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldawn -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && currentCooldawn < 0)
            StartCoroutine(Activation(activationTime, other));
    }




    private IEnumerator Activation(float waitTime, Collider other)
    {
        //animator.SetTrigger("activation");


        yield return new WaitForSeconds(waitTime);
        if (IsWithinDamageArea(other.transform.position))
        {
            StartCoroutine(Flight(activationTime, other));


        }
        currentCooldawn = cooldawn;
    }

    private IEnumerator Flight(float time, Collider other)
    {
        CharacterController characterController = other.GetComponent<CharacterController>();
        for (int i = 0; i < 500; i++)
        {
            characterController.Move(new Vector3(currentPower, currentPower, 0));
            currentPower -= currentPower / 500 / 500;
            yield return new WaitForSeconds(time / 500);
        }
        currentPower = power;
    }


    private bool IsWithinDamageArea(Vector3 target)
    {
        return target.x > transform.position.x - transform.lossyScale.x / 2 &&
            target.x < transform.position.x + transform.lossyScale.x / 2 &&
            target.z > transform.position.z - transform.lossyScale.z / 2 &&
            target.z < transform.position.z + transform.lossyScale.z / 2;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(trigger.center, 1);
    }
}
