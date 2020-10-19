using UnityEngine;

public class RaycastExample : MonoBehaviour
{
    // See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.back, out hit, 100.0f))
            print("Found an object - distance: " + hit.distance);
    }
}