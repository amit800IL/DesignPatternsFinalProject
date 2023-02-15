using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

public class GameManagerTTT : MonoBehaviour
{
    //MODEL
    //-------------------------

    //Singleton instance
    public static GameManagerTTT instance;
    //-------------------------

    int turn = 1;
    float middleX = Screen.width / 2;
    float middleY = Screen.height/ 2;

    [SerializeField] List<GameObject> Positions;

    [SerializeField] GameObject[,] positionGrid = new GameObject[3, 3];
    [SerializeField] int[,] stateGrid = new int[3, 3];


    UnityEvent EndGame;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //intentionally destroy on load so itll be easy to reload scene
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitBoard();
    }

    private void Start()
    {
        foreach(var p in positionGrid)
        {
            print(p);
        }
    }

    void InitBoard()
    {
        int posIndex = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                positionGrid[i, j] = Positions[posIndex];
                stateGrid[i, j] = 0;
                posIndex++;
            }
        }

        
    }

    void CheckWin()
    {
        // win logic check
        EndGame.Invoke();
    }

}
