using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //public Transform enemyLocation;
    public float speed;
    private Rigidbody2D marioBody;
    public float maxSpeed=10;
    public float upSpeed=2;
    //private int score = 0;
    private bool onGroundState = true;
    private bool faceRightState = true;
    private bool countScoreState = false;
    private SpriteRenderer MarioSprite;
    private Animator marioAnimator;
    public ParticleSystem[] DustParticle;
    //public Text scoreText;
    private AudioSource marioAudio;
    private bool mariodead = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate =  30;
        marioBody = GetComponent<Rigidbody2D>();
        MarioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();
        DustParticle = GetComponentsInChildren  <ParticleSystem>();
        GameManager.onPlayerDeath += PlayerDiesSequence;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            CentralManager.centralManagerInstance.consumePowerup(KeyCode.Q, this.gameObject);
        }
        if (Input.GetKeyDown("e"))
        {
            CentralManager.centralManagerInstance.consumePowerup(KeyCode.E, this.gameObject);
        }

        Debug.Log(mariodead);
        /*if (Input.GetKeyDown("a") && faceRightState){
            faceRightState= false;
            MarioSprite.flipX = true;
        }
        
        if (Input.GetKeyDown("d") && !faceRightState){
            faceRightState = true;
            MarioSprite.flipX=false;
        }
        if (!onGroundState && countScoreState)
        {
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState = false;
                score++;
                Debug.Log(score);
            }
        }*/

    }

    void FixedUpdate()
{
    marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));

	float moveHorizontal = Input.GetAxis("Horizontal");
    if (Mathf.Abs(moveHorizontal) > 0){
        Vector2 movement = new Vector2(moveHorizontal, 0);
        if (marioBody.velocity.magnitude < maxSpeed)
                marioBody.AddForce(movement * speed);
    }
    if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
        // stop
        marioBody.velocity = Vector2.zero;
    }

    if (Input.GetKeyDown("space") && onGroundState){
        marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
        onGroundState = false;

        marioAnimator.SetBool("onGround", onGroundState);

        countScoreState = true;
    }

    if (Input.GetKeyDown("a") && faceRightState){
        faceRightState= false;
        MarioSprite.flipX = true;
        if (Mathf.Abs(marioBody.velocity.x) > 1.0){
            marioAnimator.SetTrigger("onSkid");
        }
    }
        
    if (Input.GetKeyDown("d") && !faceRightState){
            faceRightState = true;
            MarioSprite.flipX=false;
        if (Mathf.Abs(marioBody.velocity.x) > 1.0){
            marioAnimator.SetTrigger("onSkid");
        }
        }

    if (Input.GetKeyUp("space") && onGroundState){          
            foreach (ParticleSystem eachChild in DustParticle){
                if (eachChild.name == "Smoke"){
                    eachChild.Play();   
                }
            }

    }


}

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacle") ) 
        {
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
            /*countScoreState = false;
            scoreText.text = "Score: " + score.ToString();*/
            

        }
        //Check who dies!
        /*if (col.gameObject.CompareTag("Enemy")){
            if (marioBody.transform.position.y - col.gameObject.transform.position.y >=0.9){
                Debug.Log("SAFE");
            }
            else {
                Debug.Log("NOT SAFE");
            }
        }*/
        
    }

    void OnTriggerEnter2D(Collider2D other){
        /*if (other.gameObject.CompareTag("Enemy")){
            Time.timeScale = 0.0f;
        }*/
    }
    
    void PlayJumpSound(){
        marioAudio.PlayOneShot(marioAudio.clip);
    }


    void PlayerDiesSequence(){
        Debug.Log("MARIO DIES");
        //GetComponent<Animator>().SetBool("mariodead",true);
        GetComponent<Collider2D>().enabled = false;
        marioBody.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
        marioBody.SetRotation(180);
        marioBody.gravityScale = 1;
        StartCoroutine(dead());
        GameManager.onPlayerDeath -= PlayerDiesSequence;
    }

    IEnumerator dead(){
        yield return new WaitForSeconds(1.0f);
        marioBody.bodyType = RigidbodyType2D.Static;
    }
}