using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        Deployment,
        Running,
        Paused,
        None
    }

    public GameState game_state = GameState.None;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel()
    {
        game_state = GameState.Deployment;
    }

    public void RunLevel()
    {
        game_state = GameState.Running;

        PieceManager.Instance.OnRun();
        UIManager.Instance.OnRun();
        BallManager.Instance.UnlockBall();
    }

    public void PauseGame()
    {
        game_state = GameState.Paused;

        BallManager.Instance.LockBall();
    }

    internal void StartDeploymentPhase()
    {
        game_state = GameState.Deployment;

        PieceManager.Instance.OnDeployment();
        UIManager.Instance.OnDeployment();
    }
}
