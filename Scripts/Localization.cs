using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Localization : MonoBehaviour
{
    public static string Play = "Играть";
    public static string GameResult = "РЕЗУЛЬТАТЫ ИГРЫ";
    public static string NewGame = "НОВАЯ ИГРА";
    public static string OpenCombination = "Открыть сет";

    public static string RulePage1 = "В китайском классическом маджонге 136 костей. В целом они делятся на 2 типа: масти (цифры) и козыри (драконы и ветра). Мастей 3 штуки: доты, бамбуки и символы. Каждая масть состоит из цифр от 1 до 9. Крайние кости (1 и 9) называются терминальными. Козыри — это драконы(красный, белый и зеленый) и ветра (восток, юг, запад и север). Каждой кости по 4 одинаковых штуки, поэтому костей так много.";
    public static string RulePage2 = "Партия длится 4 раунда (Восточный, Южный, Западный, Северный). В каждом раунде 2 раздачи (кона). То есть всего играется 8 раздач. Цель каждого игрока в каждой раздаче собрать маджонг — любые четыре сета и одну пару одинаковых костей. Варианты сетов:\n<b>Чоу</b> — ряд (последовательность) из трех костей одной масти. Чоу не может быть на драконах или ветрах. \n<b>Панг</b> — три идентичных кости. \n<b>Конг</b> — четыре идентичных кости. Любые четыре сочетания из этих сетов и одна пара идентичных костей дают маджонг.";
    public static string RulePage3 = "- одна из механик игры. Вначале каждого раунда определяется игрок Восток, от него против часовой стрелки игроки получают ветра Юг -> Запад -> Север. Ветер игрока изображен рядом с его рукой. В центре стола формируется стена, последние 14 костей которой обозначают ветер раунда. Эти кости называют <b>мертвой стеной</b>. Вначале следующего раунда, если победил игрок Востока - ветра игроков не меняются, если он проиграл, ветра смещаются против часовой стрелки.";
    public static string RulePage4 = "Каждый ход игрок получает кость со стены, после чего должен сбросить одну кость в центр стола. Если сброшенная кость позволяет игроку собрать комбинацию, он может взять ее со стола. Комбинация собранная таким образом становится открыта всем игрокам. Открытые комбинации дают вдвое меньше очков. Собрать открытый <b>Чоу</b> можно только из сброса предыдущего по ходу игры от себя игрока. При сборе открытого <b>Конг'а</b> берется дополнительная кость.";
    public static string RulePage5 = "Есть 3 способа собрать конг (каждый способ визуально отличается):\n<b>1)</b> В руке уже есть закрытый панг. Другой игрок в свой ход сбрасывает четвёртую такуюже кость. Вы берёте эту кость и выкладываете открытый конг.\n<b>2)</b> В предыдущие ходы собран открытый панг. В свой ход вы берёте со стенычетвёртую такую же кость. Прикладываете эту кость к своему открытому пангу.\n<b>3)</b> Чтобы собранный в закрытую конг был засчитан в конце раздачи как конг,игрок должен объявить его. Если игрок не объявит закрытый конг, то в конце раздачи он будет засчитан как закрытый панг.";
    public static string RulePage6 = "У игрока, которому не хватает одной кости для завершения маджонга, обьявляется просящая рука. Любая изподходящих костей позволит завершить маджонг и выиграть раздачу. Игрок может ограбить прокачанный из панга конг, если этот тайл является для него выигрышным. Можно ограбить только конг, собранный с помощью апгрейда открытого панга, и только с целью выиграть раздачу. Если стена закончилась, и осталась только мертвая стена, а никто не успел собрать маджонг, то обьявляется ничья.";
    public static string RulePage7 = "Игра начинается с 2000 очками. Они отображаются ввиде счетных палочек. Каждый из проигравших отдельно платит победителю полную стоимость его руки. Затем проигравшие рассчитываются между собой. Каждый рассчитывается с двумя другими по отдельности. Тот у кого меньше очков платит тому, у кого больше очков, разницу. При всех взаиморасчётах игрок-Восток получает или платит в два раза больше положенного.";
    public static string RulePage8 = "1. Чоу     2. Пара своих ветров     3. Пара ветров раунда    4. Пара драконов     \n5. Панг на мастях (кроме 1, 9)     6. Панг терминальных     7. Панг ветров/драконов     \n8. Конг на мастях (кроме 1, 9)     9.Конг терминальных      10.Конг ветров/драконов     \n11. За выигрыш   (Кость на выигрыш взята со стены – 2 очка) \n<b>УДВОЕНИЯ с Панг/конг</b>   12/17. Драконов     13/16. Своих ветров     14/15. Ветров раунда  \n<b>Все кости одной масти:</b>   18.с драконами и ветрами     19.без драконов и ветров";
    public static string RulePage9 = "\nМаджонг на кости, очевидно последней из 4-х в своем роде\nТерминальные и козыри в каждой комбинации, включая пару\nМаджонг собран на последней кости перед мертвой стеной\nПросящая рука на первой раздаче\nСовершено ограбление конга\nДва сета и пара драконов\nПолностью закрытая рука\nНет очков за раздачу\nДва или три конга\nТолько панги\n\n<b>Двойное удвоение очков для победителя</b>\nТри сета и пара ветров";
    public static string RulePage10 = "\n<b>Все они дают 500 очков.</b>\n<b>Найденное сокровище</b>: нет чоу, все кости одной масти, все комбинации закрытые\n<b>Свита императора</b>: маджонг на ветрах и драконах\n<b>Головы и хвосты</b>: мажонг на терминальных (1 и 9)\n<b>Императорский нефрит</b>: все панги/конги и пара зеленые\n<b>Небесные близнецы</b>: семь пар драконов и ветров или семь пар одной масти\n<b>Трое великих ученых</b>: панги/конги всех драконов\n<b>Четыре наслаждения вошли в твою дверь</b>: панги/конги всех ветров и пара\n<b>Четыре по четыре</b>: четыре конга и пара\n<b>Тринадцать чудес Света</b>: 1 и 9 всех мастей, по одному всех драконов и ветров, одна пара к любой из них\n<b>Достать луну со дна моря</b>: маджонг на последней кости перед мертвой стеной и это 1 дотов";
    public static string Title1 = "Виды костей. ";
    public static string Title2 = "ЦЕЛЬ ИГРЫ. ";
    public static string Title3 = "Ветра. ";
    public static string Title4 = "Ход игры. ";
    public static string Title5 = "Cбор конга. ";
    public static string Title6 = "Просящая рука. ";
    public static string Title7 = "Очки. ";
    public static string Title8 = "";
    public static string Title9 = "Удвоение очков для победителя. ";
    public static string Title10 = "Легендарные комбинации. ";
    public static string StartT = "Старт";
    public static string Carcassonne = "Маджонг";

    public static string AlarmDiscardPutSet = "Положите плитку так, чтобы собрать один из подходящих сетов.";

    public static string InyourHand = "В вашей руке";
    public static string Scores = " очков ";
    public static string WinText = "Все игроки платят вам стоимость вашей руки";
    public static string WinText2 = "Все игроки платят вам двойную стоимость вашей руки, так-как вы Восточный ветер";

    public static string LoseText = "Вы платите ";
    public static string Also = "Также, ";
    public static string LoseTextGet = "вам платит ";
    //public static string LoseText = "Их получает";

    public static string[] Player = new string[4] { "Красный игрок", "Синий игрок", "Желтый игрок", "Зеленый игрок" };
    public static string[] Players = new string[4] { " Красному игроку", " Синему игроку", " Желтому игроку", " Зеленому игроку" };
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
