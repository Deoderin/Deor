using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextForDice : MonoBehaviour
{
    public GameObject value1;
    public GameObject value2;
    public GameObject dice_;
    public GameObject swich;
    public GameObject chipsWhite;
    public GameObject chipsBlack;

    int number_1;
    int number_2;
    void Start()
    {
    }
    private void Update()
    {
        value2.GetComponent<Text>().text = ("Игрок 2: " + number_2.ToString());
        value1.GetComponent<Text>().text = ("Игрок 1: " + number_1.ToString());
        Dice dice = dice_.GetComponent<Dice>();
        if (number_1 == 0 && (dice.i % 2) != 0)
        {
            number_1 = dice.diceValue;
        }
        if (number_1 != 0 && dice.diceValue != 0 && (dice.i % 2) == 0)
        {
            number_2 = dice.diceValue;
        }
        if (number_1 == number_2 && number_2 != 0)
        {
            swich.GetComponent<Text>().text = "Одинаковые значения кости, повторите броски";
        }
        if (number_1 == number_2)
        {
            number_1 = 0;
            number_2 = 0;
        }
        if (number_1 > number_2 && number_2 != 0)
        {
            swich.GetComponent<Text>().text = "Игрок 1 ходит первым, выберете цвет";
            dice_.SetActive(false);
            chipsWhite.SetActive(true);
            chipsBlack.SetActive(true);
        }
        else if (number_1 < number_2 && number_2 != 0)
        {
            swich.GetComponent<Text>().text = "Игрок 2 ходит первым, выберете цвет";
            dice_.SetActive(false);
            chipsBlack.SetActive(true);
            chipsWhite.SetActive(true);
        }
    }
}