using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class FishingPoleEffect : MonoBehaviour
{
    public Transform attachPoint; // The default attach point for the controller
    public float weightMultiplier = 0.1f; // Multiplier to control the effect based on mass

    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    void Awake()
    {
        // Get references to the XRGrabInteractable and Rigidbody components
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        if (attachPoint == null)
        {
            // Create a new attach point if not assigned
            GameObject attachPointObj = new GameObject("AttachPoint");
            attachPointObj.transform.SetParent(transform);
            attachPoint = attachPointObj.transform;
        }

        // Set the default attach transform
        grabInteractable.attachTransform = attachPoint;

        // Subscribe to the select event to adjust the attach point dynamically
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        //grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    void OnDestroy()
    {
        // Unsubscribe from events to prevent memory leaks
        grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        //grabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Adjust the attach point based on the object's mass
        if (rb != null)
        {
            float offset = rb.mass * weightMultiplier;
            attachPoint.localPosition = new Vector3(0, -offset, 0);
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        // Reset the attach point when the object is released
        attachPoint.localPosition = Vector3.zero;
    }
}
