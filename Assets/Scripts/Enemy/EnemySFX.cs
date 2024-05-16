using UnityEngine;

public class EnemySFX : MonoBehaviour
{
    public AudioClip sfx;
    public float zombieDistance = 3f;
    public float sfxDelay = 2f;

    private AudioSource audioSource;
    private Transform playerTransform;
    private float lastPlayed;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update()
    {
        // Calculates distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= zombieDistance && Time.time >= lastPlayed + sfxDelay)
        {
            PlayZombieSound();
            lastPlayed = Time.time;
        }
    }

    private void PlayZombieSound()
    {
        if (sfx != null)
        {
            audioSource.PlayOneShot(sfx);
        }
    }
}