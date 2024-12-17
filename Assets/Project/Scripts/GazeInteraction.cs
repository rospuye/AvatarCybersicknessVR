using UnityEngine;

public class GazeInteraction : MonoBehaviour
{
    public GameObject key; // The key GameObject
    public GameObject book; // The book GameObject
    public Canvas successCanvas; // Canvas for the success popup
    public Camera xrCamera; // Reference to the XR Rig's Main Camera
    public float rotationThreshold = 45f; // Rotation threshold for the book
    public float gazeDistance = 5f; // Maximum distance for gaze detection
    private bool bookRotated = false; // Tracks if the book has been rotated
    private bool successTriggered = false; // Ensures the success screen is shown only once

    void Start()
    {
        if (successCanvas != null)
            successCanvas.enabled = false; // Hide the success screen initially

        // Automatically find the XR Rig's Main Camera if not assigned
        if (xrCamera == null)
            xrCamera = Camera.main; // Assuming the Main Camera is correctly tagged
    }

    void Update()
    {
        // Step 1: Check if the book has been rotated
        if (!bookRotated && IsBookRotated())
        {
            bookRotated = true;
            Debug.Log("Book rotated enough to reveal the key!");
        }

        // Step 2: If the book is rotated, check if the user is looking at the key
        if (bookRotated && !successTriggered && IsLookingAtKey())
        {
            TriggerSuccess();
        }
    }

    private bool IsBookRotated()
    {
        // Check if the book's rotation exceeds the threshold on any axis
        Vector3 bookRotation = book.transform.eulerAngles;

        // Handle Unity's 0-360 rotation wrapping
        float xRotation = Mathf.Abs(NormalizeAngle(bookRotation.x));
        float zRotation = Mathf.Abs(NormalizeAngle(bookRotation.z));

        return xRotation > rotationThreshold || zRotation > rotationThreshold;
    }

    private bool IsLookingAtKey()
    {
        if (xrCamera == null)
        {
            Debug.LogWarning("XR Camera not set or found!");
            return false;
        }

        // Cast a ray from the XR Camera to detect if the user is looking at the key
        Ray ray = new Ray(xrCamera.transform.position, xrCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, gazeDistance))
        {
            if (hit.collider.gameObject == key)
            {
                Debug.Log("Looking at the key!");
                return true;
            }
        }

        return false;
    }

    private void TriggerSuccess()
    {
        successTriggered = true;

        if (successCanvas != null)
            successCanvas.enabled = true; // Show the success screen

        Debug.Log("Success! You found the key.");
    }

    // Normalize angle to range [-180, 180] for easier comparison
    private float NormalizeAngle(float angle)
    {
        return (angle > 180) ? angle - 360 : angle;
    }
}
