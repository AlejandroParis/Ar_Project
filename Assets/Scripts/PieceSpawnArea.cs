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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Collision stay");
        if (other.tag == "TargetPiece")
        {
            Debug.Log("Entered Collision stay");
            PieceSpawner ps = other.gameObject.GetComponent<PieceSpawner>();
            if (GetComponent<Collider>().bounds.Contains(other.bounds.min) && GetComponent<Collider>().bounds.Contains(other.bounds.max) && !ps.ready)
            {
                Debug.Log("Ready to tap");
                ps.PieceReady(transform);
            }
            else if(ps.ready)
            {
                ps.PieceUnReady();
            }
        }
    }
}
