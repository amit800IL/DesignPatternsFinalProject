using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewManager : MonoBehaviour
{
    //also a singleton to make sure only one thing controls the view
    public static ViewManager instance;

    [SerializeField] GameObject TicTacPrefab;
    [SerializeField] GameObject ToePrefab;
    [SerializeField] TextMeshProUGUI PlayerWinsText;
    [SerializeField] TextMeshProUGUI Player1ScoreText;
    [SerializeField] TextMeshProUGUI Player2ScoreText;


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

    }

    public void UpdateView(Model model)
    {
        //update grid view
        int[,] grid = model.CloneGridState();
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
            }
        }

        //update score
        Player1ScoreText.text = "Player 1: " + model.Player1Score;
        Player2ScoreText.text = "Player 2: " + model.Player2Score;
    }

    public void ShowPlayerWinText(int player)
    {
        float timer = 3f;
        if(player == 1)
        {
            PlayerWinsText.text = "Player " + 2 + "Wins";
            PlayerWinsText.gameObject.SetActive(true);
            while(timer > 0)
            {
                timer -= Time.deltaTime;
            }
        }
        if(player == 2)
        {
            PlayerWinsText.text = "Player " + 1 + "Wins";
            PlayerWinsText.gameObject.SetActive(true);
            while (timer > 0)
            {
                timer -= Time.deltaTime;
            }
        }
        PlayerWinsText.gameObject.SetActive(false);
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
