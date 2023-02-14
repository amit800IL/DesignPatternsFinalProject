using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerMovement player;
    public int scoreNum = 0;
    [SerializeField] Collectibles collectibles;
    [SerializeField] Transform PlayerTransform;
    [SerializeField] Transform spawnPointPosition;
    [SerializeField] TextMeshProUGUI ScoreText;


    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        ScoreText.text = "Score " + scoreNum.ToString();
    }

    //public void ResetScore()
    //{
    //    scoreNum = 0;
    //}

    public void OnPlayerDeath()
    {
        //ResetScore();
        //player.transform.position = spawnPointPosition.position;
        SceneManager.LoadScene("Runner");
    }


}
