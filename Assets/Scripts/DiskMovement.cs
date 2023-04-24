using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskMovement : MonoBehaviour
{

    public float scrollSpeed = 1f;
    public float rotationSpeed = 50f;
    public bool direction = false;
    private Vector3 endpoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isOutScene())
            gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x - (Time.deltaTime * scrollSpeed), transform.position.y);
        if(direction)
            transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        else
            transform.RotateAround(transform.position, Vector3.back, rotationSpeed * Time.deltaTime);
    }

    private bool isOutScene(){
        if(transform.position.x + transform.localScale.x/2 < endpoint.x)
            return true;
        else
            return false;
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

    public void setEndPoint(Vector3 pos)
    {
        endpoint = pos;
    }
}
