using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    public float mouseSensitivety = 100;
    public Transform playerBody;
    float Xrotation = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivety * Time.deltaTime;
        transform.Rotate(0, mouseX, 0);
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivety * Time.deltaTime;

        playerBody.Rotate(Vector3.up * mouseX);
        Xrotation -= mouseY;
        Xrotation = Mathf.Clamp(Xrotation, -80, 80);
        transform.localRotation = Quaternion.Euler(Xrotation, 0, 0);
    }
}
