using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskMovement : MonoBehaviour
{

    private const float SPEEDUPCOEF = 1.5f;
    public Animator animator;
    private float scrollSpeed = 1f;
    private float rotationSpeed = 50f;
    private bool direction = false;
    private Vector3 endpoint;

    private Score scoreScript;

    // Start is called before the first frame update
    void Start()
    {
         //scoreScript = scoreText.GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsOutScene()){
            gameObject.SetActive(false);
            if(gameObject.tag == "Infected")
                 scoreScript.UpdateScorePenalty(5 - (int)gameObject.transform.localScale.x);
        }
            

            
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

    public void SetRotation(bool newDir){
        direction = newDir;
    }

    public void SetScrollSpeed(float speed){
        scrollSpeed = speed;
    }

    public void SpeedUp(){
        scrollSpeed *= SPEEDUPCOEF;
        rotationSpeed *= SPEEDUPCOEF;
    }

    public void Cure(){
        animator.SetTrigger("Cure");
    }

    public void setScoreSpcript(Score script){
        scoreScript = script;
    }

}
