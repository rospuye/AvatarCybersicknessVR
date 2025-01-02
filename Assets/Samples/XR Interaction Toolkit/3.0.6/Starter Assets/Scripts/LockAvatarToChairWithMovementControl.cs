using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class LockAvatarToChairWithMovementControl : MonoBehaviour
{
    public Transform chair;  // Reference to the chair's Transform
    public DynamicMoveProvider moveProvider;  // Reference to the DynamicMoveProvider
    public TeleportationProvider teleportationProvider;  // Reference to the TeleportationProvider

    public bool isSeated = true;  // Toggle for seated state

    void Start()
    {
        if (moveProvider == null)
        {
            moveProvider = GetComponent<DynamicMoveProvider>();
        }

        if (teleportationProvider == null)
        {
            teleportationProvider = GetComponent<TeleportationProvider>();
        }
    }

    void LateUpdate()
    {
        // Lock avatar to the chair's position and rotation, ensuring seated posture
        if (chair != null)
        {
            transform.position = chair.position; // Keep avatar position locked to chair's position
            transform.rotation = chair.rotation;  // Lock the rotation as well
        }

        // Disable movement and teleportation when seated
        if (moveProvider != null)
        {
            moveProvider.enabled = !isSeated;  // Disable movement if seated
        }

        if (teleportationProvider != null)
        {
            teleportationProvider.enabled = !isSeated;  // Disable teleportation if seated
        }
    }
}
