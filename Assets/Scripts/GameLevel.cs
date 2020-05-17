using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    public GameObject start, end;
    public GameObject free_portal;

    [System.Serializable]
    public struct PieceAvailable
    {
       public Piece.PieceType type;
       public int number;
    }

    public PieceAvailable[] available_pieces;

    // Start is called before the first frame update
    void Start()
    {
        BallManager.Instance.SpawnBall(transform, start.transform);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int CheckPiecesAvailable(Piece.PieceType type)
    {
        foreach (PieceAvailable pa in available_pieces)
        {
            if(pa.type == type)
            {
                return pa.number;
            }
        }

        return 0;
    }

    public void DecreasePiece(Piece.PieceType type)
    {
        for(int i = 0; i < available_pieces.Length; ++i)
        {
            if (available_pieces[i].type == type)
                available_pieces[i].number--;
        }
    }

    public void IncreasePiece(Piece.PieceType type)
    {
        for (int i = 0; i < available_pieces.Length; ++i)
        {
            if (available_pieces[i].type == type)
                available_pieces[i].number++;
        }
    }
}
