using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private const float SPEEDUPCOEF = 1.5f;

    public Transform pivot;
    public Transform topLimit;
    public Transform botLimit;
    public Animator animator;
    public float startSpeed = 30f;
    public float jumpSpeed = 2f;
    private bool start = true;
    private bool direction = true;
    private bool isJump = false;
    private bool canJump = true;

    private const float ROTATION_TOP = 0.57f;
    private const float ROTATION_BOT = -0.57f;

    private float rotateSpeed = 0f;
    private float scrollSpeed = 0f;
    private Vector3 diskPosition;
    private float xSize;
    private float ySize;

    private bool turnClock = false;
    private bool turnCounterClock = false;
    private float  rotatePower = 100f;

    public GameObject scoreText;
    private Score scoreScript;

    // Start is called before the first frame update
    void Start()
    {
        xSize = GetComponent<Collider2D>().bounds.size.x;
        ySize = GetComponent<Collider2D>().bounds.size.y;
        scoreScript = scoreText.GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {

        if(canJump && Input.GetKeyDown("space")){
            canJump = false;
            isJump = true;
            start = false;
            turnClock = false;
            turnCounterClock = false;
        }
            
        if(!isJump && !start){
            if(Input.GetKeyDown(KeyCode.D))
                turnClock = true;
            if(Input.GetKeyUp(KeyCode.D))
                turnClock = false;
            
            if(Input.GetKeyDown(KeyCode.A))
                turnCounterClock = true;
            if(Input.GetKeyUp(KeyCode.A))
                turnCounterClock = false;

            diskPosition = new Vector2(diskPosition.x - (Time.deltaTime * scrollSpeed), diskPosition.y);

        }

        if(isOut())
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);


    }

    void FixedUpdate()
    {
        if(isJump)
            Jump();
        else{
            RotateAroundDisk();
            if(canJump){
                if(turnClock)
                    transform.RotateAround(diskPosition, Vector3.back, rotatePower * Time.deltaTime);
                else if(turnCounterClock)
                    transform.RotateAround(diskPosition, Vector3.forward, rotatePower * Time.deltaTime); 
            }
                   
        }
            

        if(start)
            StartFunction();

    }

    private void RotateAroundDisk()
    {
        transform.position = new Vector2(transform.position.x - (Time.deltaTime * scrollSpeed), transform.position.y);
        if(direction)
            transform.RotateAround(diskPosition, Vector3.forward, rotateSpeed  * Time.deltaTime);
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

        if(transform.position.y - xSize > topLimit.position.y)
            return true;

        if(transform.position.y + xSize < botLimit.position.y)
            return true;

        if(transform.position.x - xSize > topLimit.position.x)
            return true;

        
        return false;
    }

    public void AfterCollisionAnimation(){
        Vector2 currentDirection = transform.right;
        Vector2 hitNormal = (transform.position - diskPosition).normalized;
        transform.rotation = Quaternion.FromToRotation(transform.right, hitNormal) * transform.rotation;
        canJump = true;   
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Infected"){
            collision.gameObject.tag = "Cured";
            animator.SetTrigger("DrugCollision");
            isJump = false;
            
            DiskMovement dM = collision.gameObject.GetComponent<DiskMovement>();
            dM.Cure();
            scoreScript.UpdateScore(5 - (int)collision.gameObject.transform.localScale.x);
            rotateSpeed = dM.GetRotationSpeed();
            direction = dM.GetDirection();
            diskPosition = dM.GetPosition();
            scrollSpeed = dM.GetScrollSpeed();    
        }
        
    }

    public void SpeedUp(){
        scrollSpeed *= SPEEDUPCOEF;
        jumpSpeed *= SPEEDUPCOEF;
        rotatePower *= SPEEDUPCOEF;
    }

    public float GetXSize(){
        return xSize;
    }
}
