using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Android;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using UnityEditor.UIElements;
using System.ComponentModel.Design;
using JetBrains.Annotations;

public class Generator : MonoBehaviour
{
    float _pos_1 = 100;
    float _pos_2 = 100;
    float _pos_sum = 100;
    float _pos_sum_x2 = 100;
    float _now = 100;
    float finale;
    //float final_steps;

    Vector3 pos_1 = new Vector3(0, 0, 0);
    Vector3 pos_2 = new Vector3(0, 0, 0);
    Vector3 pos_sum = new Vector3(0, 0, 0);
    Vector3 pos_sum_x2 = new Vector3(0, 0, 0);
    Vector3 objPosition = new Vector3(0, 0, 0);
    Vector3 startPos = new Vector3(0, 0, 0);

    public GameObject black_chips;
    public GameObject white_chips;
    public GameObject board_1;
    public GameObject board_2;
    public GameObject dice_1;
    public GameObject dice_2;
    public GameObject dice_1_light;
    public GameObject dice_2_light;
    public GameObject finale_Steps_Black;
    public GameObject finale_Steps_White;
    public float distance = 60f;

    public bool go = false;
    public bool moveDice = false;
    private bool touchingness = false;
    public bool moveBlack = false;
    public bool blackToHome = false;
    private bool whiteToHome = false;
    public bool _dice_1 = false;
    public bool _dice_2 = false;
    private bool anus = false;
    public bool pressing = false;
    /*
        private int[,] board = new int[,]
            {
                {1,1,1,1,1},
                {0,0,0,0,0},
                {0,0,0,0,0},
                {0,0,0,0,0},
                {2,2,2,0,0},
                {0,0,0,0,0},
                {2,2,2,2,2},
                {0,0,0,0,0},
                {0,0,0,0,0},
                {0,0,0,0,0},
                {0,0,0,0,0},
                {1,1,0,0,0},
                {2,2,0,0,0},
                {0,0,0,0,0},
                {0,0,0,0,0},
                {0,0,0,0,0},
                {0,0,0,0,0},
                {1,1,1,1,1},
                {0,0,0,0,0},
                {1,1,1,0,0},
                {0,0,0,0,0},
                {0,0,0,0,0},
                {0,0,0,0,0},
                {2,2,2,2,2},
            };
    */
    public int[,] board = new int[24,15];
    public GameObject[,] _black_chips = new GameObject[24, 15];
    public GameObject[,] _white_chips = new GameObject[24, 15];
    public GameObject[] _position_chps_black = new GameObject[24];
    public GameObject[] _position_chps_white = new GameObject[24];

    public int spawn;
    public int scoreBlack;
    public int scoreWhite;
    public int lightPos1Black;
    public int lightPos2Black;
    public int lightPosSumBlack;
    public int lightPosSumX2Black;
    public int lightPos1White;
    public int lightPos2White;
    public int lightPosSumWhite;
    public int lightPosSumX2White;
    public int move;
    int board_correct;
    public int number_1;
    public int number_2;
    int _i;
    int _j;

    public float rayLength;
    public LayerMask layermask;
    public LayerMask _layermask;
    void Start()
    {
        //SpawnOneRowChips();
    }

