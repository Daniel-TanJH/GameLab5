using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;
    public Transform endLimit; // Indicates end of map
    private float offset; // x-offset between camera and mario
    private float startX; 
    private float endX;
    private float viewportHalfwidth;
    // Start is called before the first frame update
    void Start()
    {
        //Get coordinate of bottomleft for camera
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        viewportHalfwidth = Mathf.Abs(bottomLeft.x- this.transform.position.x);
        offset = this.transform.position.x - player.position.x;
        startX = this.transform.position.x;
        endX = endLimit.transform.position.x - viewportHalfwidth;
    }

    // Update is called once per frame
    void Update()
    {
        float desiredX = player.position.x + offset;
        //Check if desiredX is within startX and endX
        if (desiredX > startX && desiredX < endX){
            this.transform.position = new Vector3(desiredX, this.transform.position.y, this.transform.position.z);
        }
    }
}
