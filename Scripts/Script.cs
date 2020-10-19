
using UnityEngine;

#if ENABLE_WINMD_SUPPORT

using IL2CPPToDotNetBridge;

class IL2CPPBridge : IIL2CPPBridge
{

public void SetString(int arg, string value)
    {
         switch (arg)
    {

case 0:
       BackEndImagesLoader.pathMiniCanvasImage = value;
        break;


    default:
        break;
    }
}




    public int GetInt(int arg)
    {
        int retValue = 0;
        switch (arg)
        {
            case 5:
                retValue = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ChekerUWP>().TypeTable;
                break;
            case 8:
                retValue = UnityEngine.Screen.height;
                break;
            case 9:
                retValue = UnityEngine.Screen.width;
                break;
            case 10:
                retValue = ResolutionControl.startload;
                break;

            default:
                break;
        }
        return retValue;
    }

    public void SetTochFunc(int id, float x, float y, float z)

    {
        ChekerUWP.myObj.Add(new ChekerUWP.InputTypeo(id, new Vector3(x, y, z)));
    }

    public void SaveSetScore(int p1, int p2, int p3, int p4)
    {
        Saveloader.SaveFileTest.PlayerScore[0] = p1;
        Saveloader.SaveFileTest.PlayerScore[1] = p2;
        Saveloader.SaveFileTest.PlayerScore[2] = p3;
        Saveloader.SaveFileTest.PlayerScore[3] = p4;
    }
    public void SaveSetMainParam(int pickIndexS, int IndexWall, int East, int WallStartIndex, int InWallStartIndexSpawn, int activeindex, int RoundNumber, bool GameIsActual, bool RoundLoad, bool floor)
    {
        Saveloader.SaveFileTest.SetMainParam(pickIndexS, IndexWall, East, WallStartIndex, InWallStartIndexSpawn, activeindex, RoundNumber, GameIsActual, RoundLoad, floor);
    }

    public void SaveAddWallSpawn(int value)
    {
        Saveloader.SaveFileTest.WallSpawn.Add(value);
    }
    public void SaveAddDiscardTiles(float x, float y, float z)
    {
        Saveloader.SaveFileTest.DiscardTiles.Add(new Vector3(x, y, z));
    }
    public void SaveAddOpenTileFinderSet(float x, float y, float z, float w)
    {
        Saveloader.SaveFileTest.OpenTileFinderSet.Add(new Vector4(x, y, z, w));
    }
    public void SaveAddHandTiles(float x, float y, float z, int posinHand, int ID)
    {
        Saveloader.SaveFileTest.HandTiles.Add(new Saveloader.TileInHand(ID, new Vector3(x, y, z), posinHand));
    }




    public void LangugageInit(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, string s11, string s12, string s13, string s14, string s15, string s16, string s17, string s18, string s19, string s20, string s21, string s22, string s23, string s24, string s25, string s26, string s27, string s28, string s29, string s30, string s31, string s32, string s33, string s34, string s35, string s36, string s37, string s38, string s39, string s40, string s41)
    {
        Localization.InitStrings(s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15, s16, s17, s18, s19, s20, s21, s22, s23, s24, s25, s26, s27, s28, s29, s30, s31, s32, s33, s34, s35, s36, s37, s38, s39, s40, s41);
    }


    public void SetSession(string session)
    {
        Debug.LogError(session);

    }


    public void CallFunction(int arg)
    {
        switch (arg)
        {
            case 1:
                Saveloader.SaveFileTest.WallSpawn.Clear();
                break;
            case 2:
                Saveloader.SaveFileTest.DiscardTiles.Clear();
                break;
            case 3:
                Saveloader.SaveFileTest = new Saveloader.SaveObj();
                break;
            case 4:
                ChekerUWP.myObj.Clear();
                break;
            case 5:
                ResolutionControl.startload = 0;
                break;
            case 6:
                Suspq.Suspend();
                break;
            case 7:
                Suspq.On();
                break;
            case 8:
                Saveloader.LoadSaveS();
                break;
            case 9:
                GameManager.LoadNewGameAnimation();
                break;
            case 10:
                Saveloader.SaveFileTest.OpenTileFinderSet.Clear();
                break;
            case 11:
                Saveloader.SaveFileTest.HandTiles.Clear();
                break;
            case 12:
                BackEndImagesLoader.startload = true;
                break;
            default:
                break;
        }
    }
}

#endif

public class Script : MonoBehaviour
{
    void Awake()
    {
#if ENABLE_WINMD_SUPPORT
        BridgeBootstrapper.SetIL2CPPBridge(new IL2CPPBridge());
#endif
    }

    void Start()
    {
#if ENABLE_WINMD_SUPPORT
        var dotnetBridge = BridgeBootstrapper.GetDotNetBridge();
              dotnetBridge.Start();
#endif
    }


    public static void Pushsave()
    {
#if ENABLE_WINMD_SUPPORT
                var dotnetBridge = BridgeBootstrapper.GetDotNetBridge();

        dotnetBridge.SaveSetScore(Saveloader.SaveFile.PlayerScore[0], Saveloader.SaveFile.PlayerScore[1], Saveloader.SaveFile.PlayerScore[2], Saveloader.SaveFile.PlayerScore[3]);
        dotnetBridge.SaveSetMainParam(Saveloader.SaveFile.pickIndexS, Saveloader.SaveFile.IndexWall, Saveloader.SaveFile.East, Saveloader.SaveFile.WallStartIndex, Saveloader.SaveFile.InWallStartIndexSpawn, Saveloader.SaveFile.activeindex, Saveloader.SaveFile.RoundNumber, Saveloader.SaveFile.GameIsActual, Saveloader.SaveFile.RoundLoad, Saveloader.SaveFile.floor);
        dotnetBridge.SaveClearWallSpawn();
        dotnetBridge.SaveClearDiscardTiles();
        dotnetBridge.SaveClearOpenTileFinderSet();
        dotnetBridge.SaveClearHandTiles();


        


        foreach (int value in Saveloader.SaveFile.WallSpawn)
        {
        dotnetBridge.SaveAddWallSpawn(value);
        }

        foreach (Vector3 posinwall in Saveloader.SaveFile.DiscardTiles)
        {
            dotnetBridge.SaveAddDiscardTiles(posinwall.x, posinwall.y, posinwall.z);
        }
        foreach (Vector4 handtileinfo in Saveloader.SaveFile.OpenTileFinderSet)
        {
            dotnetBridge.SaveAddOpenTileFinderSet(handtileinfo.x, handtileinfo.y, handtileinfo.z, handtileinfo.w);
        }
        foreach (Saveloader.TileInHand info in Saveloader.SaveFile.HandTiles)
        {
            dotnetBridge.SaveAddHandTiles(info.posinWall.x, info.posinWall.y, info.posinWall.z, info.PosInhand, info.PlayerID);
        }

#endif
    }


    void Update()
    {
#if ENABLE_WINMD_SUPPORT
        var dotnetBridge = BridgeBootstrapper.GetDotNetBridge();
   dotnetBridge.Update();

#endif
    }
}