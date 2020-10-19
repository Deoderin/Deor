using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacityCheker : MonoBehaviour
{
    private CellDragging parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.GetComponent<CellDragging>();
    }
    public void OpacityCheck()
    {
        if(parent != null)
        {
            if (!parent.Set && !parent.SetAbsolute)
            {
                Ray ray = new Ray(transform.position + Vector3.down * 0.047f, Vector3.up * 1.5f);
                RaycastHit hit;
                Renderer mat = GetComponent<Renderer>();
                //check all new touch letting the ray from their coordinates into the game scene
                if (Physics.Raycast(ray, out hit, 8))
                {
                    mat.material.color = new Color(mat.material.color.r, mat.material.color.g, mat.material.color.b, 0);
                }
                else
                {
                    mat.material.color = new Color(mat.material.color.r, mat.material.color.g, mat.material.color.b, 1);
                }
            }
        }
    }
}
