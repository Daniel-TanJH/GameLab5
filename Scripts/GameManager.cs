using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Text score;
    private int playerScore=0;
    private static GameManager _instance; //Singleton pattern
    public delegate void gameEvent();
    public static event gameEvent onPlayerDeath;

    public void increaseScore()
    {
        Debug.Log("Score Up");
        playerScore += 1;
        score.text = "SCORE: " + playerScore.ToString();
    }

    public void damagePlayer()
    {
        onPlayerDeath();
    }

}