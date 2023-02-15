using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    public static ViewManager instance;

    [SerializeField] GameObject TicTacPrefab;
    [SerializeField] GameObject ToePrefab;

    bool[,] activeShape = new bool[3, 3]; 

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
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                activeShape[i, j] = false;
            }
        }
    }

    public void UpdateView()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (GameManagerTTT.instance.stateGrid[i, j] != 0 && !activeShape[i,j])
                {
                    print("position state: " + GameManagerTTT.instance.stateGrid[i, j]);
                    Instantiate(InstShape(GameManagerTTT.instance.stateGrid[i, j]), GameManagerTTT.instance.positionGrid[i,j].transform);
                    activeShape[i, j] = true;
                }
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
