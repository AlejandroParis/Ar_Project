using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTargetBehaviour : MonoBehaviour
{

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SnapToTarget();
    }

    void SnapToTarget()
    {
        transform.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
    }
}
