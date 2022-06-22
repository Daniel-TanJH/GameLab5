using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    private  Rigidbody2D rigidBody;
    private  Vector3 scaler;
    private GameObject Debries;
    // Start is called before the first frame update
    void Start()
    {
        for (int x =  0; x<5; x++){
            Instantiate(Debries, transform.position, Quaternion.identity);
        }
        // we want the object to have a scale of 0 (disappear) after 30 frames. 
        scaler  =  transform.localScale  / (float) 30 ;
        rigidBody  =  GetComponent<Rigidbody2D>();
        StartCoroutine("ScaleOut");        
        Debug.Log("Couroutine");
    }   

    // Update is called once per frame


    IEnumerator  ScaleOut(){

        Vector2 direction =  new  Vector2(Random.Range(-1.0f, -1.0f), 1);
        rigidBody.AddForce(direction.normalized  *  10, ForceMode2D.Impulse);
        rigidBody.AddTorque(10, ForceMode2D.Impulse);
        // wait for next frame
        yield  return  null;

        // render for 0.5 second
        for (int step =  0; step  < 30; step++)
        {
            this.transform.localScale  =  this.transform.localScale  -  scaler;
            // wait for next frame
            yield  return  null;
        }


    }
    void Update(){
        
    }
}
