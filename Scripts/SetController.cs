using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetController : MonoBehaviour
{
    public enum SetType
    {
        Pair = 0,
        Chow = 1,
        Pong = 2,
        Kong = 3,
    }
    public SetType type;
    public bool OpenSet;
    public CellDragging Container;
    int Timer = -2;
   // public bool invert;
    // Start is called before the first frame update

    public void OpenKong()
    {
        if(!OpenSet)
        {
            if (Timer == -2)
            {
                for (int i = 1; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).gameObject.activeSelf) transform.GetChild(i).GetChild(0).GetComponent<Animator>().SetBool("click", true);
                }
                Timer = 200;
            }
            else
            {
                for (int i = 1; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).gameObject.activeSelf) transform.GetChild(i).GetChild(0).GetComponent<Animator>().SetBool("click", false);
                }

                Container.Player.GetComponent<PlayerController>().CheckDiscardSteelSet(Container.gameObject,true);

                //открыть сет
            }
        }
       
    }
    private void Update()
    {
        if(Timer >=0)
        {
            Timer--;
        }
        else
        {
            for (int i = 1;i < transform.childCount;i++ )
            {
                if (transform.GetChild(i).gameObject.activeSelf) transform.GetChild(i).GetChild(0).GetComponent<Animator>().SetBool("click", false);
            }
           
            Timer = -2;
        }
    }
    public void ResetIndicator()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild((int)type).gameObject.SetActive(true);
    }
    void Start()
    {
        transform.GetChild((int)type).gameObject.SetActive(true);
        
    }
    public void DestroyObj()
    {
        Destroy(this.gameObject);
    }
}
