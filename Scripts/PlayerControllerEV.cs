using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerControllerEV : MonoBehaviour
{
    public UnityEvent onPlayerDeath;
    public UnityEvent onEnemyDeath;
    private float force;
    public IntVariable marioUpSpeed;
    public IntVariable marioMaxSpeed;
    public GameConstants gameConstants;
    public Animator marioAnimator;
    public Rigidbody2D marioBody;
    private bool onGroundState = true;
    private bool faceRightState = true;
    private SpriteRenderer MarioSprite;
    private AudioSource marioAudio;
    public ParticleSystem[] DustParticle;
void Start()
{
    marioUpSpeed.SetValue(gameConstants.playerMaxJumpSpeed);
    marioMaxSpeed.SetValue(gameConstants.playerStartingMaxSpeed);
    force = gameConstants.playerDefaultForce;   
    MarioSprite = GetComponent<SpriteRenderer>();
}
 void FixedUpdate()
{
    marioAnimator.SetFloat("xmarioUpSpeed", Mathf.Abs(marioBody.velocity.x));

	float moveHorizontal = Input.GetAxis("Horizontal");
    if (Mathf.Abs(moveHorizontal) > 0){
        Vector2 movement = new Vector2(moveHorizontal, 0);
        if (marioBody.velocity.magnitude < marioMaxSpeed.Value)
                marioBody.AddForce(movement * marioUpSpeed.Value);
    }
    if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
        // stop
        marioBody.velocity = Vector2.zero;
    }

    if (Input.GetKeyDown("space") && onGroundState){
        marioBody.AddForce(Vector2.up * (marioUpSpeed.Value-10), ForceMode2D.Impulse);
        onGroundState = false;

        marioAnimator.SetBool("onGround", onGroundState);

        //countScoreState = true;
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

    public void OnCollisionEnter2D(Collision2D col)
    {
    if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacle") ) 
    {
        Debug.Log("TOUCH");
        onGroundState = true;
        marioAnimator.SetBool("onGround", onGroundState);
    }   
}

    public void PlayerDiesSequence(){
        Debug.Log("MARIO DIES");
        GetComponent<Animator>().SetBool("mariodead",true);
        GetComponent<Collider2D>().enabled = false;
        marioBody.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
        marioBody.SetRotation(180);
        marioBody.gravityScale = 1;
        StartCoroutine(dead());
    }

    IEnumerator dead(){
        yield return new WaitForSeconds(1.0f);
        marioBody.bodyType = RigidbodyType2D.Static;
    }
}
