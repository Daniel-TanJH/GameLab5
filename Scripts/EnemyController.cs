using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameConstants gameConstants;
    private float originalX;
    //private float maxOffset = 5.0f; // max distance before changing direction
    //private float enemyPatroltime = 2.0f;
    private int moveRight;
    private Vector2 velocity;
    private bool mariodead = false;
    private SpriteRenderer enemySprite;
    private Rigidbody2D enemyBody;
    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();
        // get the starting position
        originalX = transform.position.x;
        moveRight = Random.Range(0,2) == 0 ? -1 : 1;
        ComputeVelocity();
        GameManager.onPlayerDeath += EnemyRejoice;
    }

    void Update()
    {
      if (Mathf.Abs(enemyBody.position.x - originalX) < gameConstants.maxOffset)
      {
        // move gomba
        MoveGomba();
      }
      else{
        // change direction
        moveRight *= -1;
        ComputeVelocity();
        MoveGomba();
      }

    }

    void ComputeVelocity(){
        velocity = new Vector2((moveRight)* (Random.Range(4.0f, 10.0f)) / gameConstants.enemyPatroltime, 0);
    }
    void MoveGomba(){
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }
    // Update is called once per frame
    
    void OnTriggerEnter2D(Collider2D other)
    {
      if (other.gameObject.CompareTag("Player"))
      {
        float yoffset = (other.transform.position.y - this.transform.position.y);
        if (yoffset > 0.9f)
        {
          Debug.Log("Enemy DEAD!");
          KillSelf();
        }
        else 
        {
          //Debug.Log("Mario DEAD!");
          CentralManager.centralManagerInstance.damagePlayer();
        }
      }
      if (other.gameObject.CompareTag("Obstacle"))
      {
        moveRight *= -1;
        ComputeVelocity();
        MoveGomba();
      }
    }

    void EnemyRejoice()
    {
      GetComponent<Animator>().SetBool("mariodead", true);
      velocity = Vector3.zero;
      GameManager.onPlayerDeath -= EnemyRejoice; //Ensure next instance does not trigger this instantly
    }

    //Start flatten > Pancake > End of Flatten
    void KillSelf()
    {
      CentralManager.centralManagerInstance.increaseScore();
      //CentralManager.centralManagerInstance.spawnFromPooler(0);
      StartCoroutine(flatten());
      Debug.Log("Pancake");
    }

    IEnumerator flatten()
    {
      Debug.Log("Start flatten");
      int steps = 5;
      float stepper = 1.0f/(float) steps;

      for (int i=0; i<steps; i++)
      {
        this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y - stepper, this.transform.localScale.z);
        //Make sure the enemy is above ground
        this.transform.position = new Vector3(this.transform.position.x, (float)gameConstants.groundSurface + GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
        yield return null;
      }
      Debug.Log("End of Flatten");
      this.gameObject.SetActive(false);
      Debug.Log("EnemyController: Enemy back to pool");
      yield break;
    }

    
}
