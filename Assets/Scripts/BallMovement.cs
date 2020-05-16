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
    public float sprint_speed = 14.0f;
    public float gravity = -9.8f;
    public float rotation_speed = 30.0f;
    public float jump_power = 500.0f;
    public float sprint_duration = 5.0f;

    bool sprinting = false;
    float sprint_timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        movement_direction = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        //Sprint timer, move to Sprinting State if it's ever done.
        if(sprinting && sprint_timer <= sprint_duration)
        {
            sprint_timer += Time.deltaTime;
            if (sprint_timer >= sprint_duration)
                sprinting = false;

        }

        if ((controller.collisionFlags & CollisionFlags.Sides) != 0) //Check if controller has collided from the side to swap direction.
            movement_direction = -movement_direction;

        //Keep y velocity
        float curr_y = velocity.y;
        velocity = movement_direction * (sprinting? sprint_speed:movement_speed);
        velocity.y = curr_y;

        //Apply gravity
        if (!controller.isGrounded)
            velocity.y += gravity * Time.deltaTime;

        transform.Rotate(Vector3.forward, rotation_speed * velocity.magnitude * movement_direction.x * Time.deltaTime);    
        controller.Move(velocity * Time.deltaTime);
    }

    private void Jump(Vector3 N)
    {
        velocity.y = jump_power * (N.y > 0 ? 1 : -1);

        if ((N.x < 0 && movement_direction.x > 0) || (N.x > 0 && movement_direction.x < 0))
            movement_direction = -movement_direction;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Piece")
        {
            Piece piece = hit.gameObject.GetComponent<Piece>();

            if (current_platform != piece.gameObject && piece.type == Piece.PieceType.Trampoline)
            {
                Vector3 N = hit.transform.up;
                Jump(N);
            }

            if (piece.type == Piece.PieceType.SpeedBoost)
            {
                sprinting = true;
                sprint_timer = 0.0f;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DeathTrigger")
        {
            LevelManager.Instance.OnBallFall();
        }
    }
}