    void Update()
    {
        if (go == true)
        {
            if (spawn == 1)
            {
                SpawnOneRowChips();
                spawn = 2;
            }
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, rayLength, layermask))
                {
                    for (int i = 0; i < 24; i++)
                    {
                        for (int j = 0; j < 15; j++)
                        {
                            if (moveBlack == true)
                            {
                                if (hit.collider.gameObject == _black_chips[i, j])
                                {
                                    PositionCheck(i, j);
                                    if (touchingness == true)
                                    {
                                        anus = true;
                                        startPos = new Vector3(_black_chips[i, j].gameObject.transform.localPosition.x, -0.48f, _black_chips[i, j].gameObject.transform.localPosition.z);
                                    }
                                }
                            }
                            if (moveBlack == false)
                            {
                                if (hit.collider.gameObject == _white_chips[i, j])
                                {
                                    Debug.LogError(_j);
                                    PositionCheck(i, j);
                                    if (touchingness == true)
                                    {
                                        anus = true;
                                        startPos = new Vector3(_white_chips[i, j].gameObject.transform.localPosition.x, -0.48f, _white_chips[i, j].gameObject.transform.localPosition.z);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (Input.GetMouseButton(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, rayLength, layermask))
                {
                    for (int i = 0; i < 24; i++)
                    {
                        for (int j = 0; j < 15; j++)
                        {
                            if (moveBlack == true)
                            {
                                if (hit.collider.gameObject == _black_chips[i, j])
                                {
                                    PositionCheck(i, j);
                                    if (touchingness == true)
                                    {
                                        MoveBlack(i, j);
                                        pressing = true;
                                    }
                                }
                                if (pressing == true)
                                {
                                    PresingBlack();
                                }
                            }
                            if (moveBlack == false)
                            {
                                if (hit.collider.gameObject == _white_chips[i, j])
                                {
                                    PositionCheck(i, j);
                                    if (touchingness == true)
                                    {
                                        MoveWhite(i, j);
                                        pressing = true;
                                    }
                                }
                                if (pressing == true)
                                {
                                    PresingWhite();
                                }
                            }
                        }
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                pressing = false;
                if (moveBlack == true && move != 0 && anus == true)
                {
                    MuvingForPositionfBlack();
                }
                if (moveBlack == false && move != 0 && anus == true)
                {
                    MuvingForPositionfWhite();
                }
            }
            if (move == 0)
            {
                NumberOfDice();
            }
            DiceLight();
            ChipsLight();
            Finish();
            BigDimasChips();
            ChoiseChips();
        }
    }

    void PresingBlack()
    {
        if (move != 0)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance); // переменной записываються координаты мыши по иксу и игрику
            objPosition = Camera.main.ScreenToWorldPoint(mousePosition); // переменной - объекту присваиваеться переменная с координатами мыши
            _black_chips[_i, _j].transform.position = objPosition;
        }
    }
    void PresingWhite()
    {
        if (move != 0)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance); // переменной записываються координаты мыши по иксу и игрику
            objPosition = Camera.main.ScreenToWorldPoint(mousePosition); // переменной - объекту присваиваеться переменная с координатами мыши
            _white_chips[_i, _j].transform.position = objPosition;
        }
    }

    //Перетаскивание шашки и генерация трех возможных маршрутов для черной шашки 
    private void MoveBlack(int i, int j)
    {
        _i = i;
        _j = j;

        if (i + number_1 < 24)
        {
            if (board[i + number_1, 0] != 2)
            {
                pos_1 = new Vector3(_position_chps_black[i + number_1].gameObject.transform.position.x, -0.48f, _position_chps_black[i + number_1].gameObject.transform.position.z);
                _pos_1 = Vector3.Distance(objPosition, pos_1);
            }
            else
            {
                _pos_1 = 10000;
            }
        }
        else if (i + number_1 >= 24 && blackToHome == true)
        {
            pos_1 = new Vector3(finale_Steps_Black.gameObject.transform.position.x, 0.18f, finale_Steps_Black.gameObject.transform.position.z);
            _pos_1 = Vector3.Distance(objPosition, pos_1);
        }
        else
        {
            _pos_1 = 10000;
        }
        if (i + number_2 < 24)
        {
            if (board[i + number_2, 0] != 2)
            {
                pos_2 = new Vector3(_position_chps_black[i + number_2].gameObject.transform.position.x, -0.48f, _position_chps_black[i + number_2].gameObject.transform.position.z);
                _pos_2 = Vector3.Distance(objPosition, pos_2);
            }
            else
            {
                _pos_2 = 10000;
            }
        }
        else if (i + number_2 >= 24 && blackToHome == true)
        {
            pos_2 = new Vector3(finale_Steps_Black.gameObject.transform.position.x, 0.18f, finale_Steps_Black.gameObject.transform.position.z);
            _pos_2 = Vector3.Distance(objPosition, pos_2);
        }
        else
        {
            _pos_2 = 10000;
        }
        if (i + (number_2 + number_1) < 24)
        {
            if (board[i + (number_2 + number_1), 0] != 2)
            {
                pos_sum = new Vector3(_position_chps_black[i + number_2 + number_1].gameObject.transform.position.x, -0.48f, _position_chps_black[i + number_2 + number_1].gameObject.transform.position.z);
                _pos_sum = Vector3.Distance(objPosition, pos_sum);
            }
            else
            {
                _pos_sum = 10000;
            }
        }
        else if (i + (number_1 + number_2) >= 24 && blackToHome == true)
        {
            pos_sum = new Vector3(finale_Steps_Black.gameObject.transform.position.x, 0.18f, finale_Steps_Black.gameObject.transform.position.z);
            _pos_sum = Vector3.Distance(objPosition, pos_sum);
        }
        else
        {
            _pos_sum = 10000;
        }
        if (i + (number_2 * 4) < 24)
        {
            if (board[i + (number_2  * 4), 0] != 2)
            {
                pos_sum_x2 = new Vector3(_position_chps_black[i + (number_2 * 4)].gameObject.transform.position.x, -0.48f, _position_chps_black[i + (number_2 * 4)].gameObject.transform.position.z);
                _pos_sum_x2 = Vector3.Distance(objPosition, pos_sum_x2);
            }
            else
            {
                _pos_sum_x2 = 10000;
            }
        }
        else
        {
            _pos_sum_x2 = 10000;
        }
    }

    //Перетаскивание шашки и генерация трех возможных маршрутов для белой шашки 
    private void MoveWhite(int i, int j)
    {
        _i = i;
        _j = j;

        if (i + number_1 < 24)
        {
            if (i + number_1 < 12)
            {
                board_correct = 12;
            } 
            else if (i + number_1 > 11)
            {
                board_correct = -12;
            }
            if (board[i + number_1 + board_correct, 0] != 1)
            {
                pos_1 = new Vector3(_position_chps_white[i + number_1].gameObject.transform.position.x, -0.48f, _position_chps_white[i + number_1].gameObject.transform.position.z);
                _pos_1 = Vector3.Distance(objPosition, pos_1);
            }
            else
            {
                _pos_1 = 10000;
            }
        }
        else if (i + number_1 >= 24 && whiteToHome == true)
        {
            pos_1 = new Vector3(finale_Steps_White.gameObject.transform.position.x, 0.18f, finale_Steps_White.gameObject.transform.position.z);
            _pos_1 = Vector3.Distance(objPosition, pos_1);
        }
        else
        {
            _pos_1 = 10000;
        }
        if (i + number_2 < 24)
        {
            if (i + number_2 < 12)
            {
                board_correct = 12;
            }
            else if (i + number_2 > 11)
            {
                board_correct = -12;
            }
            if (board[i + number_2 + board_correct, 0] != 1)
            {
                pos_2 = new Vector3(_position_chps_white[i + number_2].gameObject.transform.position.x, -0.48f, _position_chps_white[i + number_2].gameObject.transform.position.z);
                _pos_2 = Vector3.Distance(objPosition, pos_2);
            }
            else
            {
                _pos_2 = 10000;
            }
        }
        else if (i + number_1 >= 24 && whiteToHome == true)
        {
            pos_2 = new Vector3(finale_Steps_White.gameObject.transform.position.x, 0.18f, finale_Steps_White.gameObject.transform.position.z);
            _pos_2 = Vector3.Distance(objPosition, pos_2);
        }
        else
        {
            _pos_2 = 10000;
        }
        if (i + (number_2 + number_1) < 24)
        {
            if (i + (number_2 + number_1) < 12)
            {
                board_correct = 12;
            }
            else if (i + (number_2 + number_1) > 11)
            {
                board_correct = -12;
            }
            if (board[i + (number_2 + number_1) + board_correct, 0] != 1)
            {
                pos_sum = new Vector3(_position_chps_white[i + number_2 + number_1].gameObject.transform.position.x, -0.48f, _position_chps_white[i + number_2 + number_1].gameObject.transform.position.z);
                _pos_sum = Vector3.Distance(objPosition, pos_sum);
            }
            else
            {
                _pos_sum = 10000;
            }
        }
        else if (i + (number_1 + number_2) >= 24 && whiteToHome == true)
        {
            pos_sum = new Vector3(finale_Steps_White.gameObject.transform.position.x, 0.18f, finale_Steps_White.gameObject.transform.position.z);
            _pos_sum = Vector3.Distance(objPosition, pos_sum);
        }
        else
        {
            _pos_sum = 10000;
        }
        if (i + (number_2 * 4) < 24)
        {
            if (i + (number_2 * 4) < 12)
            {
                board_correct = 12;
            }
            else if (i + (number_2 * 4) > 11)
            {
                board_correct = -12;
            }
            if (board[i + (number_2 * 4) + board_correct, 0] != 1)
            {
                pos_sum_x2 = new Vector3(_position_chps_white[i + (number_2 * 4)].gameObject.transform.position.x, -0.48f, _position_chps_white[i + (number_2 * 4)].gameObject.transform.position.z);
                _pos_sum_x2 = Vector3.Distance(objPosition, pos_sum_x2);
            }
            else
            {
                _pos_sum_x2 = 10000;
            }
        }
        else
        {
            _pos_sum_x2 = 10000;
        }
        if (whiteToHome == true)
        {
            if (i + number_1 >= 24)
            {
                
            }
            if (i + number_2 >= 24)
            {
                //переместить в канавку
            }
            if (i + (number_2 + number_1) >= 24)
            {
                //переместить в канавку
            }
        }
    }

    //Проверка доступности шашки 
    private void PositionCheck(int i, int j)
    {
        if (moveBlack == true)
        {
            if (j == 14)
            {
                touchingness = true;
            }
            else if (_black_chips[i, j + 1] != null)
            {
                touchingness = false;
            }
            else if (_black_chips[i, j + 1] == null)
            {
                touchingness = true;
            }
        }
        if (moveBlack == false)
        {
            if (j == 14)
            {
                touchingness = true;
            }
            else if (_white_chips[i, j + 1] != null)
            {
                touchingness = false;
            }
            else if (_white_chips[i, j + 1] == null)
            {
                touchingness = true;
            }
        }
    }



    public Transform[] containers;
    //Генерация массива с шашками.
    private void SpawnOneRowChips()
    {
        for (int i = 0; i < 24; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                board[i, j] = 0;
                switch (i)
                {
                    case 0:
                        {
                            board[i, j] = 1;
                            Vector3 spawnPos = new Vector3(-14.31f + (1.616f * j), -0.48f, 15.28f);
                            _black_chips[i, j] = Instantiate(black_chips, spawnPos, Quaternion.Euler(-90, 0, 0), containers[1]);
                        }
                    break;
                    case 12:
                        {
                            board[i, j] = 2;
                            Vector3 spawnPos = new Vector3(14.03f - (1.616f *j) , -0.48f, -15.28f);
                            _white_chips[i - 12,j] = Instantiate(white_chips, spawnPos, Quaternion.Euler(-90, 0, 0), containers[0]);
                        }
                    break;
                }
            }
        }
    }

