using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoDetector : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        transform.parent.GetComponent<CellDragging>().EnterCollission(other);
    }

    void OnTriggerExit(Collider other)
    {
        transform.parent.GetComponent<CellDragging>().ExitCollission(other);
    }
}
