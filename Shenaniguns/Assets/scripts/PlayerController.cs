using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [System.Serializable]
    public class PhysicsSettings
    {
        public float gravityModifier;
        public float baseGravity;
        public float resetGravityValue;
    }

    [SerializeField]
    public PhysicsSettings physics;


    bool active;
    Transform player;
    public ThirdPCamera cam;
    Transform camT;
    CharacterController controller;
    Rigidbody rigid;


    private float moveFB;
    private float moveLR;

    [Header("Movement Settings")]
    public float normalSpeed;
    public float sprintSpeed;
    public float moveSpeed;

    [Header("Jumping Settings")]
    public float jumpForce;
    public float doubleJumpForce;

    bool jumping = false;
    bool doubleAvailable = false;
    bool doubleJump = false;
    bool resetGravity;
    float gravity;
    Vector3 gravityVector;


    public Vector3 direction;


    void Start()
    {
        active = true;
        player = this.transform;
        controller = GetComponent<CharacterController>();
        camT = cam.GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
        //cam = GetComponentInChildren<ThirdPersonCamera>();
    }


    void Update()
    {
        if (active && cam)
        {
            ApplyGravity();
            SprintCheck();
            moveFB = Input.GetAxis("Vertical");
            moveLR = Input.GetAxis("Horizontal");
            direction = new Vector3(moveLR, 0, moveFB);
            direction = camT.TransformDirection(direction);
            Vector3 movement = new Vector3(moveSpeed * direction.x, gravityVector.y * Time.deltaTime, moveSpeed * direction.z);
            controller.Move(movement * Time.deltaTime);
            if (Input.GetButtonDown("Jump"))
            {

                Jump();
            }
        }


    }

    void SprintCheck()
    {
        if (Input.GetButton("Sprint"))
        {
            Debug.Log("Sprint");
            moveSpeed = sprintSpeed;
        }
        else
            moveSpeed = normalSpeed;
    }
    void Jump()
    {
        if (jumping)
        {
            return;
        }

        if (controller.isGrounded)
        {
            Debug.Log("Jump Check");

            jumping = true;

        }
        else if (doubleAvailable)
        {
            doubleJump = true;
            doubleAvailable = false;
        }

    }

    void ApplyGravity()
    {
        if (!controller.isGrounded)
        {
            if (!resetGravity)
            {
                gravity = physics.resetGravityValue;
                resetGravity = true;

            }
            gravity += physics.gravityModifier * Time.deltaTime;

        }
        else
        {
            gravity = physics.baseGravity;
            resetGravity = false;
        }
        //gravityVector = new Vector3();

        if (!jumping)
        {
            if (doubleJump)
            {
                gravityVector.y = doubleJumpForce;
                doubleJump = false;
            }
            else
                gravityVector.y -= physics.gravityModifier;

        }

        else
        {
            gravityVector.y = jumpForce;
            jumping = false;
            doubleAvailable = true;
            //jumping = false;
        }

    }
}
