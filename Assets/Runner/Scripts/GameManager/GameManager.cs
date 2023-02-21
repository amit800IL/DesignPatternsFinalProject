using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerMovement player;

    public int scoreNum = 0;
    [SerializeField] Collectibles collectibles;
    [SerializeField] Transform PlayerTransform;
    [SerializeField] Transform spawnPointPosition;
    [SerializeField] TextMeshProUGUI ScoreText;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        ScoreText.text = "Score " + scoreNum.ToString();
    }

    public void OnPlayerDeath()
    {
        SceneManager.LoadScene("Runner");
    }


}