    //Перемещение черных шашек от позиции хвата к конечной позиции  
    private void MoveBlackChips(Vector3 positiontToMove, int number)
    {
        Debug.Log(number);
        RaycastHit hit;
        _black_chips[_i, _j].transform.position = positiontToMove;
        board[_i, _j] = 0;
        PositionDownBlack(_i);
        if (_i + number <= 11)
        {
            if (Physics.Raycast(_black_chips[_i, _j].transform.position, -Vector3.right, out hit, 100f))
            {
                if (hit.collider.gameObject == board_1 || hit.collider.gameObject == board_2)
                {
                    _black_chips[_i + number, 0] = _black_chips[_i, _j];
                    _black_chips[_i, _j] = null;
                    board[_i + number, 0] = 1;
                    _i = _i + number;
                    _j = 0;
                    PositionUpBlack(_i);
                    PositionBlackHome();
                }
                else
                {
                    for (int i = 0; i < 24; i++)
                    {
                        for (int j = 0; j < 15; j++)
                        {
                            if (hit.collider.gameObject == _black_chips[i, j])
                            {
                                _black_chips[i, j + 1] = _black_chips[_i, _j];
                                _black_chips[_i, _j] = null;
                                board[i, j + 1] = 1;
                                PositionUpBlack(i);
                                PositionBlackHome();
                            }
                        }
                    }
                }
            }
        }else if(_i + number >= 12)
        {
            if (Physics.Raycast(_black_chips[_i, _j].transform.position, Vector3.right, out hit, 100f))
            {
                if (moveBlack == true && _i + number >= 24)
                {
                    _black_chips[_i, _j] = null;
                    PositionUpBlack(_i);
                    finalStepBlack();
                    scoreBlack++;
                    //сдеалать подсчет 
                    //сделать счет очков
                }
                else if (hit.collider.gameObject == board_1 || hit.collider.gameObject == board_2)
                {
                    _black_chips[_i + number, 0] = _black_chips[_i, _j];
                    _black_chips[_i, _j] = null;
                    board[_i + number, 0] = 1;
                    _i = _i + number;
                    _j = 0;
                    PositionUpBlack(_i);
                    PositionBlackHome();
                }
                else
                {
                    for (int i = 0; i < 24; i++)
                    {
                        for (int j = 0; j < 15; j++)
                        {
                            if (hit.collider.gameObject == _black_chips[i, j])
                            {
                                _black_chips[i, j + 1] = _black_chips[_i, _j];
                                _black_chips[_i, _j] = null;
                                board[i, j + 1] = 1;
                                PositionUpBlack(i);
                                PositionBlackHome();
                            }
                        }
                    }
                }
            }
        }
    }

