using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Transform pivot;
    public Transform topLimit;
    public Transform botLimit;
    public Animator animator;
    public float startSpeed = 30f;
    public float jumpSpeed = 2f;
    private bool start = true;
    private bool direction = true;
    private bool isJump = false;

    private const float ROTATION_TOP = 0.57f;
    private const float ROTATION_BOT = -0.57f;

    private float rotateSpeed = 0f;
    private float scrollSpeed = 0f;
    private Vector3 diskPosition;
    private float xSize;
    private float ySize;

    // Start is called before the first frame update
    void Start()
    {
        xSize = GetComponent<Collider2D>().bounds.size.x;
        ySize = GetComponent<Collider2D>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown("space")){
            isJump = true;
            start = false;
        }
            
        if(!isJump)
            diskPosition = new Vector2(diskPosition.x - (Time.deltaTime * scrollSpeed), diskPosition.y);

        if(isOut())
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);


    }

    void FixedUpdate()
    {
        if(isJump)
            Jump();
        else
            RotateAroundDisk();

        if(start)
            StartFunction();

    }

    private void RotateAroundDisk()
    {
        transform.position = new Vector2(transform.position.x - (Time.deltaTime * scrollSpeed), transform.position.y);
        if(direction)
            transform.RotateAround(diskPosition, Vector3.forward, rotateSpeed * Time.deltaTime);  
        else
            transform.RotateAround(diskPosition, Vector3.back, rotateSpeed * Time.deltaTime);
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

    bool isOut(){
        if(transform.position.x + xSize < pivot.position.x)
            return true;
        if(transform.position.x + ySize < pivot.position.x)
            return true;

        if(transform.position.y - ySize > topLimit.position.y)
            return true;

        if(transform.position.y + ySize < botLimit.position.y)
            return true;

        if(transform.position.x - ySize > topLimit.position.x)
            return true;

        
        return false;
    }

    public void AfterCollisionAnimation(){
        Vector2 currentDirection = transform.right;
        Vector2 hitNormal = (transform.position - diskPosition).normalized;
        transform.rotation = Quaternion.FromToRotation(transform.right, hitNormal) * transform.rotation;
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        animator.SetTrigger("DrugCollision");
        isJump = false;
        
        GameObject other = collision.gameObject;
        DiskMovement dM = collision.gameObject.GetComponent<DiskMovement>();
        rotateSpeed = dM.getRotationSpeed();
        direction = dM.getDirection();
        diskPosition = dM.getPosition();
        scrollSpeed = dM.getScrollSpeed();    
    }

    public float GetXSize(){
        return xSize;
    }
}
