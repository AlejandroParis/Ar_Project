using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    GameObject current_platform = null;
    CharacterController controller;
    Vector3 movement_direction;
    Vector3 velocity;
    public float movement_speed = 10.0f;
    public float gravity = -9.8f;
    public float rotation_speed = -90.0f;
    public float jump_power = 500.0f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        movement_direction = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        if ((controller.collisionFlags & CollisionFlags.Sides) != 0) //Check if controller has collided from the side to swap direction.
            movement_direction = -movement_direction;

        //Keep y velocity
        float curr_y = velocity.y;
        velocity = movement_direction * movement_speed;
        velocity.y = curr_y;

        //Apply gravity
        if (!controller.isGrounded)
            velocity.y += gravity * Time.deltaTime;

        transform.Rotate(Vector3.forward, rotation_speed * movement_direction.x * Time.deltaTime);    
        controller.Move(velocity * Time.deltaTime);

        Debug.DrawLine(transform.position, transform.position + (movement_direction * 2), Color.green);
    }

    private void Jump(Vector3 N)
    {
       // ector3 jump_velocity = jump_power * N;
        velocity.y = jump_power * (N.y > 0 ? 1 : -1);

        if ((N.x < 0 && movement_direction.x > 0) || (N.x > 0 && movement_direction.x < 0))
            movement_direction = -movement_direction;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(current_platform != hit.gameObject)
        {
            current_platform = hit.gameObject;
            velocity.y = 0;

            Vector3 N = hit.transform.up;
            if (hit.gameObject.tag == "Trampoline")
                Jump(N);
        }

    }
}
