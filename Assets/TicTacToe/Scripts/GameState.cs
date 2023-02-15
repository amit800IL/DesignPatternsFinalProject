using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    int turn;
    int[,] gridState;

    public GameState(int turn, int[,] gridState)
    {
        this.turn = turn;
        this.gridState = gridState;
    }

}
