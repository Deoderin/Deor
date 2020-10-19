using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public enum GameState
    {
        StartGame,
        Roll,
        Turn,
        Dragging,
        ScoreShower,
    }
    public static GameState State;
    public static GM GameManager;

    // Start is called before the first frame update
    void Start()
    {
        State = GameState.StartGame;
        GameManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
