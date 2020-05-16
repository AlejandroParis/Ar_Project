using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public int selected_level = 1;

    public GameObject map_target;
    GameLevel current_level = null;
    public List<GameObject> level_prefabs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnReachEnd()
    {

    }

    public void OnBallFall()
    {
        BallManager.Instance.ResetBall(current_level.start.transform);
    }

    public void OnTargetFound()
    {
        if(current_level == null && level_prefabs.Count >= selected_level)
        {
            GameObject map = Instantiate(level_prefabs[selected_level - 1]);
            current_level = map.GetComponent<GameLevel>();
            map.GetComponent<ObjectTargetBehaviour>().target = map_target;
        }
        else if(current_level)
        {
            current_level.gameObject.SetActive(true);
            if(GameManager.Instance.game_state == GameManager.GameState.Running)
            {
                BallManager.Instance.UnlockBall();
            }
        }
    }
    public void OnTargetLost()
    {
        if (current_level)
        {
            current_level.gameObject.SetActive(false);
            BallManager.Instance.LockBall();
        }
    }


}
