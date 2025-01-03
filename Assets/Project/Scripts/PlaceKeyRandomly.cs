using UnityEngine;

public class PlaceKeyRandomly : MonoBehaviour
{
    [Header("Position Boundaries")]
    public float x_start = -10f;
    public float x_end = 10f;
    public float z_start = -10f;
    public float z_end = 10f;

    [Header("Target Object")]
    public GameObject targetObject;

    // Bounds for respawning
    private const float minBound = -100f;
    private const float maxBound = 100f;

    // Start is called before the first frame update
    void Start()
    {
        if (targetObject != null)
        {
            SetRandomPosition();
        }
        else
        {
            Debug.LogError("Target object is not assigned in the inspector.");
        }
    }

    void Update()
    {
        if (targetObject != null)
        {
            CheckBoundsAndRespawn();
        }
    }

    void SetRandomPosition()
    {
        float randomX = Random.Range(x_start, x_end);
        float randomZ = Random.Range(z_start, z_end);

        Vector3 newPosition = new Vector3(randomX, targetObject.transform.position.y, randomZ);
        targetObject.transform.position = newPosition;
    }

    void CheckBoundsAndRespawn()
    {
        Vector3 position = targetObject.transform.position;

        if (position.x < minBound || position.x > maxBound ||
            position.y < minBound || position.y > maxBound ||
            position.z < minBound || position.z > maxBound)
        {
            Debug.Log("Target object out of bounds. Respawning at a random position.");
            SetRandomPosition();
        }
    }
}
