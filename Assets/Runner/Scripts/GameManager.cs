using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerMovement player;
    public Collectibles collectibles;
    public Transform PlayerTransform;
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
        player.transform.position = spawnPointPosition.position;
    }


}
