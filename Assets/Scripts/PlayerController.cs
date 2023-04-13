using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform pivot;
    public float startSpeed = 30f;
    public float jumpSpeed = 2f;
    private bool start = true;
    private bool direction = true;
    private bool isJump = false;

    private const float ROTATION_TOP = 0.57f;
    private const float ROTATION_BOT = -0.57f;

    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown("space")){
            isJump = true;
            start = false;
        }
            

        if(isJump)
            Jump();

        if(start)
            StartFunction();
            
        
    }

    void StartFunction()
    {
        if(transform.rotation.z >= ROTATION_TOP)
            direction = false;  
        if(transform.rotation.z <= ROTATION_BOT)
            direction = true;    
                
        if(direction)
            transform.RotateAround(pivot.position, Vector3.forward, startSpeed * Time.deltaTime);  
        else
            transform.RotateAround(pivot.position, Vector3.back, startSpeed * Time.deltaTime);  
    }

    void Jump(){
        transform.position += transform.right * Time.deltaTime * jumpSpeed;
    }
}
