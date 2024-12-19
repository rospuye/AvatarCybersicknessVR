using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class RandomObjectPlacer : MonoBehaviour
{
    [Header("Volume Bounds")]
    public float xStart = -10f;
    public float xEnd = 10f;
    public float yStart = 0f;
    public float yEnd = 10f;
    public float zStart = -10f;
    public float zEnd = 10f;

    [Header("Object Settings")]
    public int objectCount = 100; // Number of objects to spawn
    public Vector2 sizeRange = new Vector2(0.5f, 2f); // Min and max scale

    public Material cubeMaterial;
    public Material sphereMaterial;

    void Start()
    {
        PlaceRandomObjects();
    }

    void PlaceRandomObjects()
    {
        for (int i = 0; i < objectCount; i++)
        {
            // Randomly decide whether to create a cube or a sphere
            GameObject obj;
            if (Random.value > 0.5f)
            {
                obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                if (cubeMaterial != null)
                {
                    obj.GetComponent<Renderer>().material = cubeMaterial;
                }
            }
            else
            {
                obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                if (sphereMaterial != null)
                {
                    obj.GetComponent<Renderer>().material = sphereMaterial;
                }
            }

            // Add Rigidbody, Collider, and XR Grab Interactable
            if (obj.TryGetComponent(out Collider collider))
            {
                collider.enabled = true; // Ensure the collider is enabled
            }
            else
            {
                obj.AddComponent<BoxCollider>(); // Add default BoxCollider if none exists
            }

            Rigidbody rb = obj.AddComponent<Rigidbody>();
            rb.useGravity = true;

            XRGrabInteractable grabInteractable = obj.AddComponent<XRGrabInteractable>();
            grabInteractable.movementType = XRBaseInteractable.MovementType.Kinematic;

            // Set random position within the volume
            float xPos = Random.Range(xStart, xEnd);
            float yPos = Random.Range(yStart, yEnd);
            float zPos = Random.Range(zStart, zEnd);
            obj.transform.position = new Vector3(xPos, yPos, zPos);

            // Set random scale within the specified range
            float randomScale = Random.Range(sizeRange.x, sizeRange.y);
            obj.transform.localScale = Vector3.one * randomScale;

            // Parent to this GameObject for better organization
            obj.transform.parent = this.transform;
        }
    }
}
