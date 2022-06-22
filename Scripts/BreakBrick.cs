using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrick : MonoBehaviour
{
    bool broken = false;
    private GameObject Debris;
    private  Rigidbody2D rigidBody;
    private  Vector3 scaler;
    // Start is called before the first frame update
    void Start()
    {
        Debris = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Player") && !broken){
            broken = true;

            for (int x =  0; x<5; x++){
            Instantiate(Debris, transform.position, Quaternion.identity);
        }
            // set the number of debris
            //Sprite poofs and no longer hittable
            gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<EdgeCollider2D>().enabled = false;
            Debug.Log("Box should be destroyed");
        }
    }

    
}
