using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    public GameObject black_chips;
    public GameObject white_chips;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(gameObject.transform.position, Vector3.right);
        if (Physics.Raycast(ray, out hit, 30f))
        {
            if (hit.collider.gameObject == black_chips || hit.collider.gameObject == white_chips)
            {
                gameObject.transform.position += Vector3.right * 1.98f;
            }
        }
    }
}
