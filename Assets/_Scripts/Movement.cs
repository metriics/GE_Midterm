using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float mouseX;
    float mouseY;
    public float mouseSensitivity = 500;
    public float speed = 15.0f;
    public GameObject mazeManager;

    bool cursorVisible = false;
    float xRotation = 0f;
    float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        hideCursor();
    }

    // Update is called once per frame
    void Update()
    {
        // show cursor when game paused
        if (Input.GetKeyDown(KeyCode.P))
        {
            cursorVisible = !cursorVisible;
            if (cursorVisible) // game paused
            {
                showCursor();
            }
            else // game unpaused
            {
                hideCursor();
            }
        }

        if (!cursorVisible) // game unpasued
        {
            // first person camera
            mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            yRotation += mouseX;
            xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0.0f);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(transform.forward.x * speed * Time.deltaTime, 0.0f, transform.forward.z * speed * Time.deltaTime);
        }
    }

    void hideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cursorVisible = false;
    }

    void showCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        cursorVisible = true;
    }

    void OnTriggerEnter(Collider other)
    {
        mazeManager.GetComponent<MazeLogic>().checkpointCallback(other.gameObject);
    }
}
