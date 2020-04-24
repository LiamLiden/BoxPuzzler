using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    public GameObject player;
    public float speed;
    public Vector3 cameraOffset;

    private Vector3 offset;

    private void Start()
    {
        Cursor.visible = false;
        offset = player.transform.position + cameraOffset;
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal Rotation
        Quaternion camAngleHorizontal = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * speed, Vector3.up);
        
        // Vertical Rotation
        Vector3 perpendicular = Vector3.Cross(player.transform.position - transform.position, Vector3.up).normalized;
        Quaternion camAngleVertical = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * speed, perpendicular);

        // Combine to create orbit
        offset = camAngleHorizontal * camAngleVertical * offset;
        transform.position = player.transform.position + offset

        transform.LookAt(player.transform);

    }
}
