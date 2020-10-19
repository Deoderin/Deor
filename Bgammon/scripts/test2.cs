using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        Ray ray = new Ray(gameObject.transform.position, Vector3.right);

        //Debug.DrawLine(gameObject.transform.position, Vector3.right, Color.red, 5f);
        if (Physics.Raycast(ray, out hit, 50))
        {
            Debug.LogError(ray);
            if (hit.collider)
            {
                Debug.LogError(hit);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Gest();
        }
    }
    void Gest()
    {
        Debug.DrawRay(gameObject.transform.position, Vector3.right, Color.red, 10f);
    }

}
