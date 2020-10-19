using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoShower : MonoBehaviour
{
    public int PosInHand;
    public bool Drag;
    public bool activate;
    // Update is called once per frame
    void Update()
    {
        if (Drag)
        {
            if (Application.isEditor)
            {
              if(Input.GetMouseButtonUp(0))
                {
                //    GetComponent<Animator>().SetBool("show", false);
                    Drag = false;
                    CellDragging.DragMoment = false;
                }
               
            }
            else if (ChekerUWP.DragObj)
            {
               
                ChekerUWP CUWP = GameObject.FindGameObjectWithTag("Canvas").GetComponent<ChekerUWP>();
                int ind = CUWP.myObjMain.FindIndex(x => x.id == ChekerUWP.DragTouchId);
                if (ind == -1)
                {
                   // GetComponent<Animator>().SetBool("show", false);
                    ChekerUWP.DragObj = false;
                    ChekerUWP.DragTouchId = -1;
                    CellDragging.DragMoment = false;
                     Drag = false;
                }
            }
        }


        bool s = (transform.parent.GetComponent<PlayerController>().id == GameManager.PlayersS.transform.GetChild(GameManager.ActivePlayerIndex).GetComponent<PlayerController>().id && GameManager.GState != GameManager.GameState.StartGame && GameManager.GState != GameManager.GameState.RoundResults);
        Vector3 staterot = s ? (Vector3.right * 90) : (Vector3.right * -90);

        transform.rotation = Quaternion.Euler((Vector3.right * 90));
        Vector3 doppos = s ? Vector3.zero : new Vector3(0, 0, -0.5f);
       
        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero + ((Vector3.right * 0.36f) * PosInHand), 1.2f);
    }
}
