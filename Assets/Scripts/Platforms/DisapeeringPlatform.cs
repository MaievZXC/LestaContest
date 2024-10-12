using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DisapeeringPlatform : MonoBehaviour
{
    public static List<DisapeeringPlatform> instance;
    [SerializeField] private float activationTime;
    private Animator animator;
    private BoxCollider trigger;
    private bool isActivated;


    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
            instance = new List<DisapeeringPlatform>();
        isActivated = false;
        animator = GetComponent<Animator>();
        //stupidest thing i have ever done
        trigger = GetComponent<BoxCollider>();
        trigger.size = new Vector3(trigger.size.x, trigger.size.y + 1, trigger.size.z);
        //end of nightmare
    }

    // Update is called once per frame

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player" && !isActivated)
            StartCoroutine(Activation(activationTime, other));
    }




    private IEnumerator Activation(float waitTime, Collider other)
    {
        animator.SetTrigger("activation");


        yield return new WaitForSeconds(waitTime);
        instance.Add(this);
        gameObject.SetActive(false);
    }
}
