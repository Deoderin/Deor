using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        switch (id)
        {
            case 0:
                if (BackEndImagesLoader.CanvasMiniImage != null)
                {
                    GetComponent<Image>().sprite = BackEndImagesLoader.CanvasMiniImage;
                }
                break;

        }
    }
}
