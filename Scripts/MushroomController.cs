using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MushroomController : MonoBehaviour
{
    public GameConstants gameConstants;
    private Rigidbody2D mushroomBody;
    private SpriteRenderer mushroomSprite;
    private Vector2 velocity;
    public bool keepMovingRight = true;
    public bool test = true;
    //private float mushroomSpeed = 5;
    private int moveRight = 1;
    


// Testing stuff, works without the physics
    // Start is called before the first frame update
    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();
        mushroomSprite = GetComponent<SpriteRenderer>();
        mushroomBody.AddForce(Vector2.up*20, ForceMode2D.Impulse);
        ComputeVelocity();

    }
    void ComputeVelocity(){
        velocity = new Vector2(moveRight*gameConstants.mushroomSpeed, 0);
    }
    void moveMushroom(){
        mushroomBody.MovePosition(mushroomBody.position + velocity*Time.fixedDeltaTime);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Constantly move mushroom. update of direction comes from collision
        moveMushroom();
    }
    
    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("Obstacle")){
            moveRight *= -1;
            ComputeVelocity();
            Debug.Log("Change direction!");
        }

        if (col.gameObject.CompareTag("Player")){
            moveRight = 0;
            ComputeVelocity();
            Debug.Log("Got Mario!");
        }
    }

    void OnBecomeInvisible (){
        Destroy(gameObject);
    }
    
}