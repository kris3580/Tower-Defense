using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Joystick joystick;
    private Rigidbody rigidBody;

    private float inputX;
    private float inputY;
    public float moveSpeed;

    private void Awake()
    {
        joystick = GameObject.Find("Joystick").GetComponent<Joystick>();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Movement();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void Movement()
    {
        inputX = joystick.Horizontal;
        inputY = joystick.Vertical;
    }

    private void ApplyMovement()
    {
        float moveX = inputY * moveSpeed;
        float moveZ = inputX * moveSpeed * -1;

        rigidBody.AddForce(new Vector3(moveX, 0, moveZ));
    }



}
