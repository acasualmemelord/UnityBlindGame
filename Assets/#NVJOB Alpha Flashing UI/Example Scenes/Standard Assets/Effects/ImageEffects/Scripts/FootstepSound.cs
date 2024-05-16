using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioClip footstepClip;
    public float footstepDelay = 0.5f;
    public float movementThreshold = 0.1f;

    private AudioSource audioSource;
    private float nextFootstepTime;
    private Vector3 previousPosition;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        previousPosition = transform.position;
    }

    private void PlayFootstepSound()
    {
        if (footstepClip != null)
        {
            audioSource.PlayOneShot(footstepClip);
            nextFootstepTime = Time.time + footstepDelay;
        }
    }

    private void Update()
    {
        if (IsMoving() && IsGrounded() && Time.time >= nextFootstepTime)
        {
            PlayFootstepSound();
        }

        previousPosition = transform.position;
    }

    private bool IsMoving()
    {
        // Check if the player is moving based on position change
        Vector3 currentPosition = transform.position;
        float distance = Vector3.Distance(currentPosition, previousPosition);
        return distance > movementThreshold;
    }

    private bool IsGrounded()
    {
        // Check if the player is on the ground using a raycast
        float raycastDistance = 0.1f;
        LayerMask groundLayer = LayerMask.GetMask("Ground"); // Replace "Ground" with your ground layer name
        return Physics.Raycast(transform.position, Vector3.down, raycastDistance, groundLayer);
    }
}