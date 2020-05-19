using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public int selected_level = 0;

    public GameObject map_target;
    public GameLevel current_level = null;
    public List<GameObject> level_prefabs;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            LoadLevel(0);
        if (Input.GetKeyDown(KeyCode.F2))
            LoadLevel(1);
    }
    private void Start()
    {
        map_target.SetActive(false);
    }
    public void OnReachEnd()
    {
        BallManager.Instance.LockBall();
    }

    public void OnBallFall()
    {
        BallManager.Instance.ResetBall(current_level.start.transform);
    }

    public void OnTargetFound()
    {
        if(!current_level)
        {
            LoadLevel(selected_level);
        }
        else
        {
            current_level.gameObject.SetActive(true);
        }
    }

    private void SpawnNewLevel(int index)
    {
        if(level_prefabs.Count >= index)
        {
            selected_level = index;
            GameObject map = Instantiate(level_prefabs[selected_level]);
            current_level = map.GetComponent<GameLevel>();
            map.GetComponent<ObjectTargetBehaviour>().target = map_target;

            GameManager.Instance.StartDeploymentPhase();
        }       
    }

    public void OnTargetLost()
    {
        if (current_level)
        {
            //current_level.gameObject.SetActive(false);
            //BallManager.Instance.LockBall();
        }
    }

    public void LoadLevel(int index)
    {
        if (current_level)
        {
            Destroy(current_level.gameObject);
            current_level = null;
            BallManager.Instance.DestroyBall();
        }         

        SpawnNewLevel(index);
    }
}
