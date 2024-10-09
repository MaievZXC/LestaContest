using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CatapultePlatform : MonoBehaviour
{
    [SerializeField] private float activationTime;
    [SerializeField] private float cooldawn;
    [SerializeField] private float power;
    [SerializeField] private float flightTime;
    [SerializeField] private float vertivality;
    private float flightTimer;
    private CharacterController characterController;
    private float currentPower;
    private Animator animator;
    private float currentCooldawn;
    private Material material;
    private BoxCollider trigger;
    private bool isTriggered;
    private bool isActivated;




    // Start is called before the first frame update
    void Start()
    {
        isTriggered = false;
        isActivated = false;
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;

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
        if (isActivated)
        {
            switch (transform.rotation.eulerAngles.y)
            {
                case 0:
                    characterController.Move(new Vector3(currentPower * Time.deltaTime,
                        currentPower * Time.deltaTime * vertivality, 0));
                    break;
                case 90:
                    characterController.Move(new Vector3(0,
                        currentPower * Time.deltaTime * vertivality, -currentPower * Time.deltaTime));
                    break;
                case 180:
                    characterController.Move(new Vector3(-currentPower * Time.deltaTime,
                        currentPower * Time.deltaTime * vertivality, 0));
                    break;
                case 270:
                    characterController.Move(new Vector3(0,
                        currentPower * Time.deltaTime * vertivality, currentPower * Time.deltaTime));
                    break;

            }
            currentPower -= power * Time.deltaTime / flightTime;
        }
        else
        {
            currentPower = power;
            flightTimer = flightTime;
        }

        if (flightTimer < 0)
            isActivated = false;
        else
            flightTimer -= Time.deltaTime;

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
        characterController = other.GetComponent<CharacterController>();

        yield return new WaitForSeconds(waitTime);
        if (IsWithinDamageArea(other.transform.position))
        {
            isActivated = true;
        }
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
