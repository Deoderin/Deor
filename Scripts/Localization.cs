using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Localization : MonoBehaviour
{
    public static string Play = "������";
    public static string GameResult = "���������� ����";
    public static string NewGame = "����� ����";
    public static string OpenCombination = "������� ���";

    public static string RulePage1 = "� ��������� ������������ �������� 136 ������. � ����� ��� ������� �� 2 ����: ����� (�����) � ������ (������� � �����). ������ 3 �����: ����, ������� � �������. ������ ����� ������� �� ���� �� 1 �� 9. ������� ����� (1 � 9) ���������� �������������. ������ � ��� �������(�������, ����� � �������) � ����� (������, ��, ����� � �����). ������ ����� �� 4 ���������� �����, ������� ������ ��� �����.";
    public static string RulePage2 = "������ ������ 4 ������ (���������, �����, ��������, ��������). � ������ ������ 2 ������� (����). �� ���� ����� �������� 8 ������. ���� ������� ������ � ������ ������� ������� ������� � ����� ������ ���� � ���� ���� ���������� ������. �������� �����:\n<b>���</b> � ��� (������������������) �� ���� ������ ����� �����. ��� �� ����� ���� �� �������� ��� ������. \n<b>����</b> � ��� ���������� �����. \n<b>����</b> � ������ ���������� �����. ����� ������ ��������� �� ���� ����� � ���� ���� ���������� ������ ���� �������.";
    public static string RulePage3 = "- ���� �� ������� ����. ������� ������� ������ ������������ ����� ������, �� ���� ������ ������� ������� ������ �������� ����� �� -> ����� -> �����. ����� ������ ��������� ����� � ��� �����. � ������ ����� ����������� �����, ��������� 14 ������ ������� ���������� ����� ������. ��� ����� �������� <b>������� ������</b>. ������� ���������� ������, ���� ������� ����� ������� - ����� ������� �� ��������, ���� �� ��������, ����� ��������� ������ ������� �������.";
    public static string RulePage4 = "������ ��� ����� �������� ����� �� �����, ����� ���� ������ �������� ���� ����� � ����� �����. ���� ���������� ����� ��������� ������ ������� ����������, �� ����� ����� �� �� �����. ���������� ��������� ����� ������� ���������� ������� ���� �������. �������� ���������� ���� ����� ������ �����. ������� �������� <b>���</b> ����� ������ �� ������ ����������� �� ���� ���� �� ���� ������. ��� ����� ��������� <b>����'�</b> ������� �������������� �����.";
    public static string RulePage5 = "���� 3 ������� ������� ���� (������ ������ ��������� ����������):\n<b>1)</b> � ���� ��� ���� �������� ����. ������ ����� � ���� ��� ���������� �������� ������� �����. �� ����� ��� ����� � ������������ �������� ����.\n<b>2)</b> � ���������� ���� ������ �������� ����. � ���� ��� �� ����� �� ������������� ����� �� �����. ������������� ��� ����� � ������ ��������� �����.\n<b>3)</b> ����� ��������� � �������� ���� ��� �������� � ����� ������� ��� ����,����� ������ �������� ���. ���� ����� �� ������� �������� ����, �� � ����� ������� �� ����� �������� ��� �������� ����.";
    public static string RulePage6 = "� ������, �������� �� ������� ����� ����� ��� ���������� ��������, ����������� �������� ����. ����� ������������ ������ �������� ��������� ������� � �������� �������. ����� ����� �������� ����������� �� ����� ����, ���� ���� ���� �������� ��� ���� ����������. ����� �������� ������ ����, ��������� � ������� �������� ��������� �����, � ������ � ����� �������� �������. ���� ����� �����������, � �������� ������ ������� �����, � ����� �� ����� ������� �������, �� ����������� �����.";
    public static string RulePage7 = "���� ���������� � 2000 ������. ��� ������������ ����� ������� �������. ������ �� ����������� �������� ������ ���������� ������ ��������� ��� ����. ����� ����������� �������������� ����� �����. ������ �������������� � ����� ������� �� �����������. ��� � ���� ������ ����� ������ ����, � ���� ������ �����, �������. ��� ���� �������������� �����-������ �������� ��� ������ � ��� ���� ������ �����������.";
    public static string RulePage8 = "1. ���     2. ���� ����� ������     3. ���� ������ ������    4. ���� ��������     \n5. ���� �� ������ (����� 1, 9)     6. ���� ������������     7. ���� ������/��������     \n8. ���� �� ������ (����� 1, 9)     9.���� ������������      10.���� ������/��������     \n11. �� �������   (����� �� ������� ����� �� ����� � 2 ����) \n<b>�������� � ����/����</b>   12/17. ��������     13/16. ����� ������     14/15. ������ ������  \n<b>��� ����� ����� �����:</b>   18.� ��������� � �������     19.��� �������� � ������";
    public static string RulePage9 = "\n������� �� �����, �������� ��������� �� 4-� � ����� ����\n������������ � ������ � ������ ����������, ������� ����\n������� ������ �� ��������� ����� ����� ������� ������\n�������� ���� �� ������ �������\n��������� ���������� �����\n��� ���� � ���� ��������\n��������� �������� ����\n��� ����� �� �������\n��� ��� ��� �����\n������ �����\n\n<b>������� �������� ����� ��� ����������</b>\n��� ���� � ���� ������";
    public static string RulePage10 = "\n<b>��� ��� ���� 500 �����.</b>\n<b>��������� ���������</b>: ��� ���, ��� ����� ����� �����, ��� ���������� ��������\n<b>����� ����������</b>: ������� �� ������ � ��������\n<b>������ � ������</b>: ������ �� ������������ (1 � 9)\n<b>������������� ������</b>: ��� �����/����� � ���� �������\n<b>�������� ��������</b>: ���� ��� �������� � ������ ��� ���� ��� ����� �����\n<b>���� ������� ������</b>: �����/����� ���� ��������\n<b>������ ����������� ����� � ���� �����</b>: �����/����� ���� ������ � ����\n<b>������ �� ������</b>: ������ ����� � ����\n<b>���������� ����� �����</b>: 1 � 9 ���� ������, �� ������ ���� �������� � ������, ���� ���� � ����� �� ���\n<b>������� ���� �� ��� ����</b>: ������� �� ��������� ����� ����� ������� ������ � ��� 1 �����";
    public static string Title1 = "���� ������. ";
    public static string Title2 = "���� ����. ";
    public static string Title3 = "�����. ";
    public static string Title4 = "��� ����. ";
    public static string Title5 = "C��� �����. ";
    public static string Title6 = "�������� ����. ";
    public static string Title7 = "����. ";
    public static string Title8 = "";
    public static string Title9 = "�������� ����� ��� ����������. ";
    public static string Title10 = "����������� ����������. ";
    public static string StartT = "�����";
    public static string Carcassonne = "�������";

    public static string AlarmDiscardPutSet = "�������� ������ ���, ����� ������� ���� �� ���������� �����.";

    public static string InyourHand = "� ����� ����";
    public static string Scores = " ����� ";
    public static string WinText = "��� ������ ������ ��� ��������� ����� ����";
    public static string WinText2 = "��� ������ ������ ��� ������� ��������� ����� ����, ���-��� �� ��������� �����";

    public static string LoseText = "�� ������� ";
    public static string Also = "�����, ";
    public static string LoseTextGet = "��� ������ ";
    //public static string LoseText = "�� ��������";

    public static string[] Player = new string[4] { "������� �����", "����� �����", "������ �����", "������� �����" };
    public static string[] Players = new string[4] { " �������� ������", " ������ ������", " ������� ������", " �������� ������" };
    public static bool Init;

    public Text[] ConnectButtons;
    public Text Start;
    public Text PlayB;
    public Text GameName;
    public Text GameNameShadow;

    private void Awake()
    {
        Init = false;
        InitLanguage();
    }

    private void Update()
    {
        if (Init)
        {
            InitLanguage();
            Init = false;
        }
    }
    public void InitLanguage()
    {

        //foreach (Text t in ConnectButtons)
        //{
        //    //t.text = Connect;
        //}
        if(PlayB != null)
        {
            PlayB.text = Play;
        }
        if (Start != null)
        {
            Start.text = StartT;
        }
        if (GameName != null)
        {
            GameName.text = Carcassonne;
        }
        if (GameNameShadow != null)
        {
            GameNameShadow.text = Carcassonne;
        }
    
   
       
    }
    public static void InitStrings(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, string s11, string s12, string s13, string s14, string s15, string s16, string s17, string s18, string s19,string s20, string s21, string s22, string s23, string s24, string s25, string s26, string s27, string s28, string s29, string s30, string s31, string s32, string s33, string s34, string s35, string s36, string s37, string s38, string s39, string s40, string s41)
    {

        Play = s1;
        GameResult = s2;
        NewGame = s3;
        InyourHand = s4;
        Scores = s5;
     
        RulePage1 = s6.Replace("(NEWLINE)","\n");
        RulePage2 = s7.Replace("(NEWLINE)", "\n");
        RulePage3 = s8.Replace("(NEWLINE)", "\n");
        RulePage4 = s9.Replace("(NEWLINE)", "\n");
        RulePage5 = s10.Replace("(NEWLINE)", "\n");
        RulePage6 = s11.Replace("(NEWLINE)", "\n");
        RulePage7 = s12.Replace("(NEWLINE)", "\n");
        RulePage8 = s13.Replace("(NEWLINE)", "\n");
        RulePage9 = s14.Replace("(NEWLINE)", "\n");
        RulePage10 = s15.Replace("(NEWLINE)", "\n");
        Title1 = s16;
        Title2 = s17;
        Title3 = s18;
        Title4 = s19;
        Title5 = s20;
        Title6 = s21;
        Title7 = s22;
        Title8 = s23;
        Title9 = s24;
        Title10 = s25;
        StartT = s26;
        Carcassonne = s27;
        LoseTextGet = s28;
        AlarmDiscardPutSet = s29;
        LoseText = s30;
        Also = s31;
        WinText = s32;
        WinText2 = s33;

        Player = new string[4] { s34, s35, s36, s37 };
        Players = new string[4] { s38, s39, s40, s41 };


        Init = true;
    }
}
