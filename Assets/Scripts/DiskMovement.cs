using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskMovement : MonoBehaviour
{

    public float scrollSpeed = 1f;
    public float rotationSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x - (Time.deltaTime * scrollSpeed), transform.position.y);
        transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        
    }
}
