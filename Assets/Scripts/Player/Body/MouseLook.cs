using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
    public float mouseSensitivity = 250f;
    public Transform playerBody;
    public Transform playerHead;
    float xRotation = 0f;
    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -105f, 12.5f);

        playerBody.Rotate(Vector3.up * mouseX); //look left and right
        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //look up and down
        playerHead.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
