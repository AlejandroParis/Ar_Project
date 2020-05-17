using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : Singleton<PieceManager>
{
    public List<GameObject> spawned_pieces;
    public PieceSpawner piece_spawner;

    // Start is called before the first frame update
    void Start()
    {
        spawned_pieces = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTargetFound()
    {
        if(GameManager.Instance.game_state == GameManager.GameState.Deployment)
            piece_spawner.gameObject.SetActive(true);
    }
    public void OnTargetLost()
    {
        piece_spawner.gameObject.SetActive(false);
    }

    internal void OnPieceSpawn(Piece piece)
    {
        if(piece.type == Piece.PieceType.Portal && LevelManager.Instance.current_level.free_portal)
        {
            ((PortalPiece)piece).connected_portal = LevelManager.Instance.current_level.free_portal.transform.GetChild(0).gameObject;
            LevelManager.Instance.current_level.free_portal.GetComponent<PortalPiece>().connected_portal = piece.transform.GetChild(0).gameObject;
            LevelManager.Instance.current_level.free_portal = null;
        }
        spawned_pieces.Add(piece.gameObject);
    }

    internal void OnRun()
    {
        piece_spawner.gameObject.SetActive(false);
    }

    internal void OnDeployment()
    {
       // piece_spawner.gameObject.SetActive(true);
    }
}
