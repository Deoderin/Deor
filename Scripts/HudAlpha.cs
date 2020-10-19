using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudAlpha : MonoBehaviour
{
    // Start is called before the first frame update
    public int playerID;
    public Text[] textarray;
    public Image[] spritearray;
    public float alpha = 1;

    // Update is called once per frame
    void Update()
    {
        foreach(Text text in textarray)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b,alpha);
        }
        foreach (Image sprite in spritearray)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
        }
    }
}
