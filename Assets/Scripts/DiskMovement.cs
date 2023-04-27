using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskMovement : MonoBehaviour
{

    public Animator animator;
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
        if(IsOutScene())
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

    private bool IsOutScene(){
        if(transform.position.x + transform.localScale.x/2 < endpoint.x)
            return true;
        else
            return false;
    }

    public float GetRotationSpeed(){
        return rotationSpeed;
    }

    public bool GetDirection(){
        return direction;
    }

    public float GetScrollSpeed(){
        return scrollSpeed;
    }

    public Vector3 GetPosition(){
        return transform.position;
    }

    public void SetEndPoint(Vector3 pos)
    {
        endpoint = pos;
    }

    public void Cure(){
        animator.SetTrigger("Cure");
    }

}
