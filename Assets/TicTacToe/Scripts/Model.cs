using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Model
{
    int Turn { get; set; }
    int[,] gridState = new int[3,3];

    Stack<int[,]> previousStates = new Stack<int[,]>();
    Stack<int[,]> nextStates = new Stack<int[,]>();
    public int[,] GetGridState()
    {
        return gridState;
    }

    public void SetGridState()
    {
        gridState = GetGridState();
    }

    public int[,] GetPreviousState()
    {
        //nextStates.Push(previousStates.Peek());
        return previousStates.Pop();
    }
}
