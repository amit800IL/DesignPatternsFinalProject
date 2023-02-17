using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

public class ControllerTTT : MonoBehaviour
{
    //Controller
    //-------------------------

    //Singleton instance
    public static ControllerTTT instance;
    //-------------------------

    Model model = new Model();

    [SerializeField] List<GameObject> Positions;

    public GameObject[,] positionGrid = new GameObject[3, 3];

    Stack<GameStateMemento> gameStateMementos = new Stack<GameStateMemento>();
    Stack<GameStateMemento> nextGameStateMementos = new Stack<GameStateMemento>();

    public int winningPlayer = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //intentionally allow destroy on load so itll be easy to reload scene
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        InitBoard();
    }

    private void Update()
    {
        ViewManager.instance.UpdateView(model);
        if(CheckForWin())
        {
            if(winningPlayer == 1)
            {
                model.Player2Score++;
            }
            if(winningPlayer == 2)
            {
                model.Player1Score++;
            }
            ViewManager.instance.ShowPlayerWinText(winningPlayer);
            winningPlayer = 0;
            
            ResetGrid();
        }
        else if(GridFull())
        {
            ResetGrid();
        }
    }

    public bool CheckForWin()
    {
        int[,] grid = model.GetGridState();

        for (int i = 0; i < 3; i++)
        {
            if (grid[i, 0] == grid[i, 1] && grid[i, 1] == grid[i, 2] && grid[i, 0] != 0)
            {
                winningPlayer = grid[i, 0];
                return true;
            }
            if (grid[0, i] == grid[1, i] && grid[1, i] == grid[2, i] && grid[0, i] != 0)
            {
                winningPlayer = grid[0, i];
                return true;
            }
        }

        if (grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2] && grid[0, 0] != 0)
        {
            winningPlayer = grid[1, 1];
            return true;
        }

        if (grid[0, 2] == grid[1, 1] && grid[1, 1] == grid[2, 0] && grid[0, 2] != 0)
        {
            winningPlayer = grid[1, 1];
            return true;
        }

        winningPlayer = 0;
        return false;
    }

    public bool GridFull()
    {
        foreach(var i in model.gridState)
        {
            if(i == 0)
            {
                return false;
            }
        }
        return true;
    }

    void InitBoard()
    {
        int[,] newGrid = new int[3, 3];
        int posIndex = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                positionGrid[i, j] = Positions[posIndex];
                newGrid[i, j] = 0;
                posIndex++;
            }
        }
        model.SetGridState(newGrid);
        AddMemento();
    }

    void ResetGrid()
    {
        gameStateMementos.Clear();
        nextGameStateMementos.Clear();

        int[,] newGrid = new int[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                newGrid[i, j] = 0;
            }
        }
        model.SetGridState(newGrid);
        AddMemento();
    }


    //add shape to the grid
    public void PlaceShape(GameObject position)
    {
        int[,] newGrid = model.GetGridState();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (positionGrid[i, j].Equals(position) && model.gridState[i, j] == 0)
                {
                    newGrid[i, j] = (model.Turn % 2) + 1;
                    model.SetTurn(model.Turn + 1);
                }
                else if(positionGrid[i, j].Equals(position) && model.gridState[i, j] != 0)
                {
                    return;
                }
            }
        }
        AddMemento();
    }

    public GameStateMemento CreateMemento()
    {

        return new GameStateMemento(model.Turn, model.CloneGridState());
    }

    //adds memento (saves current state)
    public void AddMemento()
    {
        nextGameStateMementos.Clear();
        gameStateMementos.Push(CreateMemento());
    }


    //undo
    public void PreviousMemento()
    {
        print("UNDO");
        if(gameStateMementos.Count <= 1)
        {
            return;
        }
        nextGameStateMementos.Push(CreateMemento());
        gameStateMementos.Pop();
        model.SetGridState(gameStateMementos.Peek().GetGridState());
        model.SetTurn(gameStateMementos.Peek().turn);

    }


    //redo if possible (not last in list)
    public void NextMemento()
    {
        print("REDO");
        if (nextGameStateMementos.Count == 0)
        {
            return;
        }
        gameStateMementos.Push(nextGameStateMementos.Pop());
        model.SetGridState(gameStateMementos.Peek().GetGridState());
        model.SetTurn(gameStateMementos.Peek().turn);
    }


    [ContextMenu("print all mementos")]
    public void PrintAllMementos()
    {
        foreach(var memento in gameStateMementos)
        {
            print("new memento");
            foreach(var num in memento.GetGridState())
            {
                print(num);
            }
        }
    }

}
