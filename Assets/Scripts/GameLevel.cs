﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    public GameObject start, end;

    // Start is called before the first frame update
    void Start()
    {
        BallManager.Instance.SpawnBall(transform, start.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
