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
    Stack<GameStateMemento> nextGameStateMemento = new Stack<GameStateMemento>();

    public GameObject WinText;


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
        ViewManager.instance.UpdateView(model.gridState);
        if(CheckForWin())
        {
            WinText.SetActive(true);
        }
        else
        {
            WinText.SetActive(false);
        }
    }

    public bool CheckForWin()
    {
        int[,] grid = model.GetGridState();

        for (int i = 0; i < 3; i++)
        {
            if (grid[i, 0] == grid[i, 1] && grid[i, 1] == grid[i, 2] && grid[i, 0] != 0)
            {
                return true;
            }
            if (grid[0, i] == grid[1, i] && grid[1, i] == grid[2, i] && grid[0, i] != 0)
            {
                return true;
            }
        }

        if (grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2] && grid[0, 0] != 0)
        {
            return true;
        }

        if (grid[0, 2] == grid[1, 1] && grid[1, 1] == grid[2, 0] && grid[0, 2] != 0)
        {
            return true;
        }

        return false;
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
        nextGameStateMemento.Clear();
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
        nextGameStateMemento.Push(CreateMemento());
        gameStateMementos.Pop();
        model.SetGridState(gameStateMementos.Peek().GetGridState());
        model.SetTurn(gameStateMementos.Peek().turn);

    }


    //redo if possible (not last in list)
    public void NextMemento()
    {
        print("REDO");
        if (nextGameStateMemento.Count == 0)
        {
            return;
        }
        gameStateMementos.Push(nextGameStateMemento.Pop());
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
