using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PieceSpawner : MonoBehaviour
{
    public float spawn_y_offset;
    public GameObject piece_to_spawn;
    GameObject map;

    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("Map");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        SpawnPiece();
    }

    private void SpawnPiece()
    {
        Vector3 spawn_position = transform.position;
        spawn_position.y = spawn_position.y + spawn_y_offset;
        spawn_position.z = 0;

        GameObject.Instantiate(piece_to_spawn, spawn_position, Quaternion.identity, map.transform);
    }
}
