using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMemento
{
    public int turn { get; private set; }
    public int[,] gridState { get; private set; }

    public GameStateMemento(int turn, int[,] gridState)
    {
        this.turn = turn;
        this.gridState = gridState;
    }

}