    //Перемещение белых шашек от позиции хвата к конечной позиции  
    private void MoveWhiteChips(Vector3 positiontToMove, int number)
    {
        RaycastHit hit;
        _white_chips[_i, _j].transform.position = positiontToMove;
        if (_i < 12)
        {
            board_correct = 12;
        }
        else if (_i > 11)
        {
            board_correct = -12;
        }
        board[_i + board_correct, _j] = 0;
        PositionDownWhite(_i);

        if (_i + number <= 11)
        {
            if (Physics.Raycast(_white_chips[_i, _j].transform.position, Vector3.right, out hit, 100f))
            {
                if (hit.collider.gameObject == board_1 || hit.collider.gameObject == board_2)
                {
                    _white_chips[_i + number, 0] = _white_chips[_i, _j];
                    _white_chips[_i, _j] = null;
                    board[_i + number + 12, 0] = 2;
                    _i = _i + number;
                    _j = 0;
                    PositionUpWhite(_i);
                    PositionWhiteHome();
                }
                else
                {
                    for (int i = 0; i < 24; i++)
                    {
                        for (int j = 0; j < 15; j++)
                        {
                            if (hit.collider.gameObject == _white_chips[i, j])
                            {
                                _white_chips[i, j + 1] = _white_chips[_i, _j];
                                _white_chips[_i, _j] = null;
                                board[i + 12, j + 1] = 2;
                                PositionUpWhite(i);
                                PositionWhiteHome();
                            }
                        }
                    }
                }
            }
        }
        else if (_i + number >= 12)
        {
            if (Physics.Raycast(_white_chips[_i, _j].transform.position, Vector3.right, out hit, 100f))
            {
                if (moveBlack == false && _i + number >= 24)
                {
                    _white_chips[_i, _j] = null;
                    PositionUpWhite(_i);
                    finalStepWhite();
                    scoreWhite++;
                    //сдеалать подсчет 
                    //сделать счет очков
                }
                else if (hit.collider.gameObject == board_1 || hit.collider.gameObject == board_2)
                {
                    _white_chips[_i + number, 0] = _white_chips[_i, _j];
                    _white_chips[_i, _j] = null;
                    board[_i + number - 12, 0] = 2;
                    _i = _i + number;
                    _j = 0;
                    PositionUpWhite(_i);
                    PositionWhiteHome();
                }
                else
                {
                    for (int i = 0; i < 24; i++)
                    {
                        for (int j = 0; j < 15; j++)
                        {
                            if (hit.collider.gameObject == _white_chips[i, j])
                            {
                                _white_chips[i, j + 1] = _white_chips[_i, _j];
                                _white_chips[_i, _j] = null;
                                board[i - 12, j + 1] = 2;
                                PositionUpWhite(i);
                                PositionWhiteHome();
                            }
                        }
                    }
                }
            }
        }
    }

