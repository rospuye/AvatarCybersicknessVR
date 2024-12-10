using UnityEngine;

public class VRRig : MonoBehaviour
{
    public Transform headConstraint;
    public Vector3 headBodyOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = headConstraint.position + headBodyOffset;
        transform.forward = Vector3.ProjectOnPlane(headConstraint.up,Vector3.up).normalized;

        
    }
}
