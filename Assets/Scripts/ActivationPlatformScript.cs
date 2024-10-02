using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ActivationPlatformScript : MonoBehaviour
{
    [SerializeField] private float activationTime;
    [SerializeField] private int numberOfFlashes;
    private Material material;
    private bool flag = true;
    private BoxCollider trigger;


    // Start is called before the first frame update
    void Start()
    {
        //stupidest thing i have ever done
        trigger = GetComponent<BoxCollider>();
        trigger.size = new Vector3(trigger.size.x, trigger.size.y + 1, trigger.size.z);
        //end of nightmare
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && flag)
            StartCoroutine(Activation(activationTime, other));
    }




    private IEnumerator Activation(float waitTime, Collider other)
    {
        flag = false;
        Color targetColor = new Color(200, 0, 0);
        Color originalColor = material.color;

        for (int i = 0; i < numberOfFlashes; i++)
        {
            material.color = originalColor;
            yield return new WaitForSeconds(waitTime / 2 / numberOfFlashes);
            material.color = targetColor;
            yield return new WaitForSeconds(waitTime / 2 / numberOfFlashes);
        }
        material.color = originalColor;
        flag = true;
        if (IsWithinDamageArea(other.transform.position))
        {
            print("ti loh");
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
