using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyButton : MonoBehaviour
{
    public bool peopleCheker;
    void DestroyObj()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        if (peopleCheker && GameManager.PlayersS.transform.childCount > 1) GetComponent<Animator>().SetTrigger("endAlert");
    }
}
