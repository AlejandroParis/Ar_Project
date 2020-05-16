using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : Singleton<PieceManager>
{
    public List<GameObject> spawned_pieces;


    // Start is called before the first frame update
    void Start()
    {
        spawned_pieces = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void OnPieceSpawn(GameObject piece)
    {
        spawned_pieces.Add(piece);
    }
}
