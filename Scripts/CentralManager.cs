using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralManager : MonoBehaviour
{
    public GameObject gameManagerObject;
    public GameObject powerupManagerObject;
    public GameObject spawnManagerObject;
    private GameManager gameManager;
    private PowerUpManager powerUpManager;
    public SpawnManager spawnManager;
    public static CentralManager centralManagerInstance;

    void Awake()
    {
        centralManagerInstance = this;
    }

    void Start()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        powerUpManager = powerupManagerObject.GetComponent<PowerUpManager>();  
    }

    public void increaseScore()
    {
        gameManager.increaseScore();
    }

    public void damagePlayer()
    {
        gameManager.damagePlayer();
    }
    //Powerup manager
    public void Powerup(){
        powerUpManager.powerup();
    }

    public void consumePowerup(KeyCode k, GameObject g)
    {
        powerUpManager.consumePowerup(k,g);
    }

    public void addPowerup(Texture t, int i, ConsumableInterface c)
    {
        Debug.Log("POWERRRRR");
        powerUpManager.addPowerup(t,i,c);
    }

    public void spawnFromPooler(ObjectType i){
        spawnManager.spawnFromPooler(i);
    }

    public void spawnTurtle() {
        spawnManager.spawnFromPooler(ObjectType.turtleEnemy);
    }

}