using UnityEngine;

public class StayInDeskArea : MonoBehaviour
{
    public GameObject xrRig;          // Reference to the XR Rig
    public Collider boundaryCollider; // The collider representing the boundary (invisible cube)

    void Update()
    {
        if (xrRig != null)
        {
            // Get the position of the XR Rig
            Vector3 rigPosition = xrRig.transform.position;

            // Check if the XR Rig is outside the boundary
            if (!boundaryCollider.bounds.Contains(rigPosition))
            {
                // Find the closest point inside the boundary
                Vector3 closestPoint = boundaryCollider.ClosestPoint(rigPosition);
                // Set the XR Rig's position to this closest point
                xrRig.transform.position = closestPoint;
            }
        }
    }
}