    //Число с кости 
    private void NumberOfDice()
    {

        number_1 = 0;
        number_2 = 0;
        Dice1 dice1 = dice_1.GetComponent<Dice1>();
        number_1 = dice1._diceValue1;
        Dice2 dice2 = dice_2.GetComponent<Dice2>();
        number_2 = dice2._diceValue2;
        if (number_1 == 0 || number_2 == 0)
        {
            return;
        }
        moveDice = true;
        if (moveBlack == true)
        {
            moveBlack = false;
        }
        else
        {
            moveBlack = true;
        }
        Logic();
    }

    //Передвидение позиции, к которым перемещаются шашки 
    public void PositionUpBlack(int i)
    {
        if (i <= 11)
        {
            _position_chps_black[i].transform.position += Vector3.right * 1.616f;
        }
        else if (i >= 12)
        {
            _position_chps_black[i].transform.position -= Vector3.right * 1.616f;
        }
    }

    public void PositionDownBlack(int i)
    {
        if (i <= 11)
        {
            _position_chps_black[i].transform.position -= Vector3.right * 1.616f;
        }
        else if (i >= 12)
        {
            _position_chps_black[i].transform.position += Vector3.right * 1.616f;
        }
    }
    public void PositionUpWhite(int i)
    {
        if (i <= 11)
        {
            _position_chps_white[i].transform.position -= Vector3.right * 1.616f;
        }
        else if (i >= 12)
        {
            _position_chps_white[i].transform.position += Vector3.right * 1.616f;
        }
    }

    public void PositionDownWhite(int i)
    {
        if (i <= 11)
        {
            _position_chps_white[i].transform.position += Vector3.right * 1.616f;
        }
        else if (i >= 12)
        {
            _position_chps_white[i].transform.position -= Vector3.right * 1.616f;
        }
    }


    //Количество ходов
    private void Logic()
    {
        if (number_1 == number_2)
        {
            move = 4;
            _dice_1 = true;
            _dice_2 = true;
        }
        else
        {
            move = 2;
            _dice_1 = true;
            _dice_2 = true;
        }
    }
    private void DiceLight()
    {
        if (move == 0)
        {
            dice_1_light.SetActive(true);
            dice_2_light.SetActive(true);
        }
        else
        {
            dice_1_light.SetActive(false);
            dice_2_light.SetActive(false);
        }
    }

