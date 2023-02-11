using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerMovement playerMovement;
    public int scoreNum = 0;

    public void ResetScore()
    {
        scoreNum = 0;
    }

    public void OnPlayerDeath()
    {
        ResetScore();
        //playerMovement.transform.position = spawnPointTransform.position;
    }

}
