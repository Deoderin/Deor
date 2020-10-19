using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchController : MonoBehaviour
{

    const int mouseidtouch = 99;




    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;


    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))
            { CustomMouseDown(Input.mousePosition, mouseidtouch); }
        }

    }
    public void CustomMouseDown(Vector2 pos, int id)
    {
        CanvasDown(pos, id);
    }

    public void CanvasDown(Vector2 pos, int id)
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = pos;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult result in results)
        {
            if(!Application.isEditor)
            {
                if (result.gameObject.GetComponent<Button>() != null) result.gameObject.GetComponent<ButtonFunction>().Click();
            }
           
        }
        if (results.Count > 0 && ChekerUWP.RotateTouchId == id) ChekerUWP.RotationTouchDispose();
        if (Screen.width > 2500) WorldDown(pos, id, results.Count);

    }

    public void WorldDown(Vector2 pos, int id, int countResult)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        //check all new touch letting the ray from their coordinates into the game scene
        if (Physics.Raycast(ray, out hit, 100) && countResult == 0)
        {
            if (hit.transform.tag == "Domino")
            {
               
                CellDrag(hit.collider.gameObject, id, hit.point);
            }
            else if (hit.transform.tag == "DominoShower")
            {
                ShowerDrag(hit.collider.gameObject, id, hit.point);
            }
        }
    }

    public void ShowerDrag(GameObject dragCell, int id, Vector3 point)
    {
        if (GameManager.GState == GameManager.GameState.Drag || GameManager.GState == GameManager.GameState.Steel)
        {
            PlayerController PC = dragCell.transform.parent.GetComponent<PlayerController>();
            dragCell.GetComponent<DominoShower>().activate = !dragCell.GetComponent<DominoShower>().activate;
            if(dragCell.GetComponent<DominoShower>().activate)
            {
                PC.CheckCombiantion();
            }
            else
            {
                PC.DeleteSetShower();
            }
           // ChekerUWP.DragObj = true;
           // ChekerUWP.DragTouchId = id;
            //GameManager.CurrendDraggingCell = dragCell;
            dragCell.GetComponent<Animator>().SetBool("show", dragCell.GetComponent<DominoShower>().activate);
           // CellDragging.DragMoment = true;
            dragCell.GetComponent<DominoShower>().Drag = true;
        }
    }
    public static void SetALternativePosToAll(bool set)
    {
        foreach (GameObject dominoSelector in GameObject.FindGameObjectsWithTag("DominoDetectors"))
        {
            dominoSelector.GetComponent<DominoPartInfo>().SetAlternativePos(set);
        }
    }
    public void CellDrag(GameObject dragCell, int id, Vector3 point)
    {
        CellDragging CD = dragCell.GetComponent<CellDragging>();
        bool drag = GameManager.GState == GameManager.GameState.Drag;
        bool steel = GameManager.GState == GameManager.GameState.Steel;
        if (drag && CD.State == CellDragging.DominoState.InHand && !CD.OpenSet || steel && CD.State == CellDragging.DominoState.DiscardNow || steel && CD.State == CellDragging.DominoState.InHand && !CD.OpenSet)
        {
            if(GameManager.GState == GameManager.GameState.Steel)
            {
                CD.Player = GameManager.PlayersS.transform.GetChild(GameManager.GetIndexPlayerByID(GameManager.GM.DiscardPlayerId)).gameObject;
                CD.Player.GetComponent<PlayerController>().LastPickedTile = CD.gameObject;
                CD.SetColor();
                CD.transform.SetParent(CD.Player.transform);
            }
            CD.Player.GetComponent<PlayerController>().DeleteSetShower();

            CD.DragID = id;
            CD.GetComponent<Rigidbody>().isKinematic = true;
            if(drag)
            {
                CD.State = CellDragging.DominoState.Drag;
            }
            else if (steel)
            {
                CD.State = CellDragging.DominoState.DiscardDrag;
            }

            
            SetALternativePosToAll((CD.ScoreInCells.x == CD.ScoreInCells.y));
            foreach (GameObject dominoSelector in GameObject.FindGameObjectsWithTag("DominoDetectors"))
            {
                DominoPartInfo dpi = dominoSelector.GetComponent<DominoPartInfo>();
                if (dpi.value == CD.ScoreInCells.x || dpi.value == CD.ScoreInCells.y)
                {
                    dominoSelector.GetComponent<Animator>().SetBool("show", true);
                }
            }
            CD.NextPosition = point;

        }
    }
}

          
  