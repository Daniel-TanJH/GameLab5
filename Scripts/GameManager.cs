using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : Singleton<GameManager>
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


    public static GameManager Instance
    {
        get { return _instance;}
    }

    //To ensure it stays between scenes
    override public void Awake (){
        base.Awake();
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        //otherwise, this is the first time the instance is created
        _instance = this;
        DontDestroyOnLoad(this.gameObject); //Root game object only
    }
}
