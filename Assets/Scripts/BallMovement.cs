using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public GameObject current_platform = null;
    public CharacterController controller;
   
    public Vector3 movement_direction;
    Vector3 velocity;
    bool air = false;

    public float movement_speed = 10.0f;
    public float acceleration = 5.0f;
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
        {
              movement_direction = -movement_direction;
              velocity = -velocity;
        }

        if (!controller.isGrounded && !air) //fuck this
        {
            velocity.y = movement_direction.y * (sprinting ? sprint_speed : movement_speed);
            air = true;
        }

        //Keep y velocity
        Vector3 new_v = movement_direction * acceleration;
        velocity += new_v * Time.deltaTime;
        velocity.x = Mathf.Clamp(velocity.x, sprinting ? -sprint_speed : -movement_speed, sprinting ? sprint_speed : movement_speed);

        //Apply gravity
        velocity.y += gravity * Time.deltaTime;

        if (controller.isGrounded && !air)
            velocity.y = 0;

        transform.Rotate(Vector3.forward, rotation_speed * velocity.magnitude * movement_direction.x * Time.deltaTime);    
        controller.Move(velocity * Time.deltaTime);

        Debug.DrawLine(transform.position, transform.position + movement_direction * 2);
    }

    internal void Reset()
    {
        transform.rotation = Quaternion.identity;
        movement_direction = transform.right;
        sprinting = false;
        velocity = Vector3.zero;
    }

    private void Jump(Vector3 N)
    {
        GameManager.Instance.audio.PlayOneShot(BallManager.Instance.trampoline_sound);

        air = true;
        if ((N.x < 0 && movement_direction.x > 0) || (N.x > 0 && movement_direction.x < 0))
        {
            velocity = -velocity;
        }

        movement_direction = N;
        velocity.y = jump_power * (N.y > 0 ? 1 : -1);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Piece" && current_platform != hit.gameObject)
        {
            Piece piece = hit.gameObject.GetComponent<Piece>();

            if (piece.type != Piece.PieceType.Portal && piece.type != Piece.PieceType.Trampoline)
                SetPlatform(piece.gameObject);

            if (piece.type == Piece.PieceType.Trampoline)
            {
                Vector3 N = hit.transform.up;
                Jump(N);
            }

            if (piece.type == Piece.PieceType.SpeedBoost)
            {
                if(!sprinting)
                    GameManager.Instance.audio.PlayOneShot(BallManager.Instance.speed_sound);
                velocity.x = movement_direction.x * sprint_speed;
                sprinting = true;
                sprint_timer = 0.0f;
            }
        }

    }

    public void SetPlatform(GameObject gameObject)
    {
        Debug.Log(gameObject.name);
        velocity.y = 0;
        air = false;
        Vector3 N = gameObject.transform.up;
        N = Quaternion.Euler(0, 0, -90) * N;

        float angle = Vector3.Angle(new Vector3(Mathf.Abs(movement_direction.x), Mathf.Abs(movement_direction.y), Mathf.Abs(movement_direction.z)), N);

        Debug.Log(angle + "from  " + movement_direction + N);
        if (angle > 80)
        {
            return;
        }
                   
        if (movement_direction.x < 0)
            N = -N;

        movement_direction = N;
        current_platform = gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DeathTrigger")
        {
            LevelManager.Instance.OnBallFall();
        }
    }
}
