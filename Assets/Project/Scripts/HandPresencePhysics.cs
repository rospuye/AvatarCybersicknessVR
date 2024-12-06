using UnityEngine;

public class HandPresencePhysics : MonoBehaviour
{
    public Transform target;
    private Rigidbody rb;
    private Collider[] handColliders;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        handColliders = GetComponentsInChildren<Collider>();
    }

    public void EnableHandCollider(){
        foreach (var item in handColliders){
            item.enabled = true;
        }
    }

    public void EnableHandColliderDelay (float delay){
        Invoke("EnableHandCollider",delay);
    }

    public void DisableHandCollider(){
        foreach (var item in handColliders){
            item.enabled = false;
        }
    }

    void FixedUpdate()
    {
        // manually calculate velocity
        rb.linearVelocity = (target.position - transform.position)/Time.fixedDeltaTime;
        
        // manually calculate angular velocity
        Quaternion rotationDiff = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDiff.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);
        Vector3 rotationDiffInDegree = angleInDegree * rotationAxis;

        rb.angularVelocity = (rotationDiffInDegree * Mathf.Deg2Rad)/Time.fixedDeltaTime;
    }
}