    void MuvingForPositionfBlack()
    {
        _now = Vector3.Distance(objPosition, startPos);
        if (_dice_1 == false || move < 1)
        {
            _pos_1 = 10000;
        }
        if (_dice_2 == false || move < 1)
        {
            _pos_2 = 10000;
        }
        if (move < 2)
        {
            _pos_sum = 10000;
        }
        if (move != 4)
        {
            _pos_sum_x2 = 10000;
        }
        finale = Mathf.Min(_now, _pos_1, _pos_2, _pos_sum, _pos_sum_x2);
        if (_now == finale || move == 0 || (_dice_1 == false && _dice_2 == false))
        {
            _black_chips[_i, _j].transform.position = startPos;
        }
        else if (_pos_1 == finale && _dice_1 == true && move >= 1)
        {
            MoveBlackChips(pos_1, number_1);
            _dice_1 = false;
            if (number_1 == number_2)
            {
                if (move >= 1)
                {
                    _dice_1 = true;
                }
                else
                {
                    _dice_1 = false;
                }
            }
            move = move - 1;
            EndTurnCheck();
        }
        else if (_pos_2 == finale && _dice_2 == true && number_1 != number_2 && move >= 1)
        {
            MoveBlackChips(pos_2, number_2);
            move = move - 1;
            EndTurnCheck();
            _dice_2 = false;
        }
        else if (_pos_sum == finale && move >= 2)
        {
            MoveBlackChips(pos_sum, (number_1 + number_2));
            move = move - 2;
            EndTurnCheck();
        }
        else if (_pos_sum_x2 == finale && move == 4)
        {
            MoveBlackChips(pos_sum_x2, (number_2 * 4));
            move = move - 4;
            EndTurnCheck();
        }
        anus = false;
    }



    void MuvingForPositionfWhite()
    {
        _now = Vector3.Distance(objPosition, startPos);
        if (_dice_1 == false || move < 1)
        {
            _pos_1 = 10000;
        }
        if (_dice_2 == false || move < 1)
        {
            _pos_2 = 10000;
        }
        if (move < 2)
        {
            _pos_sum = 10000;
        }
        if (move != 4)
        {
            _pos_sum_x2 = 10000;
        }

        finale = Mathf.Min(_now, _pos_1, _pos_2, _pos_sum, _pos_sum_x2);
        if (_now == finale || move == 0 || (_dice_1 == false && _dice_2 == false))
        {
            _white_chips[_i, _j].transform.position = startPos;
        }
        else if (_pos_1 == finale && _dice_1 == true && move >= 1)
        {
            MoveWhiteChips(pos_1, number_1);
            _dice_1 = false;
            if (number_1 == number_2)
            {
                if (move >= 1)
                {
                    _dice_1 = true;
                }
                else
                {
                    _dice_1 = false;
                }
            }
            move = move - 1;
            EndTurnCheck();
        }
        else if (_pos_2 == finale && _dice_2 == true && number_1 != number_2 && move >= 1)
        {
            MoveWhiteChips(pos_2, number_2);
            move = move - 1;
            EndTurnCheck();
            _dice_2 = false;
        }
        else if (_pos_sum == finale && move >= 2)
        {
            MoveWhiteChips(pos_sum, (number_1 + number_2));
            move = move - 2;
            EndTurnCheck();
        }
        else if (_pos_sum_x2 == finale && move == 4)
        {
            MoveWhiteChips(pos_sum_x2, (number_2 * 4));
            move = move - 4;
            EndTurnCheck();
        }
        anus = false;
    }

    public void EndTurnCheck()
    {
        if(move == 0)
        {
            RollDice();
        }
    }
    public GameObject[] Dices;
    public void RollDice()
    {
        Dices[0].GetComponent<Dice1>().AutoDicePush();
        Dices[1].GetComponent<Dice2>().AutoDicePush();
    }

