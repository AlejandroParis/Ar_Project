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
        if(LevelManager.Instance.current_level.gameObject == this.gameObject)
            transform.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        else
        {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y, LevelManager.Instance.current_level.transform.position.z);
            transform.rotation = Quaternion.Euler(0, 0, -target.transform.rotation.eulerAngles.z);
        }          

    }
}
