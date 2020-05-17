using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Vuforia;

public class PieceSpawner : MonoBehaviour
{
    public float spawn_y_offset;
    List<GameObject> pieces_to_spawn;

    Piece.PieceType current_spawn_type = Piece.PieceType.Block;


    /*Transform area_transform;
    MeshRenderer m_renderer;*/

    // Start is called before the first frame update
    void Start()
    {
        pieces_to_spawn = new List<GameObject>();
        foreach (Transform child in transform)
        {
            pieces_to_spawn.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
        pieces_to_spawn[(int)current_spawn_type].SetActive(true);
        //m_renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if there is a touch
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            // Check if finger is over a UI element /*!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) ||*/
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                SpawnPiece();
            }
        }
        /*if ()
        {
            SpawnPiece();
        }*/
    }

    internal void SetSpawnerType(Piece.PieceType type)
    {
        pieces_to_spawn[(int)current_spawn_type].SetActive(false);
        current_spawn_type = type;
        pieces_to_spawn[(int)current_spawn_type].SetActive(true);
    }

    /*public void PieceReady(Transform area_transform)
    {
        //ready = true;
        this.area_transform = area_transform;
        m_renderer.material.color = Color.green;
    }

    public void PieceUnReady()
    {
        //ready = false;
        m_renderer.material.color = Color.red;
    }*/

    private void OnMouseDown()
    {
       // SpawnPiece();
    }

    private void SpawnPiece()
    {
        Vector3 spawn_position = transform.position;
        spawn_position.z = 0;

        //transform.SetPositionAndRotation(area_transform.position, area_transform.rotation);
        //transform.localScale = area_transform.localScale;

        GameObject piece = GameObject.Instantiate(pieces_to_spawn[(int)current_spawn_type], spawn_position, transform.rotation, LevelManager.Instance.current_level.transform);
        piece.transform.localScale = transform.localScale;

        PieceManager.Instance.OnPieceSpawn(piece);
    }
}
