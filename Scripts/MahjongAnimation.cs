using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahjongAnimation : MonoBehaviour
{
    public int WinID;
    public void GO()
    {
        go = true;
        GameManager.GM.LoadROundEndAnimation(WinID);
        GetComponent<Animator>().SetTrigger("go");
    }
    public void HideText()
    {
        GetComponent<Animator>().SetTrigger("hide text");
    }
    public void EndAnimation()
    {
        GetComponent<Animator>().SetTrigger("end");
    }

    bool go;
    public void DestroyObj()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        if(go)
        {
            transform.GetChild(0).localPosition = Vector3.Lerp(transform.GetChild(0).localPosition, Vector3.zero, 5f*Time.deltaTime);
        }
       
    }
}
