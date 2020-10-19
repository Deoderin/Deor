using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BackEndImagesLoader : MonoBehaviour
{
    //logo location in local folder
    public static string pathMiniCanvasImage = "";
    //logo presence trigger
    public static bool startload;

    public static Sprite CanvasMiniImage;


    public Image CanvasMini;


    public static BackEndImagesLoader BE;
    private void Start()
    {
        BE = this;
    }
    public static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData = null;

        //Check Image in folder again
        if (File.Exists(filePath))
        {

            fileData = File.ReadAllBytes(filePath);

            //Create new texture
            tex = new Texture2D(2, 2);

            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.

        }
        return tex;
    }

    public static void LoadLogo()
    {
        //Check Image in local folder

        if (File.Exists(pathMiniCanvasImage))
        {
            if (BE.CanvasMini != null)
            {
                Texture2D Img = LoadPNG(pathMiniCanvasImage);
                CanvasMiniImage = Sprite.Create(Img, new Rect(0, 0, Img.width, Img.height), new Vector2(0.5f, 0.5f), 8); ;
                BE.CanvasMini.sprite = CanvasMiniImage;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //s1 = 
        //Debug.Log(this.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0));
        //Debug.Log(this.gameObject.GetComponent<Animator>().GetNextAnimatorStateInfo(0));
        if (startload)
        {
            //Load Image if restoran have backend
            LoadLogo();
            startload = false;
        }
    }
}
