using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationLoader : MonoBehaviour
{
    public int TextType;
    // Start is called before the first frame update
    void Start()
    {
        switch (TextType)
        {
            case 0:
                GetComponent<Text>().text = Localization.NewGame;
                break;
            case 1:
                GetComponent<Text>().text = Localization.GameResult;
                break;
            case 2:
              //  GetComponent<Text>().text = Localization.NoFreePlace;
                break;
            case 3:
              //  GetComponent<Text>().text = Localization.NoFreePlaceCell;
                break;
            case 4:
               // GetComponent<Text>().text = Localization.WaitSecondEnemies;
                break;
            case 5:
                GetComponent<Text>().text = Localization.Play;
                break;
            case 6:
                GetComponent<Text>().text = Localization.OpenCombination;
                break;
        }
    }

}
