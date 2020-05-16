using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public enum PieceType
    {
        Block,
        Trampoline,
        SpeedBoost,
        Portal
    }

    public PieceType type = PieceType.Block;
    public float min_y, max_y;
    bool dragging = false;
    Vector3 last_mouse_pos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dragging)
        {
            Vector3 curren_mouse_pos = Input.mousePosition;
            if(curren_mouse_pos.y != last_mouse_pos.y)
            {
                Vector3 tmp_position = transform.position;
                float new_y = tmp_position.y + ((curren_mouse_pos.y - last_mouse_pos.y) * Time.deltaTime * 2);
                tmp_position.y = Mathf.Clamp(new_y, min_y, max_y); 
                transform.position = tmp_position;
            }
            last_mouse_pos = curren_mouse_pos;
        }
    }
    private void OnMouseDown()
    {
        dragging = true;
        last_mouse_pos = Input.mousePosition;
    }
    private void OnMouseUp()
    {
        dragging = false;
    }
}
