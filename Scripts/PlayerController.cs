using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    public int id;

    public Color playerColor;

    public Color meepleColor;
    public GameObject[] meeplesPrefab;
    public Vector3 MeepleDistance;

    public GameObject[] meeples;
    private const int MeppleSpawnCount = 7;
    public int Score = 0;
    public int WinScore = 0;
    public float MeepleRot;

    public GameObject HUD;

    public GameObject DominoShowerPrefab;
    public GameObject DominoShower;

    public Text ScoreText;
    public Text DominoCountText;
    public Vector3 CurPos;

    public Vector3 SpawnPos;
    public Vector3[] PlayerDelta;
    //public static bool CurrentOldSession = false;

    public Vector3[] MeeplePos = new Vector3[7];
    public GameObject selectorEffectPrefab;
    public bool spawned = false;
    public int MeepleCount;
    public bool loadSave = false;
    public bool firstplayer;
    public Vector3 HUdPosition;
    public GameObject turnIndicator;
    public int InvertedShowerButton = 1;
    public Texture[] SideIndicator;
    public GameObject ScoreBox;


    public GameObject LastPickedTile;

    public GameObject SetIndecatorPrefab;
    public enum PlayerSide
    {
        East = 0,
        South = 1,
        West = 2,
        North = 3,
    }
    public PlayerSide PSide;


    public void StartNewROund()
    {
        LastOpenSetTilesSort = 0;
        OpenCombinations.Clear();
        CombinationsI.Clear();
        Combinations.Clear();
        TilesInHand.Clear();
        OneOff = false;
        HasKongSteel = false;
       // ScoreUpdate(WinScore);
    }

    public string looserAlertROundFinall;
    // Start is called before the first frame update
    void Start()
    {
        ScoreBox = GameObject.Find("SM_ScoreCase " + id);
        InvertedShowerButton = 1;
        ScoreText = HUD.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>();
        if (id == 1 || id == 3)
        {
            ScoreText.transform.localPosition = new Vector3(-ScoreText.transform.localPosition.x, ScoreText.transform.localPosition.y, ScoreText.transform.localPosition.z);
            InvertedShowerButton = -1;
        }
        DominoCountText = HUD.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>();
        if (id == 1 || id == 3)
        {
            DominoCountText.transform.localPosition = new Vector3(-DominoCountText.transform.localPosition.x, DominoCountText.transform.localPosition.y, DominoCountText.transform.localPosition.z);
        }
        DominoCountText.text = GameObject.Find("Spawner").GetComponent<Spawner>().CellsCountSpawn.Count.ToString();
        ScoreText.text = WinScore.ToString();
        Saveloader.SaveFile.PlayerScore[id] = WinScore;
        SpawnPos = transform.position;
        CurPos = SpawnPos;
        DominoShower = Instantiate(DominoShowerPrefab, transform);
        DominoShower.transform.SetAsFirstSibling();
        transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.mainTexture = SideIndicator[(int)PSide];
        ScoreUpdate(WinScore);
    }


    public void CountScore(bool winner, bool winTileFromWall, bool lasttileWithThisValueOnWAll, bool lastTileOneDot)
    {
        Score = 0;


        int windSets = 0;
        int windPair = 0;
        int dragonSets = 0;
        int dragonPair = 0;


        int[] dragonSet = new int[3];

        int[] windSet = new int[4];

        foreach (CombinationsInfo comb in OpenCombinations)
        {
            CellDragging CD = comb.CombinationsFirstTile.GetComponent<CellDragging>();
            switch (comb.Combinations)
            {
                case SetType.Kong:
                    if ((int)CD.ScoreInCells.x <= 2)
                    {
                        //Конг на мастях
                        if ((int)CD.ScoreInCells.y >= 2 && (int)CD.ScoreInCells.y <= 8)
                        {
                            Score += 8;
                        }
                        //Конг терминальных
                        else
                        {
                            Score += 16;
                        }
                    }
                    else
                    {
                        //Конг ветров
                        if ((int)CD.ScoreInCells.x >= 3 && (int)CD.ScoreInCells.x <= 6)
                        {
                            int scoreMulty = 16;
                            //Конг своих ветров
                            if ((int)CD.ScoreInCells.x == ((int)PSide + 3))
                            {
                                scoreMulty *= 2;
                            }
                            //Конг ветров раунда
                            if ((int)CD.ScoreInCells.x == (Mathf.Floor(GameManager.Round / 2) + 3))
                            {
                                scoreMulty *= 2;
                            }
                            windSet[(int)CD.ScoreInCells.x - 3]++;
                            Score += scoreMulty;
                            windSets++;
                        }
                        //Конг драконов
                        if ((int)CD.ScoreInCells.x >= 7)
                        {
                            dragonSets++;
                            dragonSet[(int)CD.ScoreInCells.x - 7]++;
                            Score += 16 * 2;
                        }
                    }
                    break;
                case SetType.Pong:
           
                    if ((int)CD.ScoreInCells.x <= 2)
                    {
                        //Панг на мастях
                        if ((int)CD.ScoreInCells.y >= 2 && (int)CD.ScoreInCells.y <= 8)
                        {
                        
                            Score += 2;
                        }
                        //Панг терминальных
                        else
                        {
                        
                            Score += 4;
                        }
                    }
                    else
                    {
                      
                        //Панг ветров
                        if ((int)CD.ScoreInCells.x >= 3 && (int)CD.ScoreInCells.x <= 6)
                        {
                       
                            windSets++;
                            int scoreMulty = 4;
                            //Панг своих ветров
                            if ((int)CD.ScoreInCells.x == ((int)PSide + 3))
                            {
                                scoreMulty *= 2;
                            }
                            //Панг ветров раунда
                            if ((int)CD.ScoreInCells.x == (Mathf.Floor(GameManager.Round / 2) + 3))
                            {
                                scoreMulty *= 2;
                            }
                            windSet[(int)CD.ScoreInCells.x - 3]++;
                            Score += scoreMulty;
                        }
                        //Панг драконов
                        if ((int)CD.ScoreInCells.x >= 7)
                        {
                        
                            dragonSet[(int)CD.ScoreInCells.x - 7]++;
                            dragonSets++;
                            Score += 4 * 2;
                        }
                    }

                    break;

            }
        }
        foreach (CombinationsInfo comb in CombinationsI)
        {
            CellDragging CD = comb.CombinationsFirstTile.GetComponent<CellDragging>();
            switch (comb.Combinations)
            {
                case SetType.Kong:
                    if ((int)CD.ScoreInCells.x <= 2)
                    {
                        //Конг на мастях
                        if ((int)CD.ScoreInCells.y >= 2 && (int)CD.ScoreInCells.y <= 8)
                        {
                            Score += 16;
                        }
                        //Конг терминальных
                        else
                        {
                            Score += 32;
                        }
                    }
                    else
                    {
                        //Конг ветров
                        if ((int)CD.ScoreInCells.x >= 3 && (int)CD.ScoreInCells.x <= 6)
                        {
                            windSets++;
                            int scoreMulty = 32;
                            //Конг своих ветров
                            if ((int)CD.ScoreInCells.x == ((int)PSide + 3))
                            {
                                scoreMulty *= 2;
                            }
                            //Конг своих ветров
                            if ((int)CD.ScoreInCells.x == (Mathf.Floor(GameManager.Round / 2) + 3))
                            {
                                scoreMulty *= 2;
                            }
                            Score += scoreMulty;
                            windSet[(int)CD.ScoreInCells.x - 3]++;
                        }
                        //Конг драконов
                        if ((int)CD.ScoreInCells.x >= 7)
                        {
                            dragonSet[(int)CD.ScoreInCells.x - 7]++;
                            dragonSets++;
                            Score += 32 * 2;
                        }
                    }
                    break;
                case SetType.Pong:
                    if ((int)CD.ScoreInCells.x <= 2)
                    {
                        //Панг на мастях
                        if ((int)CD.ScoreInCells.y >= 2 && (int)CD.ScoreInCells.y <= 8)
                        {
                            Score += 4;
                        }
                        //Панг терминальных
                        else
                        {
                            Score += 8;
                        }
                    }
                    else
                    {
                        //Панг ветров
                        if ((int)CD.ScoreInCells.x >= 3 && (int)CD.ScoreInCells.x <= 6)
                        {
                            windSet[(int)CD.ScoreInCells.x - 3]++;
                            windSets++;
                            int scoreMulty = 8;
                            //Панг своих ветров
                            if ((int)CD.ScoreInCells.x == ((int)PSide + 3))
                            {
                                scoreMulty *= 2;
                            }
                            //Панг ветров раунда
                            if ((int)CD.ScoreInCells.x == (Mathf.Floor(GameManager.Round / 2) + 3))
                            {
                                scoreMulty *= 2;
                            }
                            Score += scoreMulty;
                        }
                        //Панг драконов
                        if ((int)CD.ScoreInCells.x >= 7)
                        {
                            dragonSet[(int)CD.ScoreInCells.x - 7]++;
                            dragonSets++;
                            Score += 8 * 2;
                        }
                    }
                    break;
                case SetType.Pair:
                    //пара своих ветров
                    if ((int)CD.ScoreInCells.x == ((int)PSide + 3))
                    {
                        windPair++;
                        Score += 2;
                    }
                    //пара ветров раунда
                    if ((int)CD.ScoreInCells.x == (Mathf.Floor(GameManager.Round / 2) + 3))
                    {
                        windPair++;
                        Score += 2;
                    }
                    //пара драконов
                    if ((int)CD.ScoreInCells.x >= 7)
                    {
                        dragonPair++;
                        Score += 2;
                    }
                    break;

            }
        }


        int bamboo = 0;
        int charac = 0;
        int dots = 0;
        int minor = 0;
        int notgreen = 0;
        int windsAndDrag = 0;

        int[] bambooTile = new int[2];
        int[] characTile = new int[2];
        int[] dotsTile = new int[2];

        int[] bambooTileMinor = new int[7];
        int[] characTileMinor = new int[7];
        int[] dotsTileMinor = new int[7];

        int[] dragonAndwindTile = new int[7];





        for (int i = 1; i < transform.childCount; i++)
        {
            CellDragging CelD = transform.GetChild(i).GetComponent<CellDragging>();

            if (!GameManager.spawnerS.GetComponent<Spawner>().GreenTilesValue.Contains(CelD.ScoreInCells)) notgreen++;


            switch ((int)CelD.ScoreInCells.x)
            {
                case 0:

                    if ((int)CelD.ScoreInCells.y == 1)
                    {
                        bambooTile[0]++;
                    }
                    else if ((int)CelD.ScoreInCells.y == 9)
                    {
                        bambooTile[1]++;
                    }
                    else
                    {
                        bambooTileMinor[(int)CelD.ScoreInCells.y - 2]++;
                        minor++;
                    }
                    bamboo++;
                    break;
                case 1:
                    if ((int)CelD.ScoreInCells.y == 1)
                    {
                        characTile[0]++;
                    }
                    else if ((int)CelD.ScoreInCells.y == 9)
                    {
                        characTile[1]++;
                    }
                    else
                    {
                        characTileMinor[(int)CelD.ScoreInCells.y - 2]++;
                        minor++;
                    }
                    charac++;
                    break;
                case 2:
                    if ((int)CelD.ScoreInCells.y == 1)
                    {
                        dotsTile[0]++;
                    }
                    else if ((int)CelD.ScoreInCells.y == 9)
                    {
                        dotsTile[1]++;
                    }
                    else
                    {
                        dotsTileMinor[(int)CelD.ScoreInCells.y - 2]++;
                        minor++;
                    }
                    dots++;
                    break;

            }
            if ((int)CelD.ScoreInCells.x >= 3)
            {
                dragonAndwindTile[(int)CelD.ScoreInCells.x - 3]++;
                windsAndDrag++;
            }
        }
        bool allOneTilesType = false;
        if (bamboo + charac + windsAndDrag == 0 || dots + charac + windsAndDrag == 0 || dots + bamboo + windsAndDrag == 0)
        {
            allOneTilesType = true;
            Score *= 8;
        }
        if (bamboo == 0 && charac == 0 && dots == 0 && windsAndDrag > 0)
        {
            Score *= 2;
        }




        if (winner)
        {
            Score += 20;
            if (winTileFromWall) Score += 2;
            if (Score == 22) Score *= 2;
            if (OpenCombinations.Where(x => x.Combinations != SetType.Pong).ToList().Count + CombinationsI.Where(x => x.Combinations != SetType.Pong).ToList().Count == 0)
            {
                Score *= 2;
            }
            if (GameManager.spawnerS.GetComponent<Spawner>().NextDeadWall())
            {
                Score *= 2;
            }
            if (HasKongSteel)
            {
                Score *= 2;
            }
            if (OpenCombinations.Count == 0)
            {
                Score *= 2;
            }
            if (OpenCombinations.Where(x => x.Combinations == SetType.Kong).ToList().Count + CombinationsI.Where(x => x.Combinations == SetType.Kong).ToList().Count >= 2)
            {
                Score *= 2;
            }
            if (GameManager.Round == 1)
            {
                Score *= 2;
            }
            if (minor == 0)
            {
                Score *= 2;
            }
            if (lasttileWithThisValueOnWAll)
            {
                Score *= 2;
            }
            if (windSets == 3 && windPair == 1)
            {
                Score *= 2;
            }
            if (dragonSets == 2 && dragonPair == 1)
            {
                Score *= 2;
            }

        }
        //проверка легендарныйх комбинаций
        if (OpenCombinations.Where(x => x.Combinations == SetType.Chow).ToList().Count + CombinationsI.Where(x => x.Combinations == SetType.Chow).ToList().Count == 0 && OpenCombinations.Count == 0 && allOneTilesType)
        {
            Debug.LogError("Найденное сокровище");
            Score = 500;
        }
        if (winner && ((bamboo + charac + dots) == 0))
        {
            Debug.LogError("Свита императора");
            Score = 500;
        }
        if (winner && windsAndDrag == 0 && minor == 0)
        {
            Debug.LogError("Головы и хвосты");
            Score = 500;
        }
        if (winner && OpenCombinations.Where(x => x.Combinations == SetType.Chow).ToList().Count + CombinationsI.Where(x => x.Combinations == SetType.Chow).ToList().Count == 0 && notgreen == 0)
        {
            Debug.LogError("Императорский нефрит");
            Score = 500;
        }
        if (winner && OpenCombinations.Where(x => x.Combinations == SetType.Chow).ToList().Count + CombinationsI.Where(x => x.Combinations == SetType.Chow).ToList().Count == 0 && dragonSet[0] > 0 && dragonSet[1] > 0 && dragonSet[2] > 0)
        {
            Debug.LogError("Трое великих ученых");
            Score = 500;
        }
        if (winner && OpenCombinations.Where(x => x.Combinations == SetType.Kong).ToList().Count + CombinationsI.Where(x => x.Combinations == SetType.Kong).ToList().Count == 4)
        {
            Debug.LogError("Четыре по четыре");
            Score = 500;
        }
        if (winner && windSet[0] > 0 && windSet[1] > 0 && windSet[2] > 0 && windSet[3] > 0)
        {
            Debug.LogError("Четыре наслаждения вошли в твою дверь");
            Score = 500;
        }

        int combination = 1;

        int[] Paircombination = new int[4];

        for (int i = 0; i < 2; i++)
        {
            if (bambooTile[i] == 2) Paircombination[0]++;
            if (characTile[i] == 2) Paircombination[1]++;
            if (dotsTile[i] == 2) Paircombination[2]++;
            combination *= bambooTile[i];
            combination *= characTile[i];
            combination *= dotsTile[i];
        }
        for (int i = 0; i < 7; i++)
        {
            if (bambooTileMinor[i] == 2) Paircombination[0]++;
            if (characTileMinor[i] == 2) Paircombination[1]++;
            if (dotsTileMinor[i] == 2) Paircombination[2]++;
            if (dragonAndwindTile[i] == 2) Paircombination[3]++;
            combination *= dragonAndwindTile[i];
        }

        if (combination > 0 && Mathf.Max(Mathf.Max(bambooTile.Max(), characTile.Max()), Mathf.Max(dotsTile.Max(), dragonAndwindTile.Max())) == 2)
        {
            Debug.LogError("Тринадцать чудес Света");
            Score = 500;
        }

        if (Paircombination.Max() >= 7)
        {
            Debug.LogError("Небесные близнецы");
            Score = 500;
        }

        if (lastTileOneDot)
        {
            Debug.LogError("Достать луну со дна моря");
            Score = 500;
        }
    
        //округляем до десятых
        Score = (int)Mathf.Ceil(Score / 10f) * 10;
    }

    public void ScoreUpdate(int score)
    {
        Saveloader.SaveFile.PlayerScore[id] = score;
        ScoreText.text = score.ToString();
    }
    public bool outSortRange = false;

    public void CheckCombiantion()
    {
        Combinations.Clear();
        CombinationsI.Clear();
        for (int i = 0; i < (TilesInHand.Count - 1); i++)
        {
            Vector2 StartCombination = TilesInHand[i].GetComponent<CellDragging>().ScoreInCells;
            Vector3 pos = transform.TransformPoint(Vector3.zero + ((Vector3.right * 0.36f) * (TilesInHand[i].GetComponent<CellDragging>().PosInHand)));
            List<Vector2> NextCombPart = new List<Vector2>();
            for (int j = 1; j < 4; j++)
            {
                if (i + j < TilesInHand.Count) NextCombPart.Add(TilesInHand[i + j].GetComponent<CellDragging>().ScoreInCells);
            }

            if (NextCombPart.Count > 0)
            {   // либо пара либо триплет либо квартет либо лесенка
                if (StartCombination.x == NextCombPart[0].x)
                {
                    // либо пара либо триплет либо квартет
                    if (StartCombination.y == NextCombPart[0].y)
                    {
                        if (NextCombPart.Count > 1)
                        {
                            // либо пара либо триплет либо квартет
                            if (StartCombination.x == NextCombPart[1].x)
                            {
                                // либо триплет либо квартет
                                if (StartCombination.y == NextCombPart[1].y)
                                {
                                    if (NextCombPart.Count > 2)
                                    {
                                        //либо квартет
                                        if (StartCombination.x == NextCombPart[2].x)
                                        {
                                            if (StartCombination.y == NextCombPart[2].y)
                                            {
                                                CombinationsI.Add(new CombinationsInfo(TilesInHand[i], SetType.Kong));
                                                CombinationsI[CombinationsI.Count - 1].OtherTiles.AddRange(new GameObject[] { TilesInHand[i + 1], TilesInHand[i + 2], TilesInHand[i + 3] });
                                                InitSetObjIndicator(i, pos, (int)SetType.Kong, id, TilesInHand[i].GetComponent<CellDragging>());
                                                i += 3;

                                                Combinations.Add(SetType.Kong);

                                                //квартет
                                            }
                                            else
                                            {
                                                CombinationsI.Add(new CombinationsInfo(TilesInHand[i], SetType.Pong));
                                                CombinationsI[CombinationsI.Count - 1].OtherTiles.AddRange(new GameObject[] { TilesInHand[i + 1], TilesInHand[i + 2] });
                                                InitSetObjIndicator(i, pos, (int)SetType.Pong, id, TilesInHand[i].GetComponent<CellDragging>());
                                                i += 2;

                                                Combinations.Add(SetType.Pong);
                                                //триплет
                                            }
                                        }
                                        else
                                        {

                                            CombinationsI.Add(new CombinationsInfo(TilesInHand[i], SetType.Pong));
                                            CombinationsI[CombinationsI.Count - 1].OtherTiles.AddRange(new GameObject[] { TilesInHand[i + 1], TilesInHand[i + 2] });
                                            InitSetObjIndicator(i, pos, (int)SetType.Pong, id, TilesInHand[i].GetComponent<CellDragging>());
                                            i += 2;

                                            Combinations.Add(SetType.Pong);
                                            //триплет
                                        }
                                    }
                                    else
                                    {

                                        CombinationsI.Add(new CombinationsInfo(TilesInHand[i], SetType.Pong));
                                        CombinationsI[CombinationsI.Count - 1].OtherTiles.AddRange(new GameObject[] { TilesInHand[i + 1], TilesInHand[i + 2] });
                                        InitSetObjIndicator(i, pos, (int)SetType.Pong, id, TilesInHand[i].GetComponent<CellDragging>());
                                        i += 2;

                                        Combinations.Add(SetType.Pong);
                                        //триплет
                                    }
                                }
                                else
                                {
                                    CombinationsI.Add(new CombinationsInfo(TilesInHand[i], SetType.Pair));
                                    InitSetObjIndicator(i, pos, (int)SetType.Pair, id, TilesInHand[i].GetComponent<CellDragging>());
                                    i++;

                                    Combinations.Add(SetType.Pair);
                                    //пара
                                }
                            }
                            else
                            {
                                CombinationsI.Add(new CombinationsInfo(TilesInHand[i], SetType.Pair));
                                InitSetObjIndicator(i, pos, (int)SetType.Pair, id, TilesInHand[i].GetComponent<CellDragging>());
                                i++;

                                Combinations.Add(SetType.Pair);
                                //пара
                            }
                        }
                        else
                        {
                            CombinationsI.Add(new CombinationsInfo(TilesInHand[i], SetType.Pair));
                            InitSetObjIndicator(i, pos, (int)SetType.Pair, id, TilesInHand[i].GetComponent<CellDragging>());
                            i++;

                            Combinations.Add(SetType.Pair);
                            //пара
                        }
                    }
                    // либо лесенка
                    else if (StartCombination.y == NextCombPart[0].y - 1)
                    {
                        if (NextCombPart.Count > 1)
                        {
                            // либо лесенка
                            if (StartCombination.x == NextCombPart[1].x && StartCombination.y == NextCombPart[1].y - 2)
                            {
                                CombinationsI.Add(new CombinationsInfo(TilesInHand[i], SetType.Chow));
                                CombinationsI[CombinationsI.Count - 1].OtherTiles.AddRange(new GameObject[] { TilesInHand[i + 1], TilesInHand[i + 2] });
                                InitSetObjIndicator(i, pos, (int)SetType.Chow, id, TilesInHand[i].GetComponent<CellDragging>());
                                Combinations.Add(SetType.Chow);
                                i += 2;
                                //лесенка
                            }
                        }
                    }
                }
            }
        }

        //поиск минилесниц
        for (int i = 0; i < (TilesInHand.Count - 1); i++)
        {
            Vector2 StartCombination = TilesInHand[i].GetComponent<CellDragging>().ScoreInCells;
            if (i + 1 < TilesInHand.Count)
            {
                Vector2 NextCombPart = TilesInHand[i + 1].GetComponent<CellDragging>().ScoreInCells;
                if (StartCombination.x == NextCombPart.x)
                {
                    if (StartCombination.y == NextCombPart.y - 1)
                    {
                        CombinationsI.Add(new CombinationsInfo(TilesInHand[i], SetType.miniChow));
                        Combinations.Add(SetType.miniChow);
                        //лесенка из двух
                    }
                    else
                    if (StartCombination.y == NextCombPart.y - 2)
                    {
                        CombinationsI.Add(new CombinationsInfo(TilesInHand[i], SetType.borderChow));
                        Combinations.Add(SetType.borderChow);
                        //лесенка из двух
                    }
                }
            }
        }
        GetMahjongComboInfo();
    }
    public enum SetType
    {
        //два одинаковых
        Pair = 0,
        //три подряд по счету
        Chow = 1,
        //три одинаковых
        Pong = 2,
        //четыре одинаковых
        Kong = 3,
        //два подряд по счету
        miniChow = 4,
        //не хватает одного по центру чтобы идти по счету
        borderChow = 5,
    }

    public void CheckDiscardSteelSetSave(GameObject steelTile, bool OpenKong3)
    {
        Debug.LogError("combC - " + CombinationsI.Count);
        foreach (CombinationsInfo info in CombinationsI)
        {
            if (info.Combinations == SetType.Chow || info.Combinations == SetType.Pong || info.Combinations == SetType.Kong)
            {
                if (info.CombinationsFirstTile == steelTile || info.OtherTiles.Contains(steelTile))
                {

                    OpenCombinations.Add(new CombinationsInfo(info.CombinationsFirstTile, info.Combinations));



                    OpenCombinations[OpenCombinations.Count - 1].SetIndicator = info.SetIndicator;
                    OpenCombinations[OpenCombinations.Count - 1].OtherTiles.AddRange(info.OtherTiles);


                    info.CombinationsFirstTile.transform.SetSiblingIndex(LastOpenSetTilesSort + 1);
                    CellDragging CD = info.CombinationsFirstTile.GetComponent<CellDragging>();

                    //переворачить конги
                    CD.OpenKongCloseTile = (info.Combinations == SetType.Kong);

                    info.OtherTiles[info.OtherTiles.Count - 1].GetComponent<CellDragging>().OpenKongCloseTile = ((info.Combinations == SetType.Kong) && OpenKong3);

                    CD.openSetPosIDPlace = LastOpenSetTilesSort;
                    CD.OpenSet = true;
                    int posinH = CD.openSetPosIDPlace - (transform.childCount - 1) / 2;

                    CD.PosInHand = posinH;
                    CD.transform.position = Vector3.zero + ((Vector3.right * 0.36f) * (posinH));
                    Vector3 pos = transform.TransformPoint(Vector3.zero + ((Vector3.right * 0.36f) * (posinH)));
                    CD.SC = info.SetIndicator.GetComponent<SetController>();
                    info.SetIndicator.GetComponent<SetController>().OpenSet = true;  
                    info.SetIndicator.transform.position = GameManager.GM.MainCam.GetComponent<Camera>().WorldToScreenPoint(pos);
                    LastOpenSetTilesSort++;

                    foreach (GameObject other in info.OtherTiles)
                    {
                        CellDragging CDO = other.GetComponent<CellDragging>();
                        other.transform.SetSiblingIndex(LastOpenSetTilesSort + 1);
                        CDO.OpenSet = true;
                        CDO.openSetPosIDPlace = LastOpenSetTilesSort;
                        int posinhand = CDO.openSetPosIDPlace - (transform.childCount - 1) / 2;
                        CDO.PosInHand = posinhand;
                        CDO.transform.position = Vector3.zero + ((Vector3.right * 0.36f) * (posinhand));
                        LastOpenSetTilesSort++;
                    }
                    if ((steelTile.GetComponent<CellDragging>().SteelFromKong))
                    {
                        HasKongSteel = true;
                    }
                    return;
                }
            }
        }
    }


    public int LastOpenSetTilesSort = 0;
    public bool HasKongSteel;
    public void CheckDiscardSteelSet(GameObject steelTile, bool OpenKong3)
    {
        Debug.LogError("combC - " + CombinationsI.Count);
        foreach (CombinationsInfo info in CombinationsI)
        {
            if (info.Combinations == SetType.Chow || info.Combinations == SetType.Pong || info.Combinations == SetType.Kong)
            {
                if (info.CombinationsFirstTile == steelTile || info.OtherTiles.Contains(steelTile))
                {

                    //также выложить комбо в открытую
                    if (info.Combinations == SetType.Kong)
                    {
                        GameManager.spawnerS.GetComponent<Spawner>().AddTile(GameManager.GetIndexPlayerByID(id));
                    }
                    OpenCombinations.Add(new CombinationsInfo(info.CombinationsFirstTile, info.Combinations));

                 

                    OpenCombinations[OpenCombinations.Count - 1].SetIndicator = info.SetIndicator;
                    OpenCombinations[OpenCombinations.Count - 1].OtherTiles.AddRange(info.OtherTiles);


                    info.CombinationsFirstTile.transform.SetSiblingIndex(LastOpenSetTilesSort + 1);
                    CellDragging CD = info.CombinationsFirstTile.GetComponent<CellDragging>();

                    //переворачить конги
                    CD.OpenKongCloseTile = (info.Combinations == SetType.Kong);

                    info.OtherTiles[info.OtherTiles.Count - 1].GetComponent<CellDragging>().OpenKongCloseTile = ((info.Combinations == SetType.Kong) && OpenKong3);

                    CD.openSetPosIDPlace = LastOpenSetTilesSort;
                    CD.OpenSet = true;
                    int posinH = CD.openSetPosIDPlace - (transform.childCount - 1) / 2;

                    CD.PosInHand = posinH;
                    Vector3 pos = transform.TransformPoint(Vector3.zero + ((Vector3.right * 0.36f) * (posinH)));
                    CD.SC = info.SetIndicator.GetComponent<SetController>();
                    info.SetIndicator.GetComponent<SetController>().OpenSet = true;
                    info.SetIndicator.transform.position = GameManager.GM.MainCam.GetComponent<Camera>().WorldToScreenPoint(pos);
                    LastOpenSetTilesSort++;

                    foreach (GameObject other in info.OtherTiles)
                    {
                        CellDragging CDO = other.GetComponent<CellDragging>();
                        other.transform.SetSiblingIndex(LastOpenSetTilesSort + 1);
                        CDO.OpenSet = true;
                        CDO.openSetPosIDPlace = LastOpenSetTilesSort;
                        CDO.PosInHand = CDO.openSetPosIDPlace - (transform.childCount - 1) / 2;
                        LastOpenSetTilesSort++;
                    }
                    if(!OpenKong3)
                    {
                        GameManager.GState = GameManager.GameState.Drag;
                        if (GameObject.Find("Alarm(Clone)") != null)
                        {
                            GameObject.Find("Alarm(Clone)").GetComponent<Animator>().SetTrigger("endalarm");
                        }
                        steelTile.transform.GetChild(0).GetComponent<Animator>().SetBool("active", false);
                        GameManager.nextTurn(false, GameManager.GetIndexPlayerByID(id));

                    }

                    if((steelTile.GetComponent<CellDragging>().SteelFromKong))
                    {
                        HasKongSteel = true;
                    }


                    
                    return;
                }
            }
        }
        GameManager.GM.Alarm(Localization.AlarmDiscardPutSet, id);
        
        //  GetMahjongComboInfo();
        //показывать уведомление о составлении набора
    }

    public bool CheckSteelSetFromDiscard(Vector2 DIC, bool nextPlayer)
    {

        foreach (CombinationsInfo info in CombinationsI)
        {
            Vector2 SC = info.CombinationsFirstTile.GetComponent<CellDragging>().ScoreInCells;
            switch (info.Combinations)
            {
                case SetType.Pair:
                    //  Debug.LogError(nextPlayer + " =zstart3 ");
                    //  Debug.LogError(SC);
                    //  Debug.LogError(DIC);
                    //  Debug.LogError("er3-" + (SC.x == DIC.x && SC.y == DIC.y));
                    if ((SC.x == DIC.x && SC.y == DIC.y)) return true;
                    break;
                case SetType.Pong:
                    if (SC.x == DIC.x && SC.y == DIC.y) return true;
                    break;
                case SetType.miniChow:
                    //  Debug.LogError(nextPlayer + " =zstart1 ");
                    //  Debug.LogError(SC);
                    // Debug.LogError(DIC);
                    // Debug.LogError("er1-" + (SC.x == DIC.x && SC.y == DIC.y + 1) + " - " + (SC.x == DIC.x && SC.y == DIC.y - 2));
                    if (SC.x == DIC.x && SC.y == DIC.y + 1 && nextPlayer || SC.x == DIC.x && SC.y == DIC.y - 2 && nextPlayer) return true;
                    break;
                case SetType.borderChow:
                    //  Debug.LogError(nextPlayer + " =zstart2 ");
                    // Debug.LogError(SC);
                    // Debug.LogError(DIC);
                    // Debug.LogError("er2 - " + (SC.x == DIC.x && SC.y == DIC.y - 1));
                    if (SC.x == DIC.x && SC.y == DIC.y - 1 && nextPlayer) return true;
                    break;
            }
        }
        return false;
    }

    public void InitSetObjIndicator(int index, Vector3 pos, int type, int playerID, CellDragging CD)
    {

        Vector3 posV = GameManager.GM.MainCam.GetComponent<Camera>().WorldToScreenPoint(pos);
        CombinationsI[CombinationsI.Count - 1].SetIndicator = Instantiate(SetIndecatorPrefab, posV, (id == 1 || id == 2) ? Quaternion.Euler(0, 0, 180) : Quaternion.identity, GameObject.Find("P" + playerID).transform);
        CombinationsI[CombinationsI.Count - 1].SetIndicator.GetComponent<SetController>().type = (SetController.SetType)type;
        CombinationsI[CombinationsI.Count - 1].SetIndicator.GetComponent<SetController>().Container = CD;
        // SC.invert = ;
    }


    public void DeleteSetShower()
    {
        Transform PCanvas = GameObject.Find("P" + id).transform;
        for (int i = 0; i < PCanvas.childCount; i++)
        {
            if (!PCanvas.GetChild(i).GetComponent<SetController>().OpenSet)
            {
                PCanvas.GetChild(i).GetComponent<Animator>().SetTrigger("end");
            }
        }
    }


    public class CombinationsInfo
    {
        public CombinationsInfo(GameObject tile, SetType type)
        {
            this.CombinationsFirstTile = tile;
            this.Combinations = type;
        }
        public CombinationsInfo()
        {

        }
        public List<GameObject> OtherTiles = new List<GameObject>();
        public GameObject CombinationsFirstTile;
        public GameObject SetIndicator;
        public bool HasOpenSteelKong2;
        public SetType Combinations;
    };

    public bool OneOff;
    public void GetMahjongComboInfo()
    {
        int setCount = CombinationsI.Where(x => x.Combinations == SetType.Kong || x.Combinations == SetType.Pong || x.Combinations == SetType.Chow).ToList().Count + OpenCombinations.Where(x => x.Combinations == SetType.Kong || x.Combinations == SetType.Pong || x.Combinations == SetType.Chow).ToList().Count;
        int pairCount = CombinationsI.Where(x => x.Combinations == SetType.Pair).ToList().Count;
        int preChowCount = CombinationsI.Where(x => x.Combinations == SetType.borderChow || x.Combinations == SetType.miniChow).ToList().Count;
        if (setCount >= 4)
        {
            if (pairCount >= 1)
            {
                OneOff = true;
                GameManager.GM.BoxWithScore[id].GetComponent<Animator>().SetTrigger("mahjong");
             

                GameManager.GM.NextRound(id, false, GameManager.spawnerS.GetComponent<Spawner>().LastTileFromWall(LastPickedTile), (LastPickedTile.GetComponent<CellDragging>().ScoreInCells.x == 2 && LastPickedTile.GetComponent<CellDragging>().ScoreInCells.y == 1), false);
                return;
                //Маджонг
            }
            else
            {
                OneOff = true;
               
                //просящая рука
            }

        }
        else if (setCount == 3 && pairCount >= 1)
        {
            if (pairCount == 2 || preChowCount > 0)
            {
                OneOff = true;
           
                //просящая рука
            }
        }
        else
        {
            OneOff = false;
            //до комбо  еще далеко
        }
        GameManager.GM.BoxWithScore[id].GetComponent<Animator>().SetBool("activeoff", OneOff);
    }

    public void RebuildOpenSetPlaces()
    {
        foreach (CombinationsInfo comb in OpenCombinations)
        {
            comb.CombinationsFirstTile.GetComponent<CellDragging>().openSetPosIDPlace = comb.CombinationsFirstTile.transform.GetSiblingIndex() - 1;
            foreach (GameObject combOther in comb.OtherTiles)
            {
                combOther.GetComponent<CellDragging>().openSetPosIDPlace = combOther.transform.GetSiblingIndex() - 1;
            }
        }
    }


    public List<CombinationsInfo> OpenCombinations = new List<CombinationsInfo>();
    public List<CombinationsInfo> CombinationsI = new List<CombinationsInfo>();
    public List<SetType> Combinations = new List<SetType>();
    public List<GameObject> TilesInHand = new List<GameObject>();
    public List<GameObject> AllTilesInHand = new List<GameObject>();



    public void SetAllTilesInList()
    {
        AllTilesInHand.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            AllTilesInHand.Add(transform.GetChild(i).gameObject);
        }
        TilesInHand.Clear();
        TilesInHand.AddRange(AllTilesInHand);
    }


    public void GetAllTilesInList()
    {
        AllTilesInHand.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<CellDragging>() != null)
            {
                AllTilesInHand.Add(transform.GetChild(i).gameObject);
            }
        
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!outSortRange)
        {
            List<Vector2> cells = new List<Vector2>();
            for (int i = 1; i < transform.childCount; i++)
            {
                if(!transform.GetChild(i).GetComponent<CellDragging>().OpenSet)
                {
                    cells.Add(new Vector2(i, transform.GetChild(i).transform.localPosition.x));
                }
                else
                {
                    CellDragging CD = transform.GetChild(i).GetComponent<CellDragging>();
                    int newPosID = CD.openSetPosIDPlace - (transform.childCount - 1) / 2;
                    if (newPosID != CD.PosInHand)
                    {
                        if (CD.SC != null)
                        {
                            Vector3 pos = transform.TransformPoint(Vector3.zero + ((Vector3.right * 0.36f) * (newPosID)));
                            CD.SC.transform.position = GameManager.GM.MainCam.GetComponent<Camera>().WorldToScreenPoint(pos);
                        }

                        CD.PosInHand = newPosID;
                    }
                }
               
            }
            cells = cells.OrderBy(x => x.y).ToList();
            TilesInHand.Clear();
            AllTilesInHand.Clear();

            for (int i = 1; i < transform.childCount; i++)
            {
                AllTilesInHand.Add(transform.GetChild(i).gameObject);
            }


            for (int i = 0; i < cells.Count; i++)
            {
                TilesInHand.Add(transform.GetChild((int)cells[i].x).gameObject);
                transform.GetChild((int)cells[i].x).GetComponent<CellDragging>().PosInHand = LastOpenSetTilesSort + i - (transform.childCount - 1) / 2;
            }
            for (int i = 0; i < TilesInHand.Count; i++)
            {
                TilesInHand[i].transform.SetSiblingIndex(LastOpenSetTilesSort + i + 1);
            }
            //for (int i = 1; i < transform.childCount; i++)
            //{
                
            //}
        }
        if (id == 1 || id == 3)
        {
            transform.GetChild(0).GetComponent<DominoShower>().PosInHand = -1 - (transform.childCount - 1) / 2;
        }
        else
        {
            transform.GetChild(0).GetComponent<DominoShower>().PosInHand = (transform.childCount - 1) - (transform.childCount - 1) / 2;
        }


        HUD.transform.position = HUdPosition;
    }
}
