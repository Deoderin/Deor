using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject cellcontrolPrefab;
    public Vector3[] CenterPosition;
    public Vector3[] OutSidePosition;
    public GameObject rules;
    public static GameObject rulesPrefab;
    public GameObject Players;
    public static GameObject PlayersS;
    public const int playerToStart = 2;
    public GameObject spawner;
    public static GameObject spawnerS;
    public static GameObject CurrendDraggingCell;

    public List<GameObject> PlayersList = new List<GameObject>();
    public Vector3[] RulePos;



    public GameObject EndGameMenu;

    public GameObject EndGameMenuContainer;
    public GameObject ResultMenuPrefab;

    public GameObject startDark;


    public GameObject AlertPrefab;
    public static GameObject AlertPrefabS;

    public GameObject AlertLoadPrefab;
    public static GameObject AlertLoadPrefabS;
    public AnimationCurve BoxPositionScale;

    public Vector4[] CellControllPos;
    public static bool[] BOrderDominoRotate = new bool[2];
    public static Vector2 BorderDirectionMode;
    public static Vector2 BOrderDominoValue;
    public static GameObject BOrderDominoObjLeft;
    public static GameObject BOrderDominoObjRight;
    public GameObject MainCam;
    public GameObject[] BoxWithScore;

    public GameObject AlertROund;

    public enum GameState
    {
        StartGame,
        Drag,
        Steel,
        RoundResults,
        ScoreShower,
    }
    public static int ActivePlayerIndex = 0;
    public static int ActivePlayerID = -1;
    public static GameState GState;
    public GameObject Box;
    public static GameManager GM;
    public static GameObject BoxS;
    public static Vector3[] OutSidePositionS;

    public int DiscardPlayerId;


    public GameObject[] ScoreSticks;

    public GameObject AlarmPrefab;
    public GameObject Mahjong;
    public static int Round = 0;
    // Start is called before the first frame update
    void Start()
    {
        Saveloader.SaveFile.RoundNumber = Round;
        BOrderDominoRotate = new bool[2];
        BOrderDominoValue = Vector2.zero;
        AlertLoadPrefabS = this.AlertLoadPrefab;
        AlertPrefabS = this.AlertPrefab;
        GM = this;
        OutSidePositionS = this.OutSidePosition;
        spawnerS = this.spawner;
        BoxS = this.Box;
        rulesPrefab = this.rules;
        PlayersS = this.Players;
        GState = GameState.StartGame;
        CurrendDraggingCell = null;
    }

    public void Alarm(string text, int player)
    {
        if (GameObject.Find("Alarm(Clone)") == null)
        {
            Vector3 RulePos = GM.RulePos[player];
            int rot = 0;
            if (player == 1 || player == 2) rot = 180;
            Instantiate(AlarmPrefab, GameManager.GM.MainCam.GetComponent<Camera>().WorldToScreenPoint(BoxWithScore[player].transform.position), Quaternion.Euler(0, 0, rot), GameObject.FindGameObjectWithTag("Canvas").transform).transform.GetChild(1).GetComponent<Text>().text = text;
        }

    }

    readonly int[] Stickscore = new int[] { 1000, 500, 100, 10 };
    public void UpdateScoreSticks()
    {
        var counters = GameObject.FindGameObjectsWithTag("Counter");
        for (int i = 0; i < counters.Length; i++)
        {
            Destroy(counters[i]);
        }
      
        for (int i = 0; i < PlayersS.transform.childCount; i++)
        {
            PlayerController PC = PlayersS.transform.GetChild(i).GetComponent<PlayerController>();
            int score = PC.WinScore;
            int scorestype = 0;
            while (score > 0)
            {
                if (score >= Stickscore[scorestype])
                {
                    score -= Stickscore[scorestype];
                
                    GameObject counter = Instantiate(ScoreSticks[scorestype], BoxWithScore[PC.id].transform.position + new Vector3(Random.Range(-0.5f, 0.5f), (3 - scorestype) / 10, Random.Range(-0.3f, 0.3f)), Quaternion.Euler(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)), BoxWithScore[PC.id].transform);
                    counter.GetComponent<Rigidbody>().centerOfMass = new Vector3(0, -0.0085f, 0);

                }
                else
                {
                    scorestype++;
                }
            }
        }

    }


    public static void LoadNewGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        
    }

    public IEnumerator StartAlert(int player)
    {
        yield return new WaitForSeconds(0.6f);
        if (GameObject.Find("Alert" + player) == null)
        {
            Vector3 RulePos = GM.RulePos[player];


            GameObject rl = Instantiate(AlertPrefabS, new Vector3(RulePos.x, RulePos.z, 0), Quaternion.Euler(0, 0, RulePos.y), GameObject.FindGameObjectWithTag("Canvas").transform);
            rl.name = "Alert" + player;
        }
    }

    public static void StartAlertLoadGame(int player)
    {
        if (GameObject.Find("AlertLoad" + player) == null && PlayersS.transform.childCount == 1)
        {
            Vector3 RulePos = GM.RulePos[player];
            GameObject rl = Instantiate(AlertLoadPrefabS, new Vector3(RulePos.x, RulePos.z, 0), Quaternion.Euler(0, 0, RulePos.y), GameObject.FindGameObjectWithTag("Canvas").transform);
            rl.name = "AlertLoad" + player;
        }
    }

    public void SpawnAlert(int player)
    {
        StartCoroutine(StartAlert(player));
    }
    public static void LoadNewGameAnimation()
    {
        activateDark = true;
    }
    public static bool activateDark = false;

    public void LoadNewGameDark()
    {
        startDark.GetComponent<Animator>().SetTrigger("Restart");
        activateDark = false;
    }

    public static int NEXTEAST;

    public static void StartGame(int index, int east)
    {
        SortPlayers();
        SetActiveIndex(index, east);

        GM.StartHCoroutine(ActivePlayerIndex, -1);
        GState = GameState.Drag;
       if(Round == 0) GM.UpdateScoreSticks();
        
    }

    public static int GetIndexPlayerByID(int id)
    {
        for (int i = 0; i < PlayersS.transform.childCount; i++)
        {
            int ids = PlayersS.transform.GetChild(i).GetComponent<PlayerController>().id;
            if (ids == id)
            {
                return i;
            }
        }
        return -1;
    }

    public static void SetActiveIndex(int index,int EastIndex)
    {
        GM.RoundLoader.GetComponent<NewRound>().startindex = index;
        ActivePlayerIndex = index;
        SetSides(EastIndex);
        ActivePlayerID = PlayersS.transform.GetChild(ActivePlayerIndex).GetComponent<PlayerController>().id;
        GM.BoxWithScore[ActivePlayerID].GetComponent<Animator>().SetBool("turn", true);
        PlayersS.transform.GetChild(ActivePlayerIndex).GetComponent<PlayerController>().turnIndicator.GetComponent<Animator>().SetBool("show", true);
    }

    public static void SetSides(int eastindex)
    {

        PlayerController PC = PlayersS.transform.GetChild(eastindex).GetComponent<PlayerController>();
       
        PC.PSide = PlayerController.PlayerSide.East;
        Saveloader.SaveFile.East = eastindex;
        int index2 = eastindex - 1;
        if (index2 < 0) index2 += 4;
        PC = PlayersS.transform.GetChild(index2).GetComponent<PlayerController>();
        PC.PSide = PlayerController.PlayerSide.South;
       ;
        int index3 = eastindex - 2;
        if (index3 < 0) index3 += 4;
        PC = PlayersS.transform.GetChild(index3).GetComponent<PlayerController>();
        PC.PSide = PlayerController.PlayerSide.West;
      
        int index4 = eastindex - 3;
        if (index4 < 0) index4 += 4;
        PC = PlayersS.transform.GetChild(index4).GetComponent<PlayerController>();
        PC.PSide = PlayerController.PlayerSide.North;
     
    }

    public static void SortPlayers()
    {
        int i = 0;
        int playerindex = 0;
        while (true)
        {
            Transform pl = PlayersS.transform.Find("Player" + playerindex);
            if (pl != null)
            {
                pl.SetSiblingIndex(i);
                i++;
            }
            playerindex++;
            if (playerindex == 3) break;
        }
    }
    public static void SpawnRules(int player, PlayerController PC)
    {
        if (GameObject.Find("Rule" + player) == null)
        {
            Vector3 RulePos = GM.RulePos[player];
            GameObject rl = Instantiate(rulesPrefab, new Vector3(RulePos.x, RulePos.z, 0), Quaternion.Euler(0, 0, RulePos.y), GameObject.FindGameObjectWithTag("Canvas").transform);
            rl.GetComponent<RuleController>().PC = PC;
            if (player == 1 || player == 2)
            {
                rl.transform.GetChild(1).transform.localPosition = new Vector3(-rl.transform.GetChild(1).transform.localPosition.x, rl.transform.GetChild(1).transform.localPosition.y, rl.transform.GetChild(1).transform.localPosition.z);
            }
            rl.name = "Rule" + player;
        }
    }


    public void StartHCoroutine(int i, int previndex)
    {
        StartCoroutine(Highlight(i, previndex));
    }
    public static IEnumerator Highlight(int index, int previndex)
    {
        yield return new WaitForSeconds(0.6f);
       
        if (previndex != -1)
        {
            if (PlayersS.transform.GetChild(previndex).GetComponent<PlayerController>().DominoShower.GetComponent<DominoShower>().activate)
            {
            
                PlayersS.transform.GetChild(previndex).GetComponent<PlayerController>().CheckCombiantion();
            }
           
        }

    }

    public void GameEnd()
    {
        //  Saveloader.SaveFile.Cells.Clear();


    }

    public GameObject RoundLoader;
    public const int WinValue = 101;
    public void NextRound(int WinnerID, bool WinTileFromWall, bool lasttileWithThisValueOnWAll, bool lastTileOneDot, bool draw)
    {
        Saveloader.PushSaveInfo();
        Round++;
        Saveloader.SaveFile.RoundNumber = Round;
        
         Saveloader.SaveFile.WallSpawn.Clear();
        Saveloader.SaveFile.DiscardTiles.Clear();
        // Saveloader.SaveFile.DominoInHands.Clear();
        // Saveloader.SaveFile.DominoOnBoard.Clear();
        Saveloader.SaveFile.RoundLoad = true;
        GState = GameState.RoundResults;
        //подсчет очков


        if (!draw)
        {
            int WinnerIndex = GetIndexPlayerByID(WinnerID);


            for (int i = 0; i < PlayersS.transform.childCount; i++)
            {
                PlayersS.transform.GetChild(i).GetComponent<PlayerController>().CountScore((WinnerIndex == i), WinTileFromWall, lasttileWithThisValueOnWAll, lastTileOneDot);

                //  scores.Add(new Vector2(i, PlayersS.transform.GetChild(i).GetComponent<PlayerController>().Score));
            }

            List<PlayerController> LosePlayers = new List<PlayerController>();
            for (int i = 0; i < PlayersS.transform.childCount; i++)
            {
                if (WinnerIndex != i)
                {
                    PlayerController PC = PlayersS.transform.GetChild(i).GetComponent<PlayerController>();
                    LosePlayers.Add(PC);
                    int wincoef = 1;
                    int losecoef = 1;
                    if (PlayersS.transform.GetChild(WinnerIndex).GetComponent<PlayerController>().PSide == PlayerController.PlayerSide.East) wincoef = 2;
                    if (PC.PSide == PlayerController.PlayerSide.East) losecoef = 2;

                    int DeletedScore = PlayersS.transform.GetChild(WinnerIndex).GetComponent<PlayerController>().Score * wincoef * losecoef;
                    if (DeletedScore > PC.WinScore)
                    {
                        DeletedScore = PC.WinScore;
                    }
                    PC.looserAlertROundFinall = "";
                    PC.looserAlertROundFinall += "\n";

                    PC.looserAlertROundFinall += Localization.LoseText + DeletedScore + Localization.Scores + Localization.Players[PlayersS.transform.GetChild(WinnerIndex).GetComponent<PlayerController>().id]+".";
                    PlayersS.transform.GetChild(WinnerIndex).GetComponent<PlayerController>().WinScore += DeletedScore;

                    PC.WinScore -= DeletedScore;
                }
            }
            Vector2[] indexScoreCombinations = new Vector2[] { new Vector2(0, 1), new Vector2(0, 2), new Vector2(1, 2) };

            for (int i = 0; i < indexScoreCombinations.Length; i++)
            {
                int first = (int)indexScoreCombinations[i].x;
                int second = (int)indexScoreCombinations[i].y;

                if (LosePlayers[first].Score > LosePlayers[second].Score) PayScore(LosePlayers[first], LosePlayers[second]);
                else if (LosePlayers[first].Score < LosePlayers[second].Score) PayScore(LosePlayers[second], LosePlayers[first]);
            }

            if (PlayersS.transform.GetChild(WinnerIndex).GetComponent<PlayerController>().PSide == PlayerController.PlayerSide.East)
            {
                RoundLoader.GetComponent<NewRound>().startindex = WinnerIndex;
            }
            else
            {
                int newIndex = WinnerIndex - 1;
                if (newIndex < 0) newIndex = 3;
                RoundLoader.GetComponent<NewRound>().startindex = newIndex;
            }
        }
   
        //загружать анимацию маджонга
        // LoadROundEndAnimation
        
        GameObject mahjong = Instantiate(Mahjong,new Vector3(Screen.width/2,Screen.height/2,0),Quaternion.Euler(0,0, (WinnerID == 1 || WinnerID == 2) ? 180 : 0), GameObject.FindGameObjectWithTag("Canvas").transform);
        mahjong.GetComponent<Animator>().SetBool("invert", (WinnerID == 1 || WinnerID == 3));
        mahjong.transform.GetChild(0).transform.localPosition = (WinnerID == 1 || WinnerID == 3) ? new Vector3(1327.8f, -596,0) : new Vector3(-1327.8f, -596, 0);
        mahjong.GetComponent<MahjongAnimation>().WinID = WinnerID;
        foreach (GameObject set in GameObject.FindGameObjectsWithTag("Set"))
        {
            set.GetComponent<Animator>().SetTrigger("end");
        }

     Saveloader.PushSaveInfo();
    }


    public void LoadROundEndAnimation(int WinnerID)
    {

        // меню результатов игры
        List<Vector2> scores = new List<Vector2>() { };
        for (int i = 0; i < PlayersS.transform.childCount; i++)
        {
            scores.Add(new Vector2(i, PlayersS.transform.GetChild(i).GetComponent<PlayerController>().WinScore));
        }
        scores = scores.OrderBy(x => x.y).ToList();

        for (int i = 0; i < PlayersS.transform.childCount; i++)
        {
            PlayersS.transform.GetChild(i).GetComponent<PlayerController>().turnIndicator.GetComponent<Animator>().SetBool("show", false);
        }

        for (int i = 0; i < PlayersS.transform.childCount; i++)
        {

            int id = PlayersS.transform.GetChild(i).GetComponent<PlayerController>().id;

            Vector3 RulePos = GM.RulePos[id];
            int rot = 0;
            if (id == 1 || id == 2) rot = 180;
            GameObject rl = Instantiate(AlertROund, GameManager.GM.MainCam.GetComponent<Camera>().WorldToScreenPoint(BoxWithScore[id].transform.position), Quaternion.Euler(0, 0, rot), GameObject.FindGameObjectWithTag("Canvas").transform);
            rl.name = "Alert" + id;
            rl.GetComponent<RoundEndAlarmController>().pc = PlayersS.transform.GetChild(i).GetComponent<PlayerController>();
            rl.GetComponent<RoundEndAlarmController>().Score = (int)PlayersS.transform.GetChild(i).GetComponent<PlayerController>().Score;
            rl.GetComponent<RoundEndAlarmController>().WinplayerIndex = (int)scores[0].x;
            rl.GetComponent<RoundEndAlarmController>().east = PlayersS.transform.GetChild(i).GetComponent<PlayerController>().PSide == PlayerController.PlayerSide.East;
            rl.GetComponent<RoundEndAlarmController>().EndScore = PlayersS.transform.GetChild(i).GetComponent<PlayerController>().Score;



            if (id == WinnerID)
            {
                rl.GetComponent<RoundEndAlarmController>().winner = true;
            }
            else
            {
                rl.GetComponent<RoundEndAlarmController>().looser = true;
            }
        }

    }


    public void PayScore(PlayerController winner, PlayerController looser)
    {
        int DeletedScore = winner.Score - looser.Score;
        if (DeletedScore > looser.WinScore)
        {
            DeletedScore = looser.WinScore;
        }

        winner.looserAlertROundFinall += "\n";
        winner.looserAlertROundFinall += Localization.Also + Localization.LoseTextGet + DeletedScore + Localization.Scores + Localization.Player[PlayersS.transform.GetChild(looser.id).GetComponent<PlayerController>().id] + ".";



        looser.looserAlertROundFinall += "\n";
        looser.looserAlertROundFinall += Localization.Also + Localization.LoseText + DeletedScore + Localization.Scores + Localization.Players[PlayersS.transform.GetChild(winner.id).GetComponent<PlayerController>().id] + ".";
       
        winner.WinScore += DeletedScore;
        looser.WinScore -= DeletedScore;
    }


    public void GameEndAnimation()
    {
        Saveloader.PushSaveInfo();
        Round = 0;
        Saveloader.SaveFile.RoundNumber = Round;
        GameObject endGame = Instantiate(EndGameMenu, GameObject.FindGameObjectWithTag("Canvas").transform);
        EndGameMenuContainer = endGame;
        List<Vector2> scores = new List<Vector2>() { };
        for (int i = 0; i < PlayersS.transform.childCount; i++)
        {
            scores.Add(new Vector2(i, PlayersS.transform.GetChild(i).GetComponent<PlayerController>().WinScore));
        }
        scores = scores.OrderByDescending(x => x.y).ToList();
        for (int i = 0; i < PlayersS.transform.childCount; i++)
        {

            Vector3 RulePosirion = RulePos[PlayersS.transform.GetChild(i).GetComponent<PlayerController>().id];
            GameObject resObj = Instantiate(ResultMenuPrefab, new Vector3(RulePosirion.x, RulePosirion.z, 0), Quaternion.Euler(0, 0, RulePosirion.y), endGame.transform.GetChild(0));
            resObj.GetComponent<ResultPanel>().indexPlayer = i;
            resObj.GetComponent<ResultPanel>().place = scores.ToArray();
        }

        foreach (GameObject hud in GameObject.FindGameObjectsWithTag("Hud"))
        {
            hud.GetComponent<Animator>().SetTrigger("end");
        }
        Saveloader.SaveFile.GameIsActual = false;
        Saveloader.SaveFile.WallSpawn.Clear();
        Saveloader.SaveFile.DiscardTiles.Clear();
        // Saveloader.SaveFile.DominoOnBoard.Clear();
        
    }
    public static void nextTurn(bool picktile, int SetCustomIndex)
    {
        Saveloader.PushSaveInfo();
        //  PlayersS.transform.GetChild(ActivePlayerIndex).GetComponent<PlayerController>().DisableHighlight();
        PlayersS.transform.GetChild(ActivePlayerIndex).GetComponent<PlayerController>().turnIndicator.GetComponent<Animator>().SetBool("show", false);
        PlayersS.transform.GetChild(ActivePlayerIndex).GetComponent<PlayerController>().ScoreBox.GetComponent<Animator>().SetBool("turn", false);
        PlayersS.transform.GetChild(ActivePlayerIndex).GetComponent<PlayerController>().DeleteSetShower();
        int previndex = ActivePlayerIndex + 0;
        if (SetCustomIndex == -1)
        {
            ActivePlayerIndex--;
            if (ActivePlayerIndex < 0) ActivePlayerIndex = (PlayersS.transform.childCount - 1);
        }
        else
        {
            ActivePlayerIndex = SetCustomIndex;
        }


        PlayerController PC = PlayersS.transform.GetChild(ActivePlayerIndex).GetComponent<PlayerController>();
        ActivePlayerID = PC.id;
        PC.turnIndicator.GetComponent<Animator>().SetBool("show", true);
        PC.ScoreBox.GetComponent<Animator>().SetBool("turn", true);
        PC.DeleteSetShower();

        if (PC.OneOff)
        {
            for (int i = 0; i < PlayersS.transform.childCount; i++)
            {
                if (i != ActivePlayerIndex)
                {
                    PlayerController PCfinder = PlayersS.transform.GetChild(i).GetComponent<PlayerController>();
                    var selSetS = PCfinder.OpenCombinations.Where(p => p.HasOpenSteelKong2 == true).ToList();
                    foreach (PlayerController.CombinationsInfo selSet in selSetS)
                    {
                        CellDragging CD = selSet.CombinationsFirstTile.GetComponent<CellDragging>();
                        if (CellDragging.CheckDisSet(CD, ActivePlayerIndex))
                        {
                            CD.State = CellDragging.DominoState.DiscardNow;
                            GameObject s = Instantiate(CD.TimerPrefab, GameObject.FindGameObjectWithTag("Canvas").transform);
                            CD.transform.GetChild(0).GetComponent<Animator>().SetBool("active", true);
                            s.GetComponent<DiscardTimer>().tile = CD.gameObject;
                            s.GetComponent<DiscardTimer>().MahjongSteel = true;
                            // стиляем плиточку
                            return;
                        }
                    }
                }
            }
        }


        if (picktile) spawnerS.GetComponent<Spawner>().AddTile(ActivePlayerIndex);
        GM.StartHCoroutine(ActivePlayerIndex, previndex);

        Saveloader.PushSaveInfo();

    }

    public static void UpdateTileInfo()
    {
        Saveloader.SaveFile.HandTiles.Clear();
        Saveloader.SaveFile.OpenTileFinderSet.Clear();
        for (int i = 0; i < PlayersS.transform.childCount; i++)
        {
            PlayerController PC = PlayersS.transform.GetChild(i).GetComponent<PlayerController>();
            PC.GetAllTilesInList();
            for (int j = 0; j < PC.AllTilesInHand.Count; j++)
            {
                CellDragging CD = PC.AllTilesInHand[j].GetComponent<CellDragging>();
                Saveloader.SaveFile.HandTiles.Add(new Saveloader.TileInHand(PC.id, CD.PosInWall, CD.PosInHand));
            }
            for (int j = 0; j < PC.OpenCombinations.Count; j++)
            {
                Vector3 posInWall = PC.OpenCombinations[j].CombinationsFirstTile.GetComponent<CellDragging>().PosInWall;
                Saveloader.SaveFile.OpenTileFinderSet.Add(new Vector4(posInWall.x, posInWall.y, posInWall.z, PC.id));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GState != GameState.StartGame && BOrderDominoObjLeft != null && BOrderDominoObjRight != null)
        {
            BOrderDominoValue = new Vector2(BOrderDominoObjLeft.GetComponent<DominoPartInfo>().value, BOrderDominoObjRight.GetComponent<DominoPartInfo>().value);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {

            NextRound(0, false, false, false, false);


        }


      //  UpdateTileInfo();


        bool agame = (GState == GameState.StartGame || GState == GameState.ScoreShower);
        Saveloader.SaveFile.GameIsActual = !agame;
        Saveloader.SaveFile.activeindex = ActivePlayerIndex;

        if (activateDark) LoadNewGameDark();


    }
}
