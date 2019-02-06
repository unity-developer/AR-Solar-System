using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanet : MonoBehaviour
{
    public float speed = 1.0f;

    private bool isDrag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDrag)
            transform.Rotate(Vector3.up, Time.deltaTime * speed);

    }
    private void OnMouseDrag()
    {
        isDrag = true;
        float rotX = Input.GetAxis("Mouse X") * speed *10* Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * speed *10* Mathf.Deg2Rad;

        transform.Rotate(Vector3.up, -rotX);
       // transform.Rotate(Vector3.right, rotY);
    }
    private void OnMouseUp()
    {
        isDrag = false;
    }
}
