using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerMovement playerMovement;
    public Collectibles collectibles;
    public Transform Player;
    public Transform spawnPointPosition;
    public int scoreNum = 0;
    public TextMeshProUGUI ScoreText;


    private void Start()
    {
        instance = this;
    }

    private void Update()
    {

        ScoreText.text = "Score " + scoreNum.ToString();
    }


    public void ResetScore()
    {
        scoreNum = 0;
    }

    public void OnPlayerDeath()
    {
        ResetScore();
        playerMovement.transform.position = spawnPointPosition.position;
    }


}
