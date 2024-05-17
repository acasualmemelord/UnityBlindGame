using UnityEngine;

public class EnemySFX : MonoBehaviour {
    public AudioClip sfx;
    public AudioClip attack;
    public AudioClip idle;
    public AudioClip walking;
    public AudioClip death;
    public float zombieDistance = 3f;
    public float sfxDelay = 2f;

    public AudioSource audioSource;
    private Transform playerTransform;
    private float lastPlayed;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Start() {
        sfxDelay = Random.value * 3 + 2;
    }

    private void Update() {
        // Calculates distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= zombieDistance && Time.time >= lastPlayed + sfxDelay) {
            PlayZombieSound();
            lastPlayed = Time.time;
        }
    }

    private void PlayZombieSound() {
        if (sfx != null) {
            audioSource.PlayOneShot(sfx);
        }
    }

    private void AttackSFX() {
        if (attack != null) {
            audioSource.PlayOneShot(attack);
        }
    }
    private void IdleSFX() {
        if (idle != null) {
            audioSource.PlayOneShot(idle);
        }
    }
    private void DeathSFX() {
        if (death != null) {
            audioSource.PlayOneShot(death);
        }
    }
    private void WalkingSFX() {
        if (walking != null) {
            audioSource.PlayOneShot(walking);
        }
    }
}