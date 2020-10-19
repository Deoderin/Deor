using UnityEngine;
using UnityEngine.UI;

public class TextBoard : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject textGamerStep1;
    public GameObject textGamerStep2;
    public GameObject _gen;
    public GameObject textCentr;
    public GameObject dice_;
    public GameObject chipsWhite;
    public GameObject chipsBlack;
    public GameObject swich;

    int number_1;
    int number_2;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        Generator gen = _gen.GetComponent<Generator>();
        if (gen.go == false)
        {
           // textGamerStep2.GetComponent<Text>().text = ("Игрок 2: " + number_2.ToString());
           // textGamerStep1.GetComponent<Text>().text = ("Игрок 1: " + number_1.ToString());
            //Dice dice = dice_.GetComponent<Dice>();
            //if (number_1 == 0 && (dice.i % 2) != 0)
            //{
            //    number_1 = dice.diceValue;
            //}
            //if (number_1 != 0 && dice.diceValue != 0 && (dice.i % 2) == 0)
            //{
            //    number_2 = dice.diceValue;
            //}
            //if (number_1 == number_2 && number_2 != 0)
            //{
            //    swich.GetComponent<Text>().text = "Одинаковые значения кости, повторите броски";
            //}
            //if (number_1 == number_2)
            //{
            //    number_1 = 0;
            //    number_2 = 0;
            //}
            //if (number_1 > number_2 && number_2 != 0)
            //{
            //    swich.GetComponent<Text>().text = "Игрок 1 ходит первым, выберете цвет";
            //    dice_.SetActive(false);
            //    chipsWhite.SetActive(true);
            //    chipsBlack.SetActive(true);
            //}
            //else if (number_1 < number_2 && number_2 != 0)
            //{
            //    swich.GetComponent<Text>().text = "Игрок 2 ходит первым, выберете цвет";
            //    dice_.SetActive(false);
            //    chipsBlack.SetActive(true);
            //    chipsWhite.SetActive(true);
            //}
        }
        else if (gen.go == true)
        {
            //  textGamerStep2.GetComponent<Text>().text = "";
            //  swich.GetComponent<Text>().text = "";
            //if (gen.moveBlack == true)
            //{
            //    if (gen.move != 0)
            //    {
            //        textGamerStep1.GetComponent<Text>().text = "Ход игрока 1";
            //    }
            //}
            //if (gen.moveBlack == false)
            //{
            //    if (gen.move != 0)
            //    {
            //        textGamerStep1.GetComponent<Text>().text = "Ход игрока 2";
            //    }
            //}
            /*if (//сохранение)
            {
                if (gen.move != 0)
                {
                    textCentr.SetActive(true);
                    textCentr.GetComponent<Text>().text = "Пожалуйста, завершите ход";
                }
                 else if (//нажатие на кнопку)
                 {
                     textCentr.SetActive(true);
                     textCentr.GetComponent<Text>().text = "Игра сохранена";
                     //вызвать сохранение
                 }
                else
                {
                    textCentr.SetActive(false);
                }
            }
            */
            if (gen.scoreBlack == 15)
            {
                textCentr.GetComponent<Text>().text = "Игрок 1 одержал победу";
            }
            if (gen.scoreWhite == 15)
            {
                textCentr.GetComponent<Text>().text = "Игрок 2 одержал победу";
            }
        }
    }
    public void Switch()
    {

    }
}
