using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public CharacterController controller;
    public PlayerStats playerStats;
    public AudioSource audioSource;
    public float multiplier = 1f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool isSprinting;

    public int speed { get; internal set; }

    // Update is called once per frame
    void Update() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Sprint")) isSprinting = true;
        if (Input.GetButtonUp("Sprint")) isSprinting = false;
        if (isSprinting && playerStats.UseStamina(0.1f)) multiplier = playerStats.stats[StatNames.SprintMultiplier];
        else multiplier = 1f;
        Vector3 move = transform.right * x + transform.forward * z;
        if (move != Vector3.zero) audioSource.Play();
        controller.Move(multiplier * playerStats.stats[StatNames.Speed] * Time.deltaTime * move);

        if(Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(playerStats.stats[StatNames.JumpHeight] * -2f * gravity);
        }

        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
