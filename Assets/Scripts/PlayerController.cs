using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravityScale;
    private CharacterController characterController;

    private Animator animator;

    private Vector3 movementDirection;
    [SerializeField] private Transform pivot;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject playerModel;

    // Start is called before the first frame update
    void Start()
    {
        animator = playerModel.transform.GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //movementDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, movementDirection.y, Input.GetAxis("Vertical") * moveSpeed);

        float tempY = movementDirection.y;

        movementDirection = pivot.transform.forward * Input.GetAxisRaw("Vertical") +
            pivot.transform.right * Input.GetAxisRaw("Horizontal");
        movementDirection = movementDirection.normalized * moveSpeed;

        movementDirection.y = tempY;
        
        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
                movementDirection.y = jumpHeight;

        if (!characterController.isGrounded)
        {
            movementDirection.y += Physics.gravity.y * gravityScale * Time.deltaTime;
        }


        //model logic
        if (System.Math.Abs(movementDirection.x) + System.Math.Abs(movementDirection.z) > 0)
        {
            animator.SetBool("moving", true);
            //Smooth player rotation towards camera direction
            //transform.rotation = Quaternion.Euler(0, pivot.rotation.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, pivot.rotation, rotationSpeed * Time.deltaTime);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(movementDirection.x, transform.position.y, movementDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation,
                newRotation, rotationSpeed * 3 * Time.deltaTime);
        }
        else
            animator.SetBool("moving", false);


        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        playerModel.transform.rotation = Quaternion.Euler(0, playerModel.transform.rotation.eulerAngles.y, 0);
        characterController.Move(movementDirection * Time.deltaTime);

    }
}
