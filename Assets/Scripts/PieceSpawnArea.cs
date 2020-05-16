using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawnArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "TargetPiece")
        {
            PieceSpawner ps = other.gameObject.GetComponent<PieceSpawner>();
            if (!ps.ready && GetComponent<Collider>().bounds.Contains(other.bounds.min) && GetComponent<Collider>().bounds.Contains(other.bounds.max))
            {
                Debug.Log("Ready to tap");
                ps.PieceReady(transform);
            }
            else if (ps.ready && !GetComponent<Collider>().bounds.Contains(other.bounds.min) && !GetComponent<Collider>().bounds.Contains(other.bounds.max))
            {
                ps.PieceUnReady();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TargetPiece")
        {
            PieceSpawner ps = other.gameObject.GetComponent<PieceSpawner>();
            ps.PieceUnReady();
        }
    }
}
