using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskMovement : MonoBehaviour
{

    public float scrollSpeed = 1f;
    public float rotationSpeed = 50f;
    public bool direction = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x - (Time.deltaTime * scrollSpeed), transform.position.y);
        if(direction)
            transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        else
            transform.RotateAround(transform.position, Vector3.back, rotationSpeed * Time.deltaTime);
    }


    public float getRotationSpeed(){
        return rotationSpeed;
    }

    public bool getDirection(){
        return direction;
    }

    public float getScrollSpeed(){
        return scrollSpeed;
    }

    public Vector3 getPosition(){
        return transform.position;
    }
}
