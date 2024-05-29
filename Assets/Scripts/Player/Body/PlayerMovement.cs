using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public CharacterController controller;
    public PlayerStats playerStats;
    public AudioSource audioSource;
    public float multiplier = 1f;
    public float gravity = -9.81f;
    public float speed;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    public Vector3 position;
    bool savePosition = true;
    bool isGrounded;
    bool isSprinting;

    public int Speed { get; internal set; }

    private void Start() {
        speed = playerStats.stats[StatNames.Speed];
        position = transform.position;
    }

    // Update is called once per frame
    void Update() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(savePosition) StartCoroutine(SavePosition());

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        if (transform.position.y < -10) {
            controller.enabled = false;
            controller.transform.position = position;
            controller.enabled = true;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Sprint")) isSprinting = true;
        if (Input.GetButtonUp("Sprint")) isSprinting = false;
        if (isSprinting && playerStats.UseStamina(0.1f)) multiplier = playerStats.stats[StatNames.SprintMultiplier];
        else multiplier = 1f;
        Vector3 move = transform.right * x + transform.forward * z;
        if (move != Vector3.zero) audioSource.Play();
        controller.Move(multiplier * speed * Time.deltaTime * move);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(playerStats.stats[StatNames.JumpHeight] * -2f * gravity);
        }

        if (Input.GetButtonDown("Dash") && playerStats.UseStamina(20)) {
            StartCoroutine(Dash());
        }

        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private IEnumerator SavePosition() {
        savePosition = false;
        if (transform.position.y >= 4.4) position = transform.position;
        yield return new WaitForSeconds(5);
        savePosition = true;
    }

    private IEnumerator Dash() {
        if(speed < playerStats.stats[StatNames.Speed] * 10) speed = playerStats.stats[StatNames.Speed] * 10;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(ReturnToNormal());
    }

    private IEnumerator ReturnToNormal() {
        while (speed > playerStats.stats[StatNames.Speed]) {
            yield return null;
            speed *= 0.8f;
        }
        speed = playerStats.stats[StatNames.Speed] ;
    }
}
