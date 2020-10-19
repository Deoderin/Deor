using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System;

public class SaveGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _gen;
    public GameObject saveDice1;
    public GameObject saveDice2;
    int statsWhite;
    int statsBlack;
    Quaternion diceRotation1;
    Quaternion diceRotation2;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SaveFile();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            GameManager.LoadNewGame();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            LoadSave();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteAll();
        }
    }
    void SaveFile()
    {
        Generator gen = _gen.GetComponent<Generator>();
        if (gen.dice_1 == true)
        {
            PlayerPrefs.SetInt("boolSave1", 1);
            PlayerPrefs.SetInt("valueSave1", gen.number_1);
        }
        else
        {
            PlayerPrefs.SetInt("boolSave1", 0);
            PlayerPrefs.SetInt("valueSave1", 0);
        }
        if (gen.dice_2 == true)
        {
            PlayerPrefs.SetInt("boolSave2", 1);
            PlayerPrefs.SetInt("valueSave2", gen.number_2);
        }
        else
        {
            PlayerPrefs.SetInt("boolSave2", 0);
            PlayerPrefs.SetInt("valueSave2", 0);
        }
        //diceRotation1 = saveDice1.transform.rotation;
        //diceRotation2 = saveDice2.transform.rotation;
        PlayerPrefs.SetInt("statsBlack", gen.scoreBlack);
        PlayerPrefs.SetInt("statsWhite", gen.scoreWhite);
        PlayerPrefs.SetInt("moveSave", gen.move);
        PlayerPrefs.SetInt("moveBlack", Convert.ToInt32(gen.moveBlack));

        for (int i = 0; i < 24; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                PlayerPrefs.SetInt("saveBoard" + i + j, gen.board[i,j]);
                //Debug.LogError(PlayerPrefs.GetInt("saveBoard" + i + j));
            }
        }
        PlayerPrefs.SetFloat("Dice1.x", saveDice1.transform.rotation.x);
        PlayerPrefs.SetFloat("Dice1.y", saveDice1.transform.rotation.y);
        PlayerPrefs.SetFloat("Dice1.z", saveDice1.transform.rotation.z);
        PlayerPrefs.SetFloat("Dice1.w", saveDice1.transform.rotation.w);
        PlayerPrefs.SetFloat("Dice2.x", saveDice2.transform.rotation.x);
        PlayerPrefs.SetFloat("Dice2.y", saveDice2.transform.rotation.y);
        PlayerPrefs.SetFloat("Dice2.z", saveDice2.transform.rotation.z);
        PlayerPrefs.SetFloat("Dice2.w", saveDice2.transform.rotation.w);

        PlayerPrefs.SetFloat("Dice1.xp", saveDice1.transform.position.x);
        PlayerPrefs.SetFloat("Dice1.yp", saveDice1.transform.position.y);
        PlayerPrefs.SetFloat("Dice1.zp", saveDice1.transform.position.z);
        PlayerPrefs.SetFloat("Dice2.xp", saveDice2.transform.position.x);
        PlayerPrefs.SetFloat("Dice2.yp", saveDice2.transform.position.y);
        PlayerPrefs.SetFloat("Dice2.zp", saveDice2.transform.position.z);

        PlayerPrefs.Save();
    }
    void LoadSave()
    {
        saveDice1.GetComponent<Rigidbody>().useGravity = false;
        saveDice2.GetComponent<Rigidbody>().useGravity = false;

        saveDice1.GetComponent<Rigidbody>().isKinematic = true;
        saveDice2.GetComponent<Rigidbody>().isKinematic = true;

        float Dice1X = PlayerPrefs.GetFloat("Dice1.x");
        float Dice1Y = PlayerPrefs.GetFloat("Dice1.y");
        float Dice1Z = PlayerPrefs.GetFloat("Dice1.z");
        float Dice1W = PlayerPrefs.GetFloat("Dice1.w");
        float Dice2X = PlayerPrefs.GetFloat("Dice2.x");
        float Dice2Y = PlayerPrefs.GetFloat("Dice2.y");
        float Dice2Z = PlayerPrefs.GetFloat("Dice2.z");
        float Dice2W = PlayerPrefs.GetFloat("Dice2.w");

        saveDice1.transform.position = new Vector3(PlayerPrefs.GetFloat("Dice1.xp"), PlayerPrefs.GetFloat("Dice1.yp"), PlayerPrefs.GetFloat("Dice1.zp"));
        saveDice1.transform.rotation = new Quaternion(Dice1X, Dice1Y, Dice1Z, Dice1W); 
        saveDice2.transform.position = new Vector3(PlayerPrefs.GetFloat("Dice2.xp"), PlayerPrefs.GetFloat("Dice2.yp"), PlayerPrefs.GetFloat("Dice2.zp"));
        saveDice2.transform.rotation = new Quaternion(Dice2X, Dice2Y, Dice2Z, Dice2W);

        saveDice1.GetComponent<Rigidbody>().useGravity = true;
        saveDice2.GetComponent<Rigidbody>().useGravity = true;

        saveDice1.GetComponent<Rigidbody>().isKinematic = false;
        saveDice2.GetComponent<Rigidbody>().isKinematic = false;

        int k = 0;
        int q = 0;

        Generator gen = _gen.GetComponent<Generator>();
        gen.scoreBlack = PlayerPrefs.GetInt("statsBlack");
        gen.scoreWhite = PlayerPrefs.GetInt("statsWhite");
        gen.number_1 = PlayerPrefs.GetInt("valueSave1");
        gen.number_2 = PlayerPrefs.GetInt("valueSave2");
        gen.move = PlayerPrefs.GetInt("moveSave");
        statsBlack = gen.scoreBlack;
        statsWhite = gen.scoreWhite;
        if (PlayerPrefs.GetInt("moveBlack") == 1)
        {
            gen.moveBlack = true;
        }
        else
        {
            gen.moveBlack = false;
        }
        if (PlayerPrefs.GetInt("boolSave1") == 1)
        {
            gen._dice_1 = true;
        }
        else
        {
            gen._dice_1 = false;
        }
        if (PlayerPrefs.GetInt("boolSave2") == 1)
        {
            gen._dice_2 = true;
        }
        else
        {
            gen._dice_2 = false;
        }
        for (int i = 0; i < 24; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                gen.board[i,j] = PlayerPrefs.GetInt("saveBoard" + i + j);
            }
        }
        for (int m = 0; m < 15; m++)
        {
            gen.PositionDownBlack(0);
            gen.PositionDownWhite(0);
        }
        for (int i = 0; i < 24; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                if (gen.board[i, j] == 1 && k < 15)
                {
                    gen._black_chips[0, k].transform.position = new Vector3(gen._position_chps_black[i].gameObject.transform.position.x, -0.48f, gen._position_chps_black[i].gameObject.transform.position.z);
                    gen._black_chips[i, j] = gen._black_chips[0, k];
                    if (i != 0)
                    {
                        gen._black_chips[0, k] = null;
                    }
                    k++;
                    gen.PositionUpBlack(i);
                    //gen.PositionDownBlack(0);
                    if (statsBlack != 0)
                    {
                        gen._black_chips[0, k].transform.position = gen.finale_Steps_Black.transform.position;
                        gen._black_chips[0, k] = null;
                        statsBlack--;
                        k++;
                        gen.finalStepBlack();
                        //gen.PositionDownBlack(0);
                    }
                }
                if (gen.board[i, j] == 2 && q < 15)
                {
                    int correct;
                    if (i < 12)
                    {
                        correct = 12;
                        gen._white_chips[0, q].transform.position = new Vector3(gen._position_chps_white[i + correct].gameObject.transform.position.x, -0.48f, gen._position_chps_white[i + correct].gameObject.transform.position.z);
                        gen._white_chips[i + correct, j] = gen._white_chips[0, q];
                        if (i + correct != 0)
                        {
                            gen._white_chips[0, q] = null;
                        }
                        q++;
                        gen.PositionUpWhite(i + correct);
                        //gen.PositionDownWhite(0);
                        if (statsWhite != 0)
                        {
                            gen._white_chips[0, q].transform.position = gen.finale_Steps_White.transform.position;
                            gen._white_chips[0, q] = null;
                            statsWhite--;
                            q++;
                            gen.finalStepWhite();
                            //gen.PositionDownWhite(0);
                        }
                    }
                    if (i > 11)
                    {
                        correct = -12;
                        gen._white_chips[0, q].transform.position = new Vector3(gen._position_chps_white[i + correct].gameObject.transform.position.x, -0.48f, gen._position_chps_white[i + correct].gameObject.transform.position.z);
                        gen._white_chips[i + correct, j] = gen._white_chips[0, q];
                        if (i + correct != 0)
                        {
                            gen._white_chips[0, q] = null;
                        }
                        q++;
                        gen.PositionUpWhite(i + correct);
                        //gen.PositionDownWhite(0);
                        if (statsWhite != 0)
                        {
                            gen._white_chips[0, q].transform.position = gen.finale_Steps_White.transform.position;
                            gen._white_chips[0, q] = null;
                            statsWhite--;
                            q++;
                            gen.finalStepWhite();
                            //gen.PositionDownWhite(0);
                        }
                    }
                }
            }
        }
    }
}
