using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabObject : MonoBehaviour
{
    public GameObject endScreen;
    public float distanceFromUser = 2.0f;
    public float verticalOffset = 0.5f;

    private XRGrabInteractable grabInteractable;
    private Transform userCamera;

    public SessionManager sessionManager;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        userCamera = Camera.main.transform;
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log($"{gameObject.name} was grabbed by {args.interactorObject.transform.name}");

        if (sessionManager != null)
        {
            sessionManager.StopRecordingMovement();
        }

        if (endScreen != null && userCamera != null)
        {
            endScreen.SetActive(true);
        }
    }
}