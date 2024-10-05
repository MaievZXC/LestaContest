using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ActivationPlatformScript : MonoBehaviour
{
    [SerializeField] private float activationTime;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] private float cooldawn;
    private Animator animator;
    private float currentCooldawn;
    private BoxCollider trigger;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

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

    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag == "Player" && currentCooldawn < 0)
            StartCoroutine(Activation(activationTime, other));
    }




    private IEnumerator Activation(float waitTime, Collider other)
    {
        animator.SetTrigger("activation");

        currentCooldawn = cooldawn + activationTime;


        yield return new WaitForSeconds(waitTime);
        if (IsWithinDamageArea(other.transform.position))
        {
            other.transform.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }

    private bool IsWithinDamageArea(Vector3 target)
    {
        return target.x > transform.position.x - transform.lossyScale.x/ 2 &&
            target.x < transform.position.x + transform.lossyScale.x / 2 &&
            target.z > transform.position.z - transform.lossyScale.z / 2 &&
            target.z < transform.position.z + transform.lossyScale.z / 2;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(trigger.center, 1);
    }
}
