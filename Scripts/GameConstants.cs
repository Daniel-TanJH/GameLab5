using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    //for scoring system
    int currentScore;
    int currentPlayerHealth;
    //Mario start value from lab5
    public int playerStartingMaxSpeed = 5;
    public int playerMaxJumpSpeed = 10;
    public int playerDefaultForce = 100;

    public float groundDistance = -3.5f; //where is the ground in the y axis

    //for reset values
    public Vector3 gombaSpawnPointStart = new Vector3(2.5f, -0.45f, 0); //test and change?

    //for consume?
    public int comsumeTimeStep = 10;
    public double groundSurface = -3.5;
    
    //EnemyController.cs
    public float maxOffset = 5.0f;
    public float enemyPatroltime = 2.0f;

    //red and orange mushroom
    public float mushroomSpeed = 5;
}