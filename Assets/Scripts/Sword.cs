using UnityEngine;

public class Sword : MonoBehaviour
{ 
    void Start()
    {
        // Initializes the sword object
        InitializeSword();

        // Set initial position to zero
        
        // Sets the initial rotation to no rotation
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    void Update()
    {
        // Updates the position of the sword object
        HandleInput();
    }

    void HandleInput()
    {
        // Rotates the sword based on player input
        float rotationSpeed = 100f;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculates the rotation angles based on player input
        float rotationX = verticalInput * rotationSpeed * Time.deltaTime;
        float rotationY = horizontalInput * rotationSpeed * Time.deltaTime;

        // Applies rotation
        transform.Rotate(Vector3.right, rotationX);
        transform.Rotate(Vector3.up, rotationY);
    }

    void InitializeSword()
    {
        // Loads the sword prefab
        GameObject swordPrefab = Resources.Load<GameObject>("SwordPrefab");

        // Checks if the prefab was successfully loaded
        if (swordPrefab != null)
        {
            // Instantiates the sword prefab
            GameObject swordModel = Instantiate(swordPrefab, transform.position, Quaternion.identity);

            // Sets the instantiated sword as a child of the sword object
            swordModel.transform.parent = transform;

            // Sets the local position and rotation of the sword model to zero
            swordModel.transform.localPosition = Vector3.zero;
            swordModel.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.LogError("Cannot load sword prefab.");
        }
    }
}