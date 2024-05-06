using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;
using UnityEngine.XR;

//followed Valem Tutorials "How to Slice in VR - Unity XR Tutorial"
public class SaberSlice : MonoBehaviour
{

    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEst; 
    public LayerMask sliceLayer; 
    public Material crossSectionMat;    //could set as the saber material so it looks like the cut glows
    public float cutForce = 2000; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceLayer);
        if(hasHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEst.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMat);
            SetupSlicedComponent(upperHull);
            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMat);
            SetupSlicedComponent(lowerHull);
            Destroy(target);
        }
    }

    public void SetupSlicedComponent (GameObject slicedObj)
    {
        Rigidbody rb = slicedObj.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObj.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.useGravity = false;
        rb.AddExplosionForce(cutForce, slicedObj.transform.position, 1);
    }
}
