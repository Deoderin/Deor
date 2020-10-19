using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleController : MonoBehaviour
{
   
    public bool Scaling;
    private Camera thisCamera;
    public static float fieldOfView;
    public static Vector4 TileBorders;
    private float tileSize = 0.63f;
    public static bool CellZoomStage = false;
    public static bool UnZoommStage = false;
    public static Vector3 CameraCellPos;
   
    public Vector3 startPos;
    public Animator DarkZoom;
    public AnimationCurve Forx;
    public AnimationCurve Fory;
    public GameObject planeBackground;
    public GameObject planeBackground2;
    const float scaleBackground = 3.55641f;
    // Start is called before the first frame update
    void Start()
    {
        Scaling = false;
        ChangeZoomStage(false);
        thisCamera = GetComponent<Camera>();
        fieldOfView = thisCamera.orthographicSize;
        TileBorders = Vector4.zero;
    }

    public static void ChangeZoomStage(bool stage)
    {
        CellZoomStage = stage;
        foreach (GameObject hud in GameObject.FindGameObjectsWithTag("Hud"))
        {
            hud.GetComponent<Animator>().SetBool("select", !stage);
        }
    }

    public static void CheckNewBorders(Vector2 TilePos)
    {
        if (TilePos.x < TileBorders.x) TileBorders.x = TilePos.x;
        if (TilePos.x > TileBorders.y) TileBorders.y = TilePos.x;
        if (TilePos.y < TileBorders.z) TileBorders.z = TilePos.y;
        if (TilePos.y > TileBorders.w) TileBorders.w = TilePos.y;
        GameObject.Find("Camera").GetComponent<ScaleController>().ChangeScale();
    }

    public void ChangeScale()
    {
        Scaling = true;
        int xLength = Mathf.RoundToInt((Mathf.Max((Mathf.Abs(TileBorders.y)), (Mathf.Abs(TileBorders.x))) / tileSize));
        int yLength = Mathf.RoundToInt((Mathf.Max((Mathf.Abs(TileBorders.w)), (Mathf.Abs(TileBorders.z))) / tileSize));
           fieldOfView = Mathf.Max(Fory.Evaluate(yLength), Forx.Evaluate(xLength));
        StartCoroutine(ChangingTime());
    }
    IEnumerator ChangingTime()
    {
        yield return new WaitForSeconds(2f);
        Scaling = false;
    }
    // Update is called once per frame
    void Update()
    {
        DarkZoom.SetBool("zoom", CellZoomStage);
        if (!CellZoomStage)
        { 
           thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize, fieldOfView, Time.deltaTime * 4);
            thisCamera.transform.position = Vector3.Lerp(thisCamera.transform.position, startPos, Time.deltaTime * 4);       
        }
        else
        {
            thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize, 1.2f, Time.deltaTime * 4);
            thisCamera.transform.position = Vector3.Lerp(thisCamera.transform.position, new Vector3(CameraCellPos.x, thisCamera.transform.position.y, CameraCellPos.z), Time.deltaTime * 4);
        }
        planeBackground.transform.position = new Vector3(thisCamera.transform.position.x, planeBackground.transform.position.y, thisCamera.transform.position.z);
        planeBackground.transform.localScale = new Vector3(thisCamera.orthographicSize * scaleBackground, thisCamera.orthographicSize * scaleBackground, 1);

    }
}
