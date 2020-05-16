using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    public GameObject start, end;
    public GameObject[] spawn_areas;
    public GameObject free_portal;

    // Start is called before the first frame update
    void Start()
    {
        BallManager.Instance.SpawnBall(transform, start.transform);

        spawn_areas = GameObject.FindGameObjectsWithTag("SpawnArea");
        foreach (GameObject sa in spawn_areas)
        {
            sa.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnableSpawnAreas()
    {
        if (GameManager.Instance.game_state == GameManager.GameState.Deployment)
        {
            foreach (GameObject sa in spawn_areas)
            {
                if (!sa.activeSelf)
                    sa.SetActive(true);
            }

        }
    }

    public void OnDeployState()
    {
        EnableSpawnAreas();
    }
}
