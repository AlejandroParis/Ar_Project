using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : Singleton<BallManager>
{
    public GameObject selected_ball;
    public BallMovement ball;

    [Header("Clips")]
    public AudioClip portal_sound;
    public AudioClip trampoline_sound;
    public AudioClip speed_sound;

    internal void SpawnBall(Transform map, Transform start)
    {
        if (ball == null)
        {
            GameObject ball_go = Instantiate(selected_ball, start.position, Quaternion.identity, map);
            ball = ball_go.GetComponent<BallMovement>();
            ball.enabled = false;
        }
        else
            Debug.Log("Error: A Ball was tried to be spawned while another one exists.");
    }

    internal void UnlockBall()
    {
        if(ball)
            ball.enabled = true;
        else
            Debug.Log("Error: Trying to unlock non existing ball.");
    }

    internal void ResetBall(Transform transform)
    {
        if(ball)
        {
            ball.enabled = false;
            ball.transform.position = transform.position;
            ball.Reset();
        }
        else
            Debug.Log("Error: Trying to reset non existing ball.");
    }

    internal void LockBall()
    {
        if(ball)
            ball.enabled = false;
        else
            Debug.Log("Error: Trying to lock non existing ball.");
    }

    internal void DestroyBall()
    {
        if (ball)
        {
            Destroy(ball.gameObject);
            ball = null;
        }
            
    }
}
