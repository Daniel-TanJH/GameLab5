using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxController : MonoBehaviour
{
    public CentralManager centralManager;
    public SpawnManager spawnManager;
    public Rigidbody2D rigidBody;
    public SpringJoint2D springJoint;
    public GameObject consummablePrefab;
    public SpriteRenderer spriteRenderer;
    public Sprite usedQuestionBox;
    private bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("Player") && !hit){
            centralManager.increaseScore();
            hit = true;

            rigidBody.AddForce(new Vector2(0, rigidBody.mass*20), ForceMode2D.Impulse);

            spawnManager.spawnFromPooler(ObjectType.gombaEnemy);
            //Gives it the animation of appearing out of box
            Instantiate(consummablePrefab, new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z), Quaternion.identity);
            StartCoroutine(DisableHittable()); //executed till yield
        }
    }
    //Check if the box is coming to a stop
    bool ObjectMovedAndStopped(){
        return Mathf.Abs(rigidBody.velocity.magnitude)<0.01;
    }

    IEnumerator DisableHittable(){
        if (!ObjectMovedAndStopped()){
            yield return new WaitUntil(() => ObjectMovedAndStopped());
        }

        spriteRenderer.sprite = usedQuestionBox; //change sprite
        rigidBody.bodyType = RigidbodyType2D.Static; //change from dynamic to static

        this.transform.localPosition = Vector3.zero; //Box back to position before being hit
        springJoint.enabled = false; //no more spring!
    }
}
