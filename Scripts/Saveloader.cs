using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class Saveloader : MonoBehaviour
{
    public static string Session;
    public static bool SaveReading;
    public GameObject savealert;
    public static bool inst;
    public static bool instdel;
    public static bool instscene;
    public GameObject[] addbut;

    public class TileInHand
    {
        public TileInHand(int PlayerID, Vector3 posinWall, int PosInhand)
        {
            this.PlayerID = PlayerID;
            this.posinWall = posinWall;
            this.PosInhand = PosInhand;
        }
        public Vector3 posinWall = Vector3.zero; //+
        public int PosInhand = 0; //+
        public int PlayerID = 0; //+
    };

    public class SaveObj
    {

        public int pickIndexS;
        public int IndexWall;
        public int East;//++
        public int WallStartIndex;//++
        public int InWallStartIndexSpawn;//++
        public int activeindex = -1;//++
        public int RoundNumber = -1;//++
        public bool GameIsActual = false;//++
        public bool RoundLoad = false;//++
        public bool floor = false;

        public int[] PlayerScore = new int[4];//++

        public List<int> WallSpawn = new List<int>();//++
        public List<Vector3> DiscardTiles = new List<Vector3>();//++
        public List<Vector4> OpenTileFinderSet = new List<Vector4>();//++
        public List<TileInHand> HandTiles = new List<TileInHand>();//++

        public void SetMainParam(int pickIndexS, int IndexWall, int East, int WallStartIndex, int InWallStartIndexSpawn, int activeindex, int RoundNumber, bool GameIsActual, bool RoundLoad, bool floor)
        {
            this.pickIndexS = pickIndexS;
            this.IndexWall = IndexWall;
            this.East = East;//++
            this.WallStartIndex = WallStartIndex;//++
            this.InWallStartIndexSpawn = InWallStartIndexSpawn;//++
            this.activeindex = activeindex;//++
            this.RoundNumber = RoundNumber;//++
            this.GameIsActual = GameIsActual;//++
            this.RoundLoad = RoundLoad;//++
            this.floor = floor;
        }

        public SaveObj()
        {
        }
    };

    public static SaveObj SaveFile;

    public static Saveloader sl;
    public string deb;
    public static SaveObj SaveFileTest;
    public static string Debinfo;
    public NewRound StartNewRoundObj;
    public StartGameButton SGB;
    private void Awake()
    {
        SaveFile = new SaveObj();
    }

    // Start is called before the first frame update
    void Start()
    {
        sl = this;
        Session = "q";
        CreateSave();
    }

    IEnumerator StartloadNewGame()
    {
        GameObject.Find("BlackStart").GetComponent<Animator>().SetBool("end", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public static void PushSaveInfo()
    {
       
        GameManager.UpdateTileInfo();
        SaveFileTest = SaveFile;
        Script.Pushsave();
    }

    public static void LoadSaveS()
    {
        sl.LoadSave();
    }


    public void DestroySGB()
    {
        if (SGB != null)
        {
            Destroy(SGB.gameObject);
        }
    }



    public void SpawnWall()
    {
        GameManager.spawnerS.GetComponent<Spawner>().SaveSpawn(SaveFileTest.InWallStartIndexSpawn, SaveFileTest.WallStartIndex);

    }
    public void SpawnHand()
    {
        foreach (TileInHand discardTiles in SaveFileTest.HandTiles)
        {
            GameManager.spawnerS.GetComponent<Spawner>().GetComponent<Spawner>().SaveAddTile(GameManager.GetIndexPlayerByID(discardTiles.PlayerID), discardTiles.posinWall, discardTiles.PosInhand);

        }
    }
    public void LoadCombinations()
    {
        for (int i = 0; i < 4; i++)
        {
            GameManager.PlayersS.transform.GetChild(GameManager.GetIndexPlayerByID(i)).GetComponent<PlayerController>().SetAllTilesInList();
            GameManager.PlayersS.transform.GetChild(GameManager.GetIndexPlayerByID(i)).GetComponent<PlayerController>().CheckCombiantion();
        }
    }


    public void ActivateOpenSets()
    {
        foreach (Vector4 openTiles in SaveFileTest.OpenTileFinderSet)
        {
            GameObject findedTile = GameManager.PlayersS.transform.GetChild(GameManager.GetIndexPlayerByID((int)openTiles.w)).GetComponent<PlayerController>().AllTilesInHand.Find(x => x.GetComponent<CellDragging>().PosInWall == new Vector3(openTiles.x, openTiles.y, openTiles.z));
            if (findedTile != null)
            {
                GameManager.PlayersS.transform.GetChild(GameManager.GetIndexPlayerByID((int)openTiles.w)).GetComponent<PlayerController>().CheckDiscardSteelSetSave(findedTile, false);
            }

        }
    }

    public void LoadDiscard()
    {
        foreach (Vector3 discardTiles in SaveFileTest.DiscardTiles)
        {
            WallCOntroller WC = GameManager.spawnerS.GetComponent<Spawner>().Wall[(int)discardTiles.x].GetComponent<WallCOntroller>();
          
            if (discardTiles.y == 0)
            {
                CellDragging CD = WC.floor1.Find(x => x.GetComponent<CellDragging>().PosInWall == discardTiles).GetComponent<CellDragging>();
              //  Debug.LogError("LoadDiscard - " + CD.PosInWall);
                CD.SetHardDiscard();
            }
            else
            {
                CellDragging CD = WC.floor2.Find(x => x.GetComponent<CellDragging>().PosInWall == discardTiles).GetComponent<CellDragging>();
              //  Debug.LogError("LoadDiscard - " + CD.PosInWall);
                CD.SetHardDiscard();
            }

        }
    }



    public void LoadWallSpawnerInfo()
    {
        GameManager.spawnerS.GetComponent<Spawner>().pickIndexS = SaveFileTest.pickIndexS;
        GameManager.spawnerS.GetComponent<Spawner>().IndexWall = SaveFileTest.IndexWall;
        GameManager.spawnerS.GetComponent<Spawner>().floor = SaveFileTest.floor;
        GameManager.spawnerS.GetComponent<Spawner>().SelecterWC = GameManager.spawnerS.GetComponent<Spawner>().Wall[SaveFileTest.IndexWall].GetComponent<WallCOntroller>();

    }



    //public void Debuger()
    //{
    //    foreach (Vector3 discardTiles in SaveFileTest.DiscardTiles)
    //    {
    //          Debug.LogError("LoadDiscard - " + discardTiles);
    //    }
    //    foreach (TileInHand discardTiles in SaveFileTest.HandTiles)
    //    {
    //        Debug.LogError("LoadHand - " + discardTiles.posinWall);
    //    }
    //}

    public void INRoundGameLoad()
    {
       // Debuger();


        SpawnWall();
        LoadDiscard();
        SpawnHand();
        LoadCombinations();
        ActivateOpenSets();
        LoadWallSpawnerInfo();
    }

    public void MainValueConnect()
    {
        for (int i = 0; i < 4; i++)
        {
            GameManager.PlayersS.transform.GetChild(GameManager.GetIndexPlayerByID(i)).GetComponent<PlayerController>().WinScore = SaveFileTest.PlayerScore[i];
        }
        GameManager.GM.UpdateScoreSticks();
        for (int i = 0; i < 4; i++)
        {
            GameManager.PlayersS.transform.GetChild(GameManager.GetIndexPlayerByID(i)).GetComponent<PlayerController>().CheckCombiantion();
            GameManager.PlayersS.transform.GetChild(GameManager.GetIndexPlayerByID(i)).GetComponent<PlayerController>().DeleteSetShower();
        }
    }

    public void LoadSave()
    {
        //load save
        if (SaveFileTest != null)
        {
            if (SaveFileTest.GameIsActual)
            {
                GameManager.Round = SaveFileTest.RoundNumber;

                DestroySGB();
                if (!SaveFileTest.RoundLoad)
                {

                    INRoundGameLoad();

                }
                else
                {
                    StartNewRoundObj.LoadNewRoundSave(SaveFileTest.activeindex, SaveFileTest.East);

                }

                MainValueConnect();

                // StartCoroutine(Deleted());

                SaveFile = SaveFileTest;
                SaveFileTest = null;
            }
        }

        inst = true;
    }

    public void SetDominoOnBoard()
    {
        // StartCoroutine(HighlightEnd());

    }
    public IEnumerator Deleted()
    {
        yield return new WaitForSeconds(5.5f);
        //int i = 0;
        ////Debug.LogError("re");
        //while (GameObject.Find("New Game Object") != null)
        //{
        //    i++;
        //    if(i > 100)
        //    {
        //        break;
        //    }
        //    Destroy(GameObject.Find("New Game Object"));
        //}

    }

    //public IEnumerator HighlightEnd()
    //{
    //foreach (Vector2 domino in SaveFileTest.DominoOnBoard)
    //{
    //    //CellDragging CD = GameManager.spawnerS.GetComponent<Spawner>().SpawnInBoard((int)domino.x);
    //    CD.ResetAngle();
    //    foreach (GameObject detector in GameObject.FindGameObjectsWithTag("DominoDetectors"))
    //    {
    //        if (detector.GetComponent<DominoPartInfo>().DirectionSide == (int)domino.y)
    //        {
    //            CD.currentSelectedDomino = detector;
    //            break;
    //        }
    //    }
    //    TouchController.SetALternativePosToAll((CD.ScoreInCells.x == CD.ScoreInCells.y));
    //    CD.CheckGrid();
    //    CD.transform.rotation = Quaternion.Euler(CD.SetAngle);
    //    yield return new WaitForSeconds(0.02f);
    //}
    //for (int i = 0; i < GameManager.PlayersS.transform.childCount; i++)
    //{
    //    GameManager.PlayersS.transform.GetChild(i).GetComponent<PlayerController>().DominoCountText.text = GameObject.Find("Spawner").GetComponent<Spawner>().CellsCountSpawn.Count.ToString();
    //}
    //SaveFile = SaveFileTest;
    //GameManager.GState = GameManager.GameState.NeedSet;
    //}

    //void OnGUI()
    //{
    //    /* fps display */
    //    int w = Screen.width, h = Screen.height;

    //    GUIStyle style = new GUIStyle();

    //    Rect rect = new Rect(0, 10, w, h * 2 / 100);
    //    style.alignment = TextAnchor.UpperLeft;
    //    style.fontSize = h * 1 / 120;
    //    style.normal.textColor = new Color(0.1f, 0.1f, 0.1f, 1.0f);
    //    GUI.Label(rect, Debinfo, style);
    //}
    //public void DebugLogF()
    //{
    //    Debinfo = "";


    //    if(SaveFileTest != null)
    //    {
    //        Debinfo += "\n GRGRGRGR";
    //        foreach (Vector3 discardTiles in SaveFileTest.DiscardTiles)
    //        {
    //            Debinfo += "(" + discardTiles.x + " - " + discardTiles.y + " - " + discardTiles.z + ")";
    //        }
    //        Debinfo += "\nNNNNNNN";
    //        Debinfo += "\n";
    //        foreach (TileInHand discardTiles in SaveFileTest.HandTiles)
    //        {
    //            Debinfo += "(" + discardTiles.posinWall.x + "-" + discardTiles.posinWall.y + "-" + discardTiles.posinWall.z + ")";
    //            Debinfo += "\n";
    //        }
    //    }


    //    Debinfo += "\n";
    //}

    private void Update()
    {
       // DebugLogF();


        if (Input.GetKeyDown(KeyCode.T))
        {
            SaveFileTest = SaveFile;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            GameManager.LoadNewGame();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            LoadSave();
        }
    }

    public static void CreateSave()
    {
        SaveReading = true;
    }
    public static void RestartGame()
    {
        if (ResolutionControl.isFullScreen)
        {
            DeleteSave();
            instscene = true;
        }
    }

    public static void DeleteSave()
    {
        instdel = true;
        SaveReading = false;
    }
}
