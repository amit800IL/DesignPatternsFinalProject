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

    //float middleX = Screen.width / 2;
    //float middleY = Screen.height/ 2;

    [SerializeField] List<GameObject> Positions;

    public GameObject[,] positionGrid = new GameObject[3, 3];


    //List<GameStateMemento> mementos = new List<GameStateMemento>();
    Stack<GameStateMemento> gameStateMementos = new Stack<GameStateMemento>();
    Stack<GameStateMemento> nextGameStateMemento = new Stack<GameStateMemento>();

    //int mementoIndex = -1;


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

        ////check if the list is longer than the current index
        //if (mementos.Count - 1 > mementoIndex)
        //{
        //    ////if list is longer then remove list components that are bigger than the mementoIndex
        //    //for (int i = mementoIndex + 1; i < mementos.Count; i++)
        //    //{
        //    //    mementos.RemoveAt(mementoIndex + 1);
        //    //}
        //    while(mementos.Count - 1 > mementoIndex)
        //    {
        //        print(mementos.Count);
        //        mementos.RemoveAt(mementoIndex + 1);
        //    }
        //}
        //mementos.Add(CreateMemento());
        //mementoIndex++;

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
        model.SetGridState(gameStateMementos.Peek().gridState);
        model.SetTurn(gameStateMementos.Peek().turn);

    }
        //if(mementoIndex == 0)
        //{
        //    return;
        //}
        //mementoIndex--;
        //model.SetGridState(mementos[mementoIndex].gridState);
        //model.SetTurn(mementos[mementoIndex].turn);

    //redo if possible (not last in list)
    public void NextMemento()
    {
        print("REDO");
        if (nextGameStateMemento.Count == 0)
        {
            return;
        }
        var nextMemento = nextGameStateMemento.Pop();
        gameStateMementos.Push(nextMemento);
        model.SetGridState(gameStateMementos.Peek().gridState);
        model.SetTurn(gameStateMementos.Peek().turn);
    }

        //if (mementoIndex == mementos.Count - 1)
        //{
        //    return;
        //}
        //model.SetGridState(mementos[mementoIndex + 1].gridState);
        //model.SetTurn(mementos[mementoIndex + 1].turn);
        //mementoIndex++;

    [ContextMenu("print all mementos")]
    public void PrintAllMementos()
    {
        foreach(var memento in gameStateMementos)
        {
            print("new memento");
            foreach(var num in memento.gridState)
            {
                print(num);
            }
        }
    }

    public enum PositionState
    {
        None,
        Toe,
        TicTac
    }

}
