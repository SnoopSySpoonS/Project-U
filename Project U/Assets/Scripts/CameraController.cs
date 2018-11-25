using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Use this for initialization
    void Start () {
        
    }

    float moveSpeed = 3.5f;
    
    // Update is called once per frame
    void Update () {
        KeyboardMovement();
        MouseRotation();
        MouseZoom();
    }
    
    void KeyboardMovement()
    {
        
        Vector3 translate = new Vector3
            (
                Input.GetAxis("Horizontal"),
                0,
                Input.GetAxis("Vertical")
            );

        this.transform.Translate( translate * moveSpeed * Time.deltaTime * (1 + this.transform.position.y / 2), Space.World);
    }
    
    private void MouseRotation()
    {
        // Middle Mouse button
        if(Input.GetMouseButton(2) == false)
        {
            return;
        }
        float h = moveSpeed * Input.GetAxis("Mouse X");
        float v = moveSpeed * Input.GetAxis("Mouse Y");
        transform.Rotate(v, h, 0);
    } 
    private void MouseZoom()
    {
        var delta = Input.GetAxis("Mouse ScrollWheel");        
        if (delta > -0.001f && delta < 0.001f)
        {
            return;
        }
        Vector3 translate = new Vector3(0, 10 * delta, 0);
        Debug.Log("Translation: " + translate);
        transform.Translate( translate * moveSpeed * Time.deltaTime, Space.World);
    }
}
