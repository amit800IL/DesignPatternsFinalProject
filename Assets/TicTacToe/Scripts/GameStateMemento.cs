using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMemento
{
    public int turn { get;}
    private int[,] gridState;

    public GameStateMemento(int turn, int[,] gridState)
    {
        this.turn = turn;
        this.gridState = gridState;
    }

    public int[,] GetGridState()
    {
        return (int[,])gridState.Clone();
    }
}
