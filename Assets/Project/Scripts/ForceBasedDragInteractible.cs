using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ForceBasedDragInteractable : XRGrabInteractable
{
    private Rigidbody rb;
    private Vector3 lastControllerPosition;
    private Vector3 controllerVelocity;
    private XRBaseInteractor interactor;

    // Drag factor to tweak how heavy objects feel (larger value means more sluggish)
    public float dragFactor = 10.0f;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // Store the interactor and Rigidbody reference
        interactor = args.interactorObject as XRBaseInteractor;
        rb = GetComponent<Rigidbody>();

        if (interactor != null)
        {
            lastControllerPosition = interactor.transform.position;
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // Clear references
        interactor = null;
        rb = null;
    }

    private void FixedUpdate()
    {
        // If the object is grabbed, apply force-based drag
        if (rb != null && interactor != null)
        {
            // Calculate the velocity of the controller
            Vector3 currentControllerPosition = interactor.transform.position;
            controllerVelocity = (currentControllerPosition - lastControllerPosition) / Time.fixedDeltaTime;
            lastControllerPosition = currentControllerPosition;

            // Calculate weighted velocity based on mass
            Vector3 weightedVelocity = controllerVelocity / (rb.mass * dragFactor);

            // Interpolate Rigidbody velocity towards weighted velocity
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, weightedVelocity, Time.fixedDeltaTime * 10);
        }
    }
}
