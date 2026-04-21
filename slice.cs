using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
public class slice : MonoBehaviour
{
    // public Transform planeDebug;
    // public GameObject target;
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public LayerMask slicableLayer;
    public Material crossSection;
    public VelocityEstimator velocityEstimator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if (Keyboard.current.spaceKey.wasPressedThisFrame)
        // {
        //     Slice(target);
        // }
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, slicableLayer);
        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();
        // SlicedHull hull = target.Slice(planeDebug.position, planeDebug.up);
        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        if(hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, crossSection);
            SetupSlicedComponent(upperHull);

            GameObject lowerHull = hull.CreateLowerHull(target, crossSection);
            SetupSlicedComponent(lowerHull);

            Destroy(target);
        }
    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        slicedObject.layer = LayerMask.NameToLayer("Slicable");
        slicedObject.tag = "Slime";

        Grabbable gb = slicedObject.AddComponent<Grabbable>();
        GrabInteractable gi = slicedObject.AddComponent<GrabInteractable>();
        HandGrabInteractable hgi = slicedObject.AddComponent<HandGrabInteractable>();
        gi.InjectOptionalPointableElement(gb);
        gi.InjectRigidbody(rb);
        hgi.InjectOptionalPointableElement(gb);
        hgi.InjectRigidbody(rb);
        // rb.AddExplosionForce(0.5f)
    }
}
