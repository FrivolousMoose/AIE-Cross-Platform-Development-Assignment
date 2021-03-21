using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class for controlling the camera movement
/// </summary>
public class CameraMovement : MonoBehaviour
{
    /// Speed of camera panning
    public float panSpeed = 100.0f;
    /// speed of zooming in/out with mouse controls
    public float MouseZoomSpeed = 15.0f;
    private Rigidbody rb;
    private Camera cam;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (Input.GetAxis("Mouse X") > 0)
            {
                rb.velocity -= new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * panSpeed * cam.orthographicSize,
                                           0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * panSpeed * cam.orthographicSize);
            }

            else if (Input.GetAxis("Mouse X") < 0)
            {
                rb.velocity -= new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * panSpeed * cam.orthographicSize,
                                           0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * panSpeed * cam.orthographicSize);
            }
        }

        if (Input.touchSupported)
        {
            // Pinch to zoom
            if (Input.touchCount == 2)
            {
                // get current touch positions
                Touch tZero = Input.GetTouch(0);
                Touch tOne = Input.GetTouch(1);
                // get touch position from the previous frame
                Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
                Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

                float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
                float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);

                // get offset value
                float deltaDistance = oldTouchDistance - currentTouchDistance;
                //Zoom(deltaDistance, TouchZoomSpeed);
            }
        }
        else
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            Zoom(scroll, MouseZoomSpeed);
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject newObject = hit.transform.gameObject;
            }
        }

        float newX = Mathf.Clamp(transform.position.x, cam.orthographicSize - 5, 105 - cam.orthographicSize);
        float newZ = Mathf.Clamp(transform.position.z, cam.orthographicSize - 15, 95 - cam.orthographicSize);
        transform.position = new Vector3(newX, transform.position.y, newZ);
    }

    void Zoom(float deltaMagnitudeDiff, float speed)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - deltaMagnitudeDiff * speed, 5, 55);
        // set min and max value of Clamp function upon your requirement
    }
}
