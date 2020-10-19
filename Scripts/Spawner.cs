using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Domino;


    public List<Vector2> CellsCountSpawn;
    public GameObject[] DominoMeshes;
    public GameObject[] DominoMeshesTransparentROund;
    public GameObject DominoMeshesTransparentClear;
    public Vector2[] DominoValue;
    public GameObject[] Wall;

    //для легендарной комбинации зеленого ифрита
    public List<Vector2> GreenTilesValue;



    public GameManager GM;

    public GameObject GF;
    // Start is called before the first frame update
    void Start()
    {
        GF = GameObject.Find("GamePlane");
        GM = GameObject.Find("GM").GetComponent<GameManager>();
        //Spawn();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameManager.nextTurn(false, -1);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.GM.UpdateScoreSticks();
        }

    }
    public void SetDominoView(GameObject domino, int index, int indexR)
    {
        domino.GetComponent<CellDragging>().ScoreInCells = DominoValue[index];
        domino.GetComponent<CellDragging>().InitDOminoInfo();
        domino.GetComponent<CellDragging>().PosDominoInArraySpawner = index;
        CellsCountSpawn[indexR] = new Vector2(CellsCountSpawn[indexR].x, CellsCountSpawn[indexR].y - 1);
        if (CellsCountSpawn[indexR].y == 0)
        {
            CellsCountSpawn.RemoveAt(indexR);
        }
        //domino.GetComponent<MeshFilter>().mesh = DominoMeshes[index];
    }




    public void ResetDOminoCount()
    {
        CellsCountSpawn.Clear();
        for (int j = 0; j < DominoValue.Length; j++)
        {
            CellsCountSpawn.Add(new Vector2(j, 4));
        }
    }


    public void DOminoInit(int i, int k, int destrIndex, int j, int deadwallInd,bool useSave,int savespawnindex)

    {
        GameObject domino = Instantiate(Domino, Wall[i].transform.position, Quaternion.identity, Wall[i].transform);


        domino.transform.localRotation = Quaternion.Euler(Vector3.right * 110);
        int indexRandom = -1;
        int indexX = -1;
        if (!useSave)
        {
           indexRandom = Random.Range(0, CellsCountSpawn.Count);
            Saveloader.SaveFile.WallSpawn.Add(indexRandom);
        }
        else
        {
            indexRandom = Saveloader.SaveFileTest.WallSpawn[savespawnindex];
        }
        indexX = (int)CellsCountSpawn[indexRandom].x;

        bool deadwall = (deadwallInd == i && k >= destrIndex && k < destrIndex + 7);
        Instantiate((deadwall) ? ((j == 0) ? DominoMeshesTransparentClear : DominoMeshesTransparentROund[GameManager.Round]) : DominoMeshes[indexX], domino.transform.position, Quaternion.identity, domino.transform);

        domino.GetComponent<CellDragging>().DeadWall = deadwall;
        

       domino.transform.GetChild(1).localRotation = Quaternion.Euler(0, 180, 0);
        domino.GetComponent<CellDragging>().State = CellDragging.DominoState.Set;
        domino.GetComponent<CellDragging>().floor = j;
        if (j == 0)
        {
            domino.GetComponent<CellDragging>().PosInWall = new Vector3(i, j, Wall[i].GetComponent<WallCOntroller>().floor1.Count);
            Wall[i].GetComponent<WallCOntroller>().floor1.Add(domino);
     
            domino.transform.localPosition = new Vector3((Wall[i].GetComponent<WallCOntroller>().floor1.Count - 1) * 0.33f, 0, 0.02f);
        }
        else
        {
            domino.GetComponent<CellDragging>().PosInWall = new Vector3(i, j, Wall[i].GetComponent<WallCOntroller>().floor2.Count);
            Wall[i].GetComponent<WallCOntroller>().floor2.Add(domino);
            domino.transform.localPosition = new Vector3((Wall[i].GetComponent<WallCOntroller>().floor2.Count - 1) * 0.33f, 0.2f, 0);
        }

        SetDominoView(domino, indexX, indexRandom);
    }

    public AddPlayer[] players;
    public IEnumerator SpawnTIles(int destrIndex, int STARTplayerIndex,int DWI)
    {

        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < Wall.Length; i++)
            {
                for (int k = 0; k < 17; k++)
                {

                    DOminoInit(i, k, destrIndex, j, DWI,false,-1);
                    yield return new WaitForSeconds(0.02f / (GameManager.Round + 1));
                }

            }

        }

        foreach (AddPlayer ap in players)
        {
            if (!ap.created)
            {
                ap.AddPlayers(false);
            }

        }

        int index = (STARTplayerIndex == -1) ? Random.Range(0, GameManager.PlayersS.transform.childCount) : STARTplayerIndex;
        StartCoroutine(SetTilesToPlayer(index, destrIndex));
        //GameManager.GM.UpdateScoreSticks();

        GameManager.StartGame(index, index);

    }
    public int DestrWallIndex;
    public void Spawn(int index)
    {
        DestrWallIndex = Random.Range(0, 4);
        Saveloader.SaveFile.WallStartIndex = DestrWallIndex;
        ResetDOminoCount();

        for (int i = 0; i < Wall.Length; i++)
        {
            Wall[i].GetComponent<WallCOntroller>().floor1.Clear();
            Wall[i].GetComponent<WallCOntroller>().floor2.Clear();
        }
        floor = false;
        Saveloader.SaveFile.floor = floor;
        int wallind = Random.Range(0, 10);
        Saveloader.SaveFile.InWallStartIndexSpawn = wallind;
        StartCoroutine(SpawnTIles(wallind, index, DestrWallIndex));


        
    }

    public void SaveSpawn(int DestrWallIndexs, int wallinds)
    {
        ResetDOminoCount();

        for (int i = 0; i < Wall.Length; i++)
        {
            Wall[i].GetComponent<WallCOntroller>().floor1.Clear();
            Wall[i].GetComponent<WallCOntroller>().floor2.Clear();
        }
        floor = false;

        int wallind = wallinds;
        int spawnRandomIndexFOrSave = 0 ;
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < Wall.Length; i++)
            {
                for (int k = 0; k < 17; k++)
                {
                    DOminoInit(i, k, DestrWallIndexs, j, wallinds,true, spawnRandomIndexFOrSave);
                    spawnRandomIndexFOrSave++;
                }
            }
        }

        foreach (AddPlayer ap in players)
        {
            if (!ap.created)
            {
                ap.AddPlayers(false);
            }

        }

        //int indexz = ;

        // StartCoroutine(SpawnTIles(wallind, indexz));

        GameManager.StartGame(Saveloader.SaveFileTest.activeindex, Saveloader.SaveFileTest.East);
        
    }


    public bool NextDeadWall()
    {
        GameObject domino = floor ? SelecterWC.floor1[pickIndexS] : SelecterWC.floor2[pickIndexS];
        if (domino.GetComponent<CellDragging>().DeadWall)
        {
            return true;
        }
        else return false;
    }



    public bool LastTileFromWall(GameObject tile)
    {
        int sameTileCount = 0;
        for (int i = 0; i < Wall.Length; i++)
        {
            sameTileCount += Wall[i].GetComponent<WallCOntroller>().floor1.Where(x => x.GetComponent<CellDragging>().ScoreInCells == tile.GetComponent<CellDragging>().ScoreInCells).ToArray().Length;
            sameTileCount += Wall[i].GetComponent<WallCOntroller>().floor2.Where(x => x.GetComponent<CellDragging>().ScoreInCells == tile.GetComponent<CellDragging>().ScoreInCells).ToArray().Length;
        }

        return (sameTileCount > 0);
    }

    public void SaveAddTile(int playerIndex,Vector3 posinWall,int posinHand)
    {
        GameObject domino = new GameObject();
        SelecterWC = Wall[(int)posinWall.x].GetComponent<WallCOntroller>();
        floor = (posinWall.y == 0);

        if (floor)
        {
            domino = SelecterWC.floor1.Find(x => x.GetComponent<CellDragging>().PosInWall == posinWall);
        }
        else
        {
            domino = SelecterWC.floor2.Find(x => x.GetComponent<CellDragging>().PosInWall == posinWall);
        }
       

        //  GameObject domino = floor ? SelecterWC.floor1[pickIndexS] : SelecterWC.floor2[pickIndexS];
        if (domino.GetComponent<CellDragging>().DeadWall)
        {
            // конец раунда
            GameManager.GM.NextRound(GameManager.PlayersS.transform.GetChild(playerIndex).GetComponent<PlayerController>().id, true, false, false, true);
            return;
        }

        if (floor) SelecterWC.floor1.Remove(domino);
        else SelecterWC.floor2.Remove(domino);

  
        Transform curPlayer = GameManager.PlayersS.transform.GetChild(playerIndex);
        domino.transform.SetParent(curPlayer);

        domino.GetComponent<CellDragging>().Player = curPlayer.gameObject;
        domino.GetComponent<CellDragging>().Player.GetComponent<PlayerController>().LastPickedTile = domino;
        domino.GetComponent<CellDragging>().SetColor();
        domino.GetComponent<CellDragging>().State = CellDragging.DominoState.InHand;
        domino.GetComponent<CellDragging>().PosInHand = posinHand;
        domino.transform.localPosition = Vector3.zero + ((Vector3.right * 0.36f) * (posinHand));

    }

    public void AddTile(int playerIndex)
    {
        GameObject domino = floor ? SelecterWC.floor1[pickIndexS] : SelecterWC.floor2[pickIndexS];
        //Debug.LogError(domino.GetComponent<CellDragging>().poi);
        if (domino.GetComponent<CellDragging>().DeadWall)
        {
            // конец раунда
            GameManager.GM.NextRound(GameManager.PlayersS.transform.GetChild(playerIndex).GetComponent<PlayerController>().id, true, false, false, true);
            return;
        }
        if (floor) SelecterWC.floor1.RemoveAt(pickIndexS);
        else SelecterWC.floor2.RemoveAt(pickIndexS);

        if (SelecterWC.floor1.Count < pickIndexS + 1 && SelecterWC.floor2.Count < pickIndexS + 1)
        {
            IndexWall++;
            if (IndexWall >= Wall.Length) IndexWall = 0;
            Saveloader.SaveFile.IndexWall = IndexWall;
            SelecterWC = Wall[IndexWall].GetComponent<WallCOntroller>();
            pickIndexS = 0;
            Saveloader.SaveFile.pickIndexS = pickIndexS;
        }
        Transform curPlayer = GameManager.PlayersS.transform.GetChild(playerIndex);
        domino.transform.SetParent(curPlayer);

        domino.GetComponent<CellDragging>().Player = curPlayer.gameObject;
        domino.GetComponent<CellDragging>().Player.GetComponent<PlayerController>().LastPickedTile = domino;
        domino.GetComponent<CellDragging>().SetColor();
        domino.GetComponent<CellDragging>().State = CellDragging.DominoState.InHand;
        CellDragging CDO = domino.GetComponent<CellDragging>();
        PlayerController PC = curPlayer.GetComponent<PlayerController>();
        var selSet = PC.OpenCombinations.Where(p => p.Combinations == PlayerController.SetType.Pong).ToList();
        if (selSet != null && selSet.Count > 0)
        {
            foreach (PlayerController.CombinationsInfo SelS in selSet)
            {
                Vector2 SIC = SelS.CombinationsFirstTile.GetComponent<CellDragging>().ScoreInCells;
                if (SIC.x == CDO.ScoreInCells.x && SIC.y == CDO.ScoreInCells.y)
                {
                    SelS.HasOpenSteelKong2 = true;
                    SelS.Combinations = PlayerController.SetType.Kong;
                    SelS.OtherTiles.Add(domino);

                    SelS.SetIndicator.GetComponent<SetController>().type = (SetController.SetType)((int)(PlayerController.SetType.Kong));
                    SelS.SetIndicator.GetComponent<SetController>().ResetIndicator();


                    domino.GetComponent<CellDragging>().openSetPosIDPlace = SelS.OtherTiles[SelS.OtherTiles.Count - 2].GetComponent<CellDragging>().openSetPosIDPlace + 1;

                    //все в иерархии стоят правильно
                    CDO.transform.SetSiblingIndex(domino.GetComponent<CellDragging>().openSetPosIDPlace + 1);

                    CDO.OpenSet = true;
                    //другое
                    CDO.PosInHand = CDO.openSetPosIDPlace - (transform.childCount - 1) / 2;
                    PC.LastOpenSetTilesSort++;

                    curPlayer.GetComponent<PlayerController>().RebuildOpenSetPlaces();


                    //прокачать комбинацию
                    break;
                }
            }


        }

        floor = !floor;

        Saveloader.SaveFile.floor = floor;
        
    }
    [HideInInspector]
    public WallCOntroller SelecterWC;
    [HideInInspector]
    public int pickIndexS;
    [HideInInspector]
    public int IndexWall;
    [HideInInspector]
    public bool floor = false;
    public IEnumerator SetTilesToPlayer(int east, int index)
    {
        pickIndexS = index + 7;
  
        Saveloader.SaveFile.pickIndexS = pickIndexS;
        IndexWall = DestrWallIndex;
        Saveloader.SaveFile.IndexWall = IndexWall;
        SelecterWC = Wall[IndexWall].GetComponent<WallCOntroller>();
        floor = false;
        Saveloader.SaveFile.floor = floor;
        for (int j = 0; j < 4; j++)
        {
            for (int k = 0; k < 13; k++)
            {

                AddTile(j);
                yield return new WaitForSeconds(0.05f / (GameManager.Round + 1));

            }
            if (j == GameManager.ActivePlayerIndex)
            {
                AddTile(east);

            }
        }
        Saveloader.PushSaveInfo();

    }
}
