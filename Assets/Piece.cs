using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Piece : MonoBehaviour
{
    public float max_y, min_y;
    ImageTargetBehaviour target;
    GameObject map;
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("Map");
        target = transform.GetComponentInParent<ImageTargetBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        this.gameObject.transform.SetParent(map.transform);
        Debug.Log(target.TrackableName);
    }
}
