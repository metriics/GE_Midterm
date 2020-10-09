﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12.0f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDist = 0.01f;
    public LayerMask groundMask;
    public float jumpHeight = 2.0f;
    public GameObject mazeManager;

    Vector3 velocity;
    bool isGrounded;


    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

        if (isGrounded && velocity.y < 0.0f)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void ResetVelocity()
    {
        velocity.x = 0.0f;
        velocity.y = -2.0f;
        velocity.z = 0.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        mazeManager.GetComponent<MazeLogic>().checkpointCallback(other.gameObject);
    }
}
