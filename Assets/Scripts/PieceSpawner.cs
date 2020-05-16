using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PieceSpawner : MonoBehaviour
{
    public float spawn_y_offset;
    public GameObject piece_to_spawn;
    public bool ready = false;

    Transform area_transform;
    MeshRenderer m_renderer;

    // Start is called before the first frame update
    void Start()
    {
        m_renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ready && Input.GetMouseButtonDown(0))
        {
            SpawnPiece();
        }
    }

    public void PieceReady(Transform area_transform)
    {
        ready = true;
        this.area_transform = area_transform;
        m_renderer.material.color = Color.green;
    }

    public void PieceUnReady()
    {
        ready = false;
        m_renderer.material.color = Color.red;
    }

    private void OnMouseDown()
    {
       // SpawnPiece();
    }

    private void SpawnPiece()
    {
        /*Vector3 spawn_position = transform.position;
        spawn_position.y = spawn_position.y + spawn_y_offset;
        spawn_position.z = 0;*/

        //transform.SetPositionAndRotation(area_transform.position, area_transform.rotation);
        //transform.localScale = area_transform.localScale;

        GameObject piece = GameObject.Instantiate(piece_to_spawn, area_transform.position, area_transform.rotation, LevelManager.Instance.current_level.transform);
        piece.transform.localScale = area_transform.localScale;

        PieceManager.Instance.OnPieceSpawn(piece);
    }
}