    private void ChipsLight()
    {
        if (moveBlack == true)
        {
            if (_i + number_1 < 24)
            {
                if (board[_i + number_1, 0] != 2 && _dice_1 == true && move >= 1)
                {
                    lightPos1Black = _i + number_1;
                }
                else
                {
                    lightPos1Black = -1;
                }
            }
            else
            {
                lightPos1Black = -1;
            }
            if (_i + number_2 < 24)
            {
                if (board[_i + number_2, 0] != 2 && _dice_2 == true && move >= 1)
                {
                    lightPos2Black = _i + number_2;
                }
                else
                {
                    lightPos2Black = -1;
                }
            }
            else
            {
                lightPos2Black = -1;
            }
            if (_i + (number_1 + number_2) < 24)
            {
                if (board[_i + (number_1 + number_2), 0] != 2 && move >= 2)
                {
                    lightPosSumBlack = _i + number_1 + number_2;
                }
                else
                {
                    lightPosSumBlack = -1;
                }
            }
            else
            {
                lightPosSumBlack = -1;
            }
            if (_i + (number_2 * 4) < 24)
            {
                if (board[_i + (number_2 * 4), 0] != 2 && move >= 2)
                {
                    lightPosSumX2Black = _i + (number_2 * 4);
                }
                else
                {
                    lightPosSumX2Black = -1;
                }
            }
            else
            {
                lightPosSumX2Black = -1;
            }
        }
        else if (moveBlack == false)
        {
            int corecter;
            if (_i + number_1 < 12)
            {
                corecter = 12;
                if (board[_i + number_1 + corecter, 0] != 1 && _dice_1 == true && move >= 1)
                {
                    lightPos1White = _i + number_1;
                }
                else
                {
                    lightPos1White = -1;
                }
            } 
            else if (_i + number_1 > 11)
            {
                corecter = -12;
                if (_i + number_1 + corecter < 24)
                {
                    if (board[_i + number_1 + corecter, 0] != 1 && _dice_1 == true && move >= 1)
                    {
                        lightPos1White = _i + number_1;
                    }
                    else
                    {
                        lightPos1White = -1;
                    }
                }
                else
                {
                    lightPos1White = -1;
                }
                
            }
            if (_i + number_2 < 12)
            {
                corecter = 12;
                if (board[_i + number_2 + corecter, 0] != 1 && _dice_2 == true && move >= 1)
                {
                    lightPos2White = _i + number_2;
                }
                else
                {
                    lightPos2White = -1;
                }
            }
            else if (_i + number_2 > 11)
            {
                corecter = -12;
                if (_i + number_2 + corecter < 24)
                {
                    if (board[_i + number_2 + corecter, 0] != 1 && _dice_1 == true && move >= 1)
                    {
                        lightPos2White = _i + number_2;
                    }
                    else
                    {
                        lightPos2White = -1;
                    }
                }
                else
                {
                    lightPos2White = -1;
                }
            }
            if (_i + number_1 + number_2 < 12)
            {
                corecter = 12;
                if (board[_i + number_1 + number_2 + corecter, 0] != 1 && move >= 2)
                {
                    lightPosSumWhite = _i + number_1 + number_2;
                }
                else
                {
                    lightPosSumWhite = -1;
                }
            }
            else if (_i + number_1 + number_2 > 11)
            {
                corecter = -12;
                if (_i + number_1 + number_2 + corecter < 24)
                {
                    if (board[_i + number_1 + number_2 + corecter, 0] != 1 && move >= 2)
                    {
                        lightPosSumWhite = _i + number_1 + number_2;
                    }
                    else
                    {
                        lightPosSumWhite = -1;
                    }
                }
                else
                {
                    lightPosSumWhite = -1;
                }
            }
            if (_i + (number_2 * 4) < 12)
            {
                corecter = 12;
                if (board[_i + (number_2 * 4) + corecter, 0] != 1 && move >= 2)
                {
                    lightPosSumX2White = _i + number_1 + number_2;
                }
                else
                {
                    lightPosSumX2White = -1;
                }
            }
            else if (_i + (number_2 * 4) > 11)
            {
                corecter = -12;
                if (_i + (number_2 * 4) + corecter < 24)
                {
                    if (board[_i + (number_2 * 4) + corecter, 0] != 1 && move >= 2)
                    {
                        lightPosSumX2White = _i + (number_2 * 4);
                    }
                    else
                    {
                        lightPosSumX2White = -1;
                    }
                }
                else
                {
                    lightPosSumX2White = -1;
                }
            }
        }
    }
    //Проверяет, находятся ли черные шашки дома
    void PositionBlackHome()
    {
        int home = 0;
        for (int i = 18; i < 24; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                if (board[i, j] == 1)
                {
                    home++;
                }
                if (home == 15)
                {
                    blackToHome = true;
                }
            }
        }
    }

    //Проверяет, находятся ли белые шашки дома
    void PositionWhiteHome()
    {
        int home = 0;
        for (int i = 6; i < 12; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                if (board[i, j] == 2)
                {
                    home++;
                }
                if (home == 15)
                {
                    blackToHome = true;
                }
            }
        }
    }
    public void finalStepBlack()
    {
        finale_Steps_Black.transform.position += Vector3.right * 1.616f;
    }
    public void finalStepWhite()
    {
        finale_Steps_White.transform.position -= Vector3.right * 1.616f;
    }

    void ChoiseChips()
    {
        if (moveBlack == true)
        {
            for (int i = 0; i < 24; i++)
            {
                if (board[i, 0] == 1)
                {
                    if (blackToHome != true)
                    {
                        if (i + number_1 < 24 && dice_1 == true)
                        {
                            if (board[i + number_1, 0] != 2)
                            {
                                Debug.LogError(number_1);
                                break;
                            }
                        }
                        if (i + number_2 < 24 && dice_2 == true)
                        {
                            if (board[i + number_2, 0] != 2)
                            {
                                Debug.LogError(number_2);
                                break;
                            }
                        }
                        if (i + number_2 + number_1 < 24 && move == 2)
                        {
                            if (board[i + number_2 + number_1, 0] != 2)
                            {
                                Debug.LogError(number_2 + number_1);
                                break;
                            }
                        }
                        if (move == 4)
                        {
                            if (i + (number_2 * 4) < 24)
                            {
                                if (board[i + (number_2 * 4), 0] != 2)
                                {
                                    break;
                                }
                            }
                        }
                        move = 0;
                        EndTurnCheck();
                        NumberOfDice();
                    }
                    if (blackToHome == true)
                    {
                        if (i + number_1 > 24 && dice_1 == true)
                        {
                            break;
                        }
                        if (i + number_2 > 24 && dice_2 == true)
                        {
                            break;
                        }
                        if (i + number_2 + number_1 > 24 && move == 2)
                        {
                            break;
                        }
                        if (move == 4)
                        {
                            if (i + (number_2 * 4) > 24)
                            {
                                break;
                            }
                        }
                        move = 0;
                        EndTurnCheck();
                        NumberOfDice();
                    }
                }
            }
        }
        else if (moveBlack == false)
        {
            for (int i = 0; i < 24; i++)
            {
                if (board[i, 0] == 2)
                {
                    if (whiteToHome != true)
                    {
                        int correct;
                        if (i + number_1 < 12 && dice_1 == true)
                        {
                            correct = 12;
                            if (board[i + number_1 + correct, 0] != 1)
                            {
                                break;
                            }
                        }
                        if (i + number_2 < 12 && dice_2 == true)
                        {
                            correct = 12;
                            if (board[i + number_2 + correct, 0] != 1)
                            {
                                break;
                            }
                        }
                        if (i + number_2 + number_1 < 12 && move == 2)
                        {
                            correct = 12;
                            if (board[i + number_2 + number_1 + correct, 0] != 1)
                            {
                                break;
                            }
                        }
                        if (move == 4)
                        {
                            if (i + (number_2 * 4) < 12)
                            {
                                correct = 12;
                                if (board[i + (number_2 * 4) + correct, 0] != 1)
                                {
                                    break;
                                }
                            }
                        }
                        if (i + number_1 > 11)
                        {
                            correct = -12;
                            if (board[i + number_1 + correct, 0] != 1 && dice_1 == true)
                            {
                                break;
                            }
                        }
                        if (i + number_2 > 11)
                        {
                            correct = -12;
                            if (board[i + number_2 + correct, 0] != 1 && dice_2 == true)
                            {
                                break;
                            }
                        }
                        if (i + number_2 + number_1 > 11)
                        {
                            correct = -12;
                            if (board[i + number_2 + number_1 + correct, 0] != 1 && move == 2)
                            {
                                break;
                            }
                        }
                        if (move == 4)
                        {
                            if (i + (number_2 * 4) > 11)
                            {
                                correct = -12;
                                if (board[i + (number_2 * 4) + correct, 0] != 1)
                                {
                                    break;
                                }
                            }
                        }
                        move = 0;
                        EndTurnCheck();
                        NumberOfDice();
                    }
                    if (whiteToHome == true)
                    {
                        if (i + number_1 >= 24 && dice_1 == true)
                        {
                            break;
                        }
                        if (i + number_2 >= 24 && dice_2 == true)
                        {
                            break;
                        }
                        if (i + number_2 + number_1 >= 24 && move == 2)
                        {
                            break;
                        }
                        if (move == 4)
                        {
                            if (i + (number_2 * 4) >= 24)
                            {
                                break;
                            }
                        }
                        move = 0;
                        EndTurnCheck();
                        NumberOfDice();
                    }
                }
            }
        }
    }
    void Finish()
    {
        if (scoreBlack == 15)
        {
            //выводить сообщение, все шашки переенесены в клоаку
        }
        if (scoreBlack == 15)
        {
            //выводить сообщение, все шашки переенесены в клоаку
        }
    }
    void BigDimasChips()
    {
        //Вызывается в Update
        if (moveBlack == true)
        {
            //Ход черных шашек, которых ты сделал белыми 
        }
        else if (moveBlack == false)
        {
            //Ход белых 
        }
    }
}