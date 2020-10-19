using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChekerUWP : MonoBehaviour
{
    public static bool updateCall;
    public static bool updateTouch;
    public int TypeTable;
    public class InputTypeo
    {
        public InputTypeo(int ids, Vector3 posss)
        {
            this.id = ids;
            this.curTouchPos = posss;
        }
        public Vector3 curTouchPos;
        public int id;
    };
    public class InputTypeMain
    {
        public InputTypeMain(int ids, Vector3 posss)
        {
            this.id = ids;
            this.curTouchPos = posss;    
        }
        public Vector3 curTouchPos;
        public int interactiveMode;
        public bool interactiveModeButToo;
        public int id;

        public static string activate;
    };


    public static string activate;
    public static bool QuatrFull;
    public static bool QuatrWindow;
    public static bool DuetFull;
    public static bool DuetWindow;
    public Vector2[] ResTableType = new Vector2[] { new Vector2(3840, 2160), new Vector2(1810, 970), new Vector2(1920, 1920), new Vector2(1810, 870) };

    public enum TableType { Duet, Quatros, }
    public static TableType ThisTableType;

    public enum WindowType { Window, FullScreen, }
    public static WindowType TypeWindow = WindowType.Window;


    public static List<InputTypeo> myObj = new List<InputTypeo>();
    public List<InputTypeMain> myObjMain = new List<InputTypeMain>();
    public int ResolutionMode;

    public static int DragTouchId = -1;
    public static bool DragObj = false;

    public static int RotateTouchId = -1;
    public static bool RotateObj = false;
    public static float RotateStartAngle = -1;
    public static float RotateCellStartAngle = -1;

    public void SelectModeRes()
    {
        Vector2 CurSize = new Vector2(Screen.width, Screen.height);
        if (CurSize == ResTableType[0])
        {
            ResolutionMode = 0;
            TypeWindow = WindowType.FullScreen;
            ThisTableType = TableType.Quatros;
            TypeTable = 1;
        }
        else if (CurSize == ResTableType[1])
        {
            ResolutionMode = 1;
            TypeWindow = WindowType.Window;
            ThisTableType = TableType.Quatros;
            TypeTable = 0;
        }
        else if (CurSize == ResTableType[2])
        {
            ResolutionMode = 3;
            TypeWindow = WindowType.FullScreen;
            ThisTableType = TableType.Duet;
            TypeTable = 1;
        }
        else if (CurSize == ResTableType[3])
        {
            ResolutionMode = 2;
            TypeWindow = WindowType.Window;
            ThisTableType = TableType.Duet;
            TypeTable = 1;
        }
        else
        {
            TypeWindow = WindowType.Window;
            ThisTableType = TableType.Quatros;
            TypeTable = 1;
            ResolutionMode = 0;
        }
    }


    void Awake()
    {

        SelectModeRes();
    }
    // Use this for initialization
    void Start()
    {
        QuatrFull = (ThisTableType == TableType.Quatros) && (TypeWindow == WindowType.FullScreen);
        QuatrWindow = (ThisTableType == TableType.Quatros) && (TypeWindow == WindowType.Window);
        DuetFull = (ThisTableType == TableType.Duet) && (TypeWindow == WindowType.FullScreen);
        DuetWindow = (ThisTableType == TableType.Duet) && (TypeWindow == WindowType.Window);
    }


    public void AddTouch(int z)
    {
        myObjMain.Add(new InputTypeMain(myObj[z].id, myObj[z].curTouchPos));
        if (GameManager.GState == GameManager.GameState.Drag && DragObj)
        {
            //проверка расстояния касания для поворота плитки
            int ind = myObjMain.FindIndex(x => x.id == DragTouchId);

            if (ind != -1)
            {
                if (Vector3.Distance(myObj[z].curTouchPos, myObjMain[ind].curTouchPos) <= 400)
                {
                    //создаем касание поворачиватель
                 
                    RotateObj = true;
                    RotateTouchId = myObj[z].id;
                    RotateStartAngle = GetAngle(myObjMain[ind].curTouchPos, myObj[z].curTouchPos);
                    RotateCellStartAngle = -1;
                }
            }
          
        }
        GetComponent<TouchController>().CustomMouseDown(myObj[z].curTouchPos, myObj[z].id);
    }

    public static float GetAngle(Vector2 center,Vector2 rotpoint)
    {
       
        float res = Mathf.Atan2(center.y - rotpoint.y, rotpoint.x - center.x) * (180 / Mathf.PI);
        return (res < 0) ? res + 360 : res;
    }

    public void CheckTouch()
    {
        for (int z = 0; z < myObjMain.Count; z++)
        {
            InputTypeo found = myObj.Find(item => item.id == myObjMain[z].id);
            if (found == null)
            {
                myObjMain.RemoveAt(z);
            }
        }
    }

    public void AddHistoryAndBorderCheck()
    {
        for (int d = 0; d < myObjMain.Count; d++)
        {
            InputTypeo found2 = myObj.Find(item => item.id == myObjMain[d].id);
            if (found2 != null)
            {   if(Vector3.Distance(myObjMain[d].curTouchPos, found2.curTouchPos) >= 270)
                {       
                    myObjMain.RemoveAt(d);
                }
                else
                {
                    myObjMain[d].curTouchPos = found2.curTouchPos;
                }
            
            }
            if (QuatrWindow)
            {
                if (CheckPlaceCoordOUT(myObjMain[d].curTouchPos, 1830, 1, 960, 1))
                {
                    myObjMain.RemoveAt(d);
                }
            }
            if (DuetWindow)
            {
                if (CheckPlaceCoordOUT(myObjMain[d].curTouchPos, 1830, 1, 860, 1))
                {
                    myObjMain.RemoveAt(d);
                }
            }
        }
    }

    public bool CheckPlaceCoordOUT(Vector3 point, int xmax, int xmin, int ymax, int ymin)
    {
        return (point.x > xmax) || (point.y > ymax) || (point.x < xmin) || (point.y < ymin);
    }
    public bool CheckPlaceCoordIN(Vector3 point, int xmax, int xmin, int ymax, int ymin)
    {
        return (point.x < xmax) && (point.y < ymax) && (point.x > xmin) && (point.y > ymin);
    }

    public void BorderCheck(int z)
    {
        if (QuatrFull || QuatrWindow && CheckPlaceCoordIN(myObj[z].curTouchPos, 1830, 35, 960, 25) || DuetWindow && CheckPlaceCoordIN(myObj[z].curTouchPos, 1830, 35, 860, 25) || DuetFull)
        {
            AddTouch(z);
        }
    }

    public void ActualTouchCont(int z)
    {
        if (myObjMain != null && myObjMain.Count > 0)
        {
            NewDataCompare(z);
        }
        else
        {
            BorderCheck(z);
        }
    }
    public void NewDataCompare(int z)
    {
        if (myObjMain.Find(item => item.id == myObj[z].id) == null)
        {
            BorderCheck(z);
        }
    }
    public void StartTouchInit()
    {
        updateCall = true;
        updateTouch = true;
        if (myObj.Count > 0)
        {
            for (int z = 0; z < myObj.Count; z++)
            {
                ActualTouchCont(z);
            }
        }
    }


    public static void RotationTouchDispose()
    {
        RotateTouchId = -1;
        RotateObj = false;
        RotateStartAngle = -1;
        RotateCellStartAngle = -1;
    }
 

    // Update is called once per frame
    void Update()
    {
        SelectModeRes();
        QuatrFull = (ThisTableType == TableType.Quatros) && (TypeWindow == WindowType.FullScreen);
        QuatrWindow = (ThisTableType == TableType.Quatros) && (TypeWindow == WindowType.Window);
        DuetFull = (ThisTableType == TableType.Duet) && (TypeWindow == WindowType.FullScreen);
        DuetWindow = (ThisTableType == TableType.Duet) && (TypeWindow == WindowType.Window);

        StartTouchInit();
        CheckTouch();
        AddHistoryAndBorderCheck();
    }
}
