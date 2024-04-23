using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;

public class SliceObject : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;

    public Material crossSectionMaterial;
    public float cutForce = 2000;

    public GameObject Blood;

    // Reference to AudioManager2 script
    private AudioManager2 audioManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get reference to AudioManager2 script
        audioManager = FindObjectOfType<AudioManager2>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);
        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            if (target.gameObject.tag == "Zombie")
            {
                Slice(target);

                Instantiate(Blood, transform.position, Quaternion.identity);
            }
            

        }
                
    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        if (hull != null)
        {
            // Check if the sliced object is on the penalty layer (assuming layer 6 is for penalty objects)
            if (target.layer == 6) // Using the layer number directly
            {
                DisplayScore.score -= 5; // Deduct points for slicing a penalty object
            }
            else
            {
                DisplayScore.score++;
            }

            // Play the slicing sound effect
            audioManager.PlaySlicedSound();

            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            SetupSlicedComponent(upperHull);

            GameObject loverHull = hull.CreateLowerHull(target, crossSectionMaterial);
            SetupSlicedComponent(loverHull);

            Destroy(target);
        }
    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }
}
