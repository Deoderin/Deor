using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Choise : MonoBehaviour
{

    public GameObject _gen;
    public GameObject _CanText;
    public GameObject Dice1;
    public GameObject Dice2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    RaycastHit hit;
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    if (Physics.Raycast(ray, out hit, 100))
    //    {
    //        //if (hit.collider.gameObject == blackChips)
    //        //{
    //        //    lightBlack.SetActive(true);
    //        //}
    //        //else
    //        //{
    //        //    lightBlack.SetActive(false);
    //        //}
    //        //if (hit.collider.gameObject == whiteChips)
    //        //{
    //        //    lightWhite.SetActive(true);
    //        //}
    //        //else
    //        //{
    //        //    lightWhite.SetActive(false);
    //        //}
    //        //if (Input.GetMouseButtonDown(0))
    //        //{
    //        //    if (hit.collider.gameObject == blackChips)
    //        //    {
    //        //        Generator gen = _gen.GetComponent<Generator>();
    //        //        gen.moveBlack = false;
    //        //        Deactiv();
    //        //    }
    //        //    if (hit.collider.gameObject == whiteChips)
    //        //    {
    //        //        Generator gen = _gen.GetComponent<Generator>();
    //        //        gen.moveBlack = true;
    //        //        Deactiv();
    //        //    }
    //        //}
    //    }
    //}
    public void Deactiv()
    {
        GM.State = GM.GameState.Roll;
          Generator gen = _gen.GetComponent<Generator>();
          gen.moveBlack = (Random.value < 0.5);
        //TextForDice CanText = _CanText.GetComponent<TextForDice>();
        gen.go = true;
        gen.spawn = 1;
       // whiteChips.SetActive(false);
       // blackChips.SetActive(false);
        Dice1.SetActive(true);
        Dice2.SetActive(true);
    }
}
