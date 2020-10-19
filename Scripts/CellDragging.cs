using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellDragging : MonoBehaviour
{
    public Vector3 NextPosition;
    public bool activeTouch;
    public bool Set;
    public bool SetAbsolute;
    public Vector2 ScoreInCells;
    public Vector3 SetAngle = Vector3.zero;
    public bool canMove;
    public static bool DragMoment;

    public GameObject doubleColiders;
    public GameObject XColiders;
    public GameObject YColiders;

    public GameObject GF;
    public GameManager GM;

    public GameObject Player;
    public int PosInHand;

    public GameObject TimerPrefab;
    public int floor;
    public bool InDiscardSpace;
    public int PosDominoInArraySpawner;
    public int DragID;
    public bool OpenSet;
    public int openSetPosIDPlace;
    public SetController SC;
    public bool DeadWall;

    //какая стена-какаой этаж-какой индекс
    public Vector3 PosInWall;
    //вариация конга когда боковой тайл или оба боковых тайла должны быть перевернуты
    public bool OpenKongCloseTile;
    public enum DominoState
    {
        InHand,
        Drag,
        DiscardNow,
        DiscardDrag,
        DiscardAbsolute,
        Set,
    }
    public DominoState State = DominoState.InHand;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0, -0.0085f, 0);

        if (State == DominoState.InHand)
        {
            SetAngle = Vector3.zero;
        }

        // Debug.LogError(Player != null);
        
        //transform.GetChild(0).GetComponent<Renderer>().material.SetColor("Color_12D30E0D", Color.black);
        GF = GameObject.Find("GamePlane").gameObject;
        GM = GameObject.Find("GM").GetComponent<GameManager>();
        DragMoment = false;
    }

    public void SetColor()
    {
        if (Player != null)
        {
            // Debug.LogError(Player.GetComponent<PlayerController>().playerColor);
            transform.GetChild(1).GetComponent<Renderer>().material.SetColor("Color_12D30E0D", Player.GetComponent<PlayerController>().playerColor);
        }
    }

    public void InitDOminoInfo()
    {
        //if (ScoreInCells.x == ScoreInCells.y)
        //{
        //    doubleColiders.transform.GetChild(0).GetComponent<DominoPartInfo>().value = (int)ScoreInCells.x;
        //    doubleColiders.transform.GetChild(1).GetComponent<DominoPartInfo>().value = (int)ScoreInCells.x;
        //}
        //else
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        XColiders.transform.GetChild(i).GetComponent<DominoPartInfo>().value = (int)ScoreInCells.y;
        //    }
        //    for (int i = 0; i < 3; i++)
        //    {
        //        YColiders.transform.GetChild(i).GetComponent<DominoPartInfo>().value = (int)ScoreInCells.x;
        //    }
        //}
    }

    public void ResetAngle()
    {
        SetAngle = Vector3.zero;
    }

    public void HardSetCell()
    {
        Set = true;
        activeTouch = false;
        SetAbsolute = true;
    }

    //public int SelectDoubleIndex(DominoPartInfo dpi)
    //{
    //    float z = dpi.transform.position.z - dpi.mainParent.transform.position.z;
    //    float x = dpi.transform.position.x - dpi.mainParent.transform.position.x;
    //    return (Mathf.Abs(x) > Mathf.Abs(z)) ? ((x < 0) ? 0 : 1) : ((z < 0) ? 1 : 0);
    //}
    public Vector2 QautrosBorders = new Vector2(3, 2);

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Discard")
        {
            InDiscardSpace = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Discard")
        {
            InDiscardSpace = false;
        }
    }


    public static bool CheckDisSet(CellDragging CD,int CheckPlayerindex)

    {
        bool z = false;
        if(CheckPlayerindex == -1)
        {
            for (int i = 0; i < GameManager.PlayersS.transform.childCount; i++)
            {
                int id = GameManager.PlayersS.transform.GetChild(i).GetComponent<PlayerController>().id;
                if (id != CD.Player.GetComponent<PlayerController>().id)
                {
                    int nextindex = GameManager.GetIndexPlayerByID(CD.Player.GetComponent<PlayerController>().id) - 1;
                    if (nextindex < 0) nextindex = (GameManager.PlayersS.transform.childCount - 1);
                    bool nextPlayer = (nextindex == i);
                   // Debug.LogError("ALARM2 -" + nextPlayer + " - " + nextindex + " - " + i);
                    bool v = GameManager.PlayersS.transform.GetChild(i).GetComponent<PlayerController>().CheckSteelSetFromDiscard(CD.ScoreInCells, nextPlayer);
                   // Debug.LogError("ALARM -" + v);
                    if (v)
                    {
                        GameObject.Find("PB" + id).transform.GetChild(0).GetComponent<Animator>().SetBool("Show", true);
                        z = true;
                    }
                }
            }
        }
        else
        {
            int id = GameManager.PlayersS.transform.GetChild(CheckPlayerindex).GetComponent<PlayerController>().id;
            if (id != CD.Player.GetComponent<PlayerController>().id)
            {
                int nextindex = GameManager.GetIndexPlayerByID(CD.Player.GetComponent<PlayerController>().id) - 1;
                if (nextindex < 0) nextindex = (GameManager.PlayersS.transform.childCount - 1);
                bool nextPlayer = (nextindex == CheckPlayerindex);
              //  Debug.LogError("pALARM2 -" + nextPlayer + " - " + nextindex + " - " + CheckPlayerindex);
                bool v = GameManager.PlayersS.transform.GetChild(CheckPlayerindex).GetComponent<PlayerController>().CheckSteelSetFromDiscard(CD.ScoreInCells, true);
               // Debug.LogError("pALARM -" + v);
                if (v)
                {
                    GameObject.Find("PB" + id).transform.GetChild(0).GetComponent<Animator>().SetBool("Show", true);
                    z = true;
                }
            }
        }
        
        return z;
    }


    public void SetHardDiscard()
    {
        if (transform.parent.GetComponent<WallCOntroller>().floor1.FindIndex(x=>x.GetComponent<CellDragging>().PosInWall == PosInWall) != -1)
        {
            transform.parent.GetComponent<WallCOntroller>().floor1.Remove(gameObject);
        }
        else if(transform.parent.GetComponent<WallCOntroller>().floor2.FindIndex(x => x.GetComponent<CellDragging>().PosInWall == PosInWall) != -1)
        {
            transform.parent.GetComponent<WallCOntroller>().floor2.Remove(gameObject);
        }

        transform.SetParent(GameObject.FindGameObjectWithTag("Discard").transform);
        transform.localPosition = new Vector3(Random.Range(-1f,1f)/2, Random.Range(0.4f, 1f), Random.Range(-1f, 1f)/2);
        //transform.localScale = new Vector3(4.3f, 4.3f, 8.5f);
   
        

        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(-10, 10), 0), ForceMode.VelocityChange);
        State = DominoState.DiscardAbsolute;
    }

    public void CheckGrid()
    {
        GameManager.CurrendDraggingCell = null;

        DragMoment = false;
        Player.GetComponent<PlayerController>().CheckCombiantion();

        if (InDiscardSpace && GameObject.Find("DiscardTimer(Clone)") == null && State != DominoState.DiscardDrag)
        {
            if (Player.GetComponent<PlayerController>().id == GameManager.PlayersS.transform.GetChild(GameManager.ActivePlayerIndex).GetComponent<PlayerController>().id)
            {
                transform.SetParent(GameObject.FindGameObjectWithTag("Discard").transform);
            //    transform.localScale = new Vector3(4.3f, 4.3f, 8.5f);
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(-10, 10), 0), ForceMode.VelocityChange);
                if (CheckDisSet(this,-1))
                {
                    State = DominoState.DiscardNow;
                    Instantiate(TimerPrefab, GameObject.FindGameObjectWithTag("Canvas").transform).GetComponent<DiscardTimer>().tile = gameObject;
                    transform.GetChild(0).GetComponent<Animator>().SetBool("active",true);
                }
                else
                {
                    State = DominoState.DiscardAbsolute;
                    Saveloader.SaveFile.DiscardTiles.Add(PosInWall);

                    GameManager.nextTurn(true, -1);
                }
               
                
            }
            else
            {
                Absolve();
            }
        }
        else if (State == DominoState.DiscardDrag)
        {
            Absolve();
            Player.GetComponent<PlayerController>().CheckDiscardSteelSet(gameObject,false);


        }
        else Absolve();



    }



    public bool BorderDirectionCheck(int direction, int border)
    {
        if (direction == 2)
        {
            if (border == 0)
            {
                return (transform.position.x < -QautrosBorders.x);
            }
            else if (border == 1)
            {
                return (transform.position.z < -QautrosBorders.y);
            }
        }
        else if (direction == 1)
        {
            if (border == 0)
            {
                return (transform.position.x > QautrosBorders.x);
            }
            else if (border == 1)
            {
                return (transform.position.z > QautrosBorders.y);
            }
        }
        return false;
    }
    public bool BorderCheck()
    {
        return (transform.position.x < -QautrosBorders.x || transform.position.x > QautrosBorders.x || transform.position.z < -QautrosBorders.y || transform.position.z > QautrosBorders.y);
    }


    private void Absolve()
    {
        ResetAngle();
        if (State == DominoState.DiscardDrag)
        {
            GameObject.FindGameObjectWithTag("Discard").GetComponent<Collider>().enabled = true;
        }
        State = DominoState.InHand;
    }
    public bool magnitide;
    private Vector3 Check2(Vector3 NextPos)
    {
        if (currentSelectedDomino != null)
        {
            if (magnitide && Vector3.Distance(NextPos, currentSelectedDomino.transform.position) < 0.2f)
            {
                return new Vector3(currentSelectedDomino.transform.position.x, GF.transform.position.y + 0.48f, currentSelectedDomino.transform.position.z);
            }
            else magnitide = false;
        }
        else magnitide = false;

        return NextPos;
    }

    public void ActivateColliders(int mode)
    {
        switch (mode)
        {
            case 0:
                doubleColiders.transform.GetChild(0).gameObject.SetActive(true);
                doubleColiders.transform.GetChild(1).gameObject.SetActive(true);
                break;
        }
    }
    public void ExitCollission(Collider other)
    {
        if (currentSelectedDomino == other.gameObject)
        {
            currentSelectedDomino = null;
        }
    }

    public void Rotate(DominoPartInfo dpi)
    {
        //SetAngle = new Vector3(-90, 0, 0);
        //if (ScoreInCells.x == ScoreInCells.y)
        //{
        //    float z = dpi.transform.position.z - dpi.mainParent.transform.position.z;
        //    if (Mathf.Abs(z) > 0.1f) SetAngle.z = 90;
        //}
        //else
        //{
        //    float z = dpi.transform.position.z - dpi.mainParent.transform.position.z;
        //    float x = dpi.transform.position.x - dpi.mainParent.transform.position.x;
        //    if (Mathf.Abs(x) > Mathf.Abs(z))
        //    {
        //        SetAngle.z = (x < 0) ? ((dpi.value == ScoreInCells.x) ? 90 : 270) : ((dpi.value == ScoreInCells.x) ? 270 : 90);
        //    }
        //    else
        //    {
        //        SetAngle.z = (z < 0) ? ((dpi.value == ScoreInCells.x) ? 0 : 180) : ((dpi.value == ScoreInCells.x) ? 180 : 0);
        //    }
        //}
    }

    public void EnterCollission(Collider other)
    {
        //if (State == DominoState.Drag)
        //{
        //    currentSelectedDomino = other.gameObject;
        //    DominoPartInfo dpi = currentSelectedDomino.GetComponent<DominoPartInfo>();
        //    if (dpi.value == ScoreInCells.x || dpi.value == ScoreInCells.y)
        //    {
        //        if (!magnitide)
        //        {
        //            ChekerUWP.RotationTouchDispose();
        //            magnitide = true;
        //        }
        //        Rotate(dpi);
        //    }
        //}
    }

    public GameObject currentSelectedDomino;

    public bool show;
    public bool SteelFromKongActive;
    public bool SteelFromKong;
    // Update is called once per frame
    void Update()
    {
        if (State == DominoState.DiscardNow || State == DominoState.DiscardAbsolute)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0)), 15f * Time.deltaTime);
        }

        if (State == DominoState.Set)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(Vector3.right * 180), 4f * Time.deltaTime);
        }
        if (State == DominoState.Drag || State == DominoState.DiscardDrag)
        {
            if (transform.parent.GetComponent<PlayerController>() != null)
            {
                transform.parent.GetComponent<PlayerController>().outSortRange = (transform.localPosition.magnitude > 3.6);
            }
            if (Application.isEditor)
            {
                Vector3 touch = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 37.7f);
                Vector3 mousepos = Camera.main.ScreenToWorldPoint(touch);
                NextPosition = new Vector3(mousepos.x, GF.transform.position.y + 0.48f, mousepos.z);

                Vector3 NextPos = Check2(NextPosition);
                transform.position = NextPos;
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(SetAngle), 4f * Time.deltaTime);
                if (Input.GetMouseButtonUp(0))
                {
                    DragID = 0;
                    ResetAngle();
                    CheckGrid();
                    Player.GetComponent<PlayerController>().outSortRange = false;
                }
            }
            else
            {
                ChekerUWP CUWP = GameObject.FindGameObjectWithTag("Canvas").GetComponent<ChekerUWP>();
                int ind = CUWP.myObjMain.FindIndex(x => x.id == DragID);
                if (ind != -1)
                {
                    Vector3 touch = new Vector3(CUWP.myObjMain[ind].curTouchPos.x, CUWP.myObjMain[ind].curTouchPos.y, 37.7f);
                    Vector3 mousepos = Camera.main.ScreenToWorldPoint(touch);
                    NextPosition = new Vector3(mousepos.x, GF.transform.position.y + 0.48f, mousepos.z);
                    Vector3 NextPos = Check2(NextPosition);
                    transform.position = NextPos;
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(SetAngle), 4f * Time.deltaTime);
                }
                else
                {
                    DragID = 0;
                    ResetAngle();
                    CheckGrid();
                    Player.GetComponent<PlayerController>().outSortRange = false;
                }
            }
        }

        if(!SteelFromKongActive  && State == DominoState.DiscardNow && OpenSet)
        {
            foreach (PlayerController.CombinationsInfo comb in Player.GetComponent<PlayerController>().OpenCombinations)
            {
                if(comb.CombinationsFirstTile == gameObject)
                {
                    comb.HasOpenSteelKong2 = false;
                    comb.Combinations = PlayerController.SetType.Pong;
                    comb.CombinationsFirstTile = comb.OtherTiles[0];



                    CellDragging CDO = comb.CombinationsFirstTile.GetComponent<CellDragging>();
                    CDO.SC = comb.SetIndicator.GetComponent<SetController>();
                    comb.OtherTiles.Remove(comb.CombinationsFirstTile);

                    comb.SetIndicator.GetComponent<SetController>().type = (SetController.SetType)((int)(PlayerController.SetType.Pong));
                    comb.SetIndicator.GetComponent<SetController>().ResetIndicator();
                    Player.GetComponent<PlayerController>().LastOpenSetTilesSort--;
                }
            }
            transform.SetSiblingIndex(Player.GetComponent<PlayerController>().LastOpenSetTilesSort + 1);
            Player.GetComponent<PlayerController>().RebuildOpenSetPlaces();
            SC = null;
            // функция ребилда полного открытой части руки
            SteelFromKong = true;
            SteelFromKongActive = true;
        }

        if (State == DominoState.InHand || State == DominoState.DiscardNow && OpenSet)
        {
            show = Player.transform.GetChild(0).GetComponent<DominoShower>().activate || GameManager.GState == GameManager.GameState.RoundResults || OpenSet;
            Vector3 staterot = OpenKongCloseTile ? Vector3.right * 180 : (show ? (Vector3.zero) : (Vector3.right * 270));

            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(staterot), 4f * Time.deltaTime);
            //Vector3 doppos = (transform.parent.GetComponent<PlayerController>().id == GameManager.PlayersS.transform.GetChild(GameManager.ActivePlayerIndex).GetComponent<PlayerController>().id || GameManager.GState == GameManager.GameState.RoundResults) ? Vector3.zero : new Vector3(0, 0, -0.5f);
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero + ((Vector3.right * 0.36f) * (PosInHand)), 0.2f);
        }
    }
}
