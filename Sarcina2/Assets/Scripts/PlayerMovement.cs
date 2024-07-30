using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Joystick joystick;
    private Rigidbody rigidBody;

    private float inputX;
    private float inputY;
    public float moveSpeed;
    public Animator animator;
    public static bool isShooting = false;

    private void Awake()
    {
        joystick = GameObject.Find("Joystick").GetComponent<Joystick>();
        rigidBody = GetComponent<Rigidbody>();
        animator = transform.Find("PlayerCapsule").GetComponent<Animator>();
    }

    private void Update()
    {
        Movement();
        IdleWalkAnimationHandling();
    }

    private void IdleWalkAnimationHandling()
    {
        if (inputX == 0 && inputY == 0)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        PlayerRotation();
    }

    private void Movement()
    {
        inputX = joystick.Horizontal;
        inputY = joystick.Vertical;
    }

    private void ApplyMovement()
    {
        float moveX = inputX * moveSpeed * -1;
        float moveZ = inputY * moveSpeed;

        rigidBody.velocity = new Vector3(moveZ, 0, moveX);
    }

    public float speed = 5;
    public float rotationSpeed = 720;

    private void PlayerRotation()
    {
        if (!isShooting)
        {
            Vector3 movementDirection = new Vector3(inputY, 0, inputX * -1);
            movementDirection.Normalize();

            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}