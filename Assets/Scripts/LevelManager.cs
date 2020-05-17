using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public int selected_level = 1;

    public GameObject map_target;
    public GameLevel current_level = null;
    public List<GameObject> level_prefabs;

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
            SpawnNewLevel(selected_level);
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
            GameObject map = Instantiate(level_prefabs[selected_level - 1]);
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


}
