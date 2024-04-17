using UnityEngine;

public class Sword : MonoBehaviour
{
    // Fields
    public Animator playerAnimator;
    public string swingAnimationTrigger = "Swing";
    public float swingDuration = 0.5f;
    public int damageAmount = 10;
    public GameObject swordPrefab;
    private GameObject swordInstance;
    private bool isSwinging = false;
    private bool isSheathed = true;
    private float swingSpeed = 10f;

    private void Start()
    {
        // Loads the sword prefab
        swordInstance = Instantiate(swordPrefab, transform.position, Quaternion.identity);
        // Sets the player GameObject as the parent of the sword
        swordInstance.transform.parent = transform;

        // Gets the Animator component from the player GameObject if not assigned
        if (playerAnimator == null)
        {
            playerAnimator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSwinging)
        {
            SwingSword();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleSwordSheath();
        }
    }

    private void ToggleSwordSheath()
    {
        isSheathed = !isSheathed;
        swordInstance.SetActive(!isSheathed);
    }

    private void SwingSword()
    {
        isSwinging = true;
        playerAnimator.SetTrigger(swingAnimationTrigger);

        // Calculates swing direction based on mouse position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.z;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 direction = mouseWorldPosition - swordInstance.transform.position;

        // Check if the direction vector is not zero
        if (direction != Vector3.zero)
        {
            // Restricts movement to the XZ plane
            direction.y = 0f;
            swordInstance.transform.rotation = Quaternion.LookRotation(direction);

            // Moves the sword towards the mouse position
            swordInstance.transform.position = Vector3.MoveTowards(swordInstance.transform.position, mouseWorldPosition, swingSpeed * Time.deltaTime);
        }

        Invoke("ResetSwing", swingDuration);
    }

    private void ResetSwing()
    {
        isSwinging = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && isSwinging && !isSheathed)
        {
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.Damage(damageAmount);
            }
        }
    }
}
