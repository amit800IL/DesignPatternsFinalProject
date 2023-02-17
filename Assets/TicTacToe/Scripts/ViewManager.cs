using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    //also a singleton to make sure only one thing controls the view
    public static ViewManager instance;

    [SerializeField] GameObject TicTacPrefab;
    [SerializeField] GameObject ToePrefab;

    //bool[,] activeShape = new bool[3, 3];
    GameObject[,] gameObjects = new GameObject[3, 3];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitView();
    }

    private void InitView()
    {
        //for (int i = 0; i < 3; i++)
        //{
        //    for (int j = 0; j < 3; j++)
        //    {
        //        activeShape[i, j] = false;
        //    }
        //}
    }

    public void UpdateView(int[,] grid)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (grid[i,j] == 0 && gameObjects[i,j] != null)
                {
                    Destroy(gameObjects[i, j]);
                }
                else if (grid[i,j] != 0 && gameObjects[i,j] == null)
                {
                    gameObjects[i, j] = Instantiate(InstShape(grid[i, j]), ControllerTTT.instance.positionGrid[i, j].transform);
                }


                //if (grid[i, j] != 0 && !activeShape[i,j])
                //{
                //    print("position state: " + ControllerTTT.instance.stateGrid[i, j]);
                //    Instantiate(InstShape(ControllerTTT.instance.stateGrid[i, j]), ControllerTTT.instance.positionGrid[i,j].transform);
                //    activeShape[i, j] = true;
                //}
            }
        }
    }

    private GameObject InstShape(int state)
    {
        switch(state)
        {
            case 1:
                return ToePrefab;
            case 2:
                return TicTacPrefab;
            default: 
                return null;
        }
    }
}
