using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravityScale;
    private CharacterController characterController;

    private Vector3 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //movementDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, movementDirection.y, Input.GetAxis("Vertical") * moveSpeed);

        float tempY = movementDirection.y;


        movementDirection = transform.forward * Input.GetAxis("Vertical") + 
            transform.right * Input.GetAxis("Horizontal");
        movementDirection = movementDirection.normalized * moveSpeed;

        movementDirection.y = tempY;
        
        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
                movementDirection.y = jumpHeight;

        if (!characterController.isGrounded)
        {
            movementDirection.y += Physics.gravity.y * gravityScale * Time.deltaTime;
        }

        characterController.Move(movementDirection * Time.deltaTime);
    }
}
