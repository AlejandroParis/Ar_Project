using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PieceSpawner : MonoBehaviour
{
    public float spawn_y_offset;
    public GameObject piece_to_spawn;
    public GameObject map;
    public bool ready = false;
    Transform area_transform;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(ready && Input.GetMouseButtonDown(0))
        {

        }
    }

    public void PieceReady(Transform area_transform)
    {
        ready = true;
        this.area_transform = area_transform;
        GetComponent<MeshRenderer>().material.color = Color.green;
    }

    public void PieceUnReady()
    {
        ready = false;
        GetComponent<MeshRenderer>().material.color = Color.red;
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

        GameObject piece = GameObject.Instantiate(piece_to_spawn, spawn_position, Quaternion.identity, map.transform);

        PieceManager.Instance.OnPieceSpawn(piece);
    }
}
