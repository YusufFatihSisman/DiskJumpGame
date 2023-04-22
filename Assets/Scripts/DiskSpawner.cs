using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskSpawner : MonoBehaviour
{

    public Transform endPoint;
    public GameObject diskPrefab;
    public Transform TopLimit;
    public Transform BottomLimit;
    private float bottomSize = 1f;
    private float topSize = 4f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateDisk(5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator CreateDisk(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            float size = Random.Range(bottomSize, topSize);
            float pos = Random.Range(BottomLimit.position.y, TopLimit.position.y);
            if(pos + size/2 > TopLimit.position.y)
                pos = TopLimit.position.y - size/2;
            if(pos - size/2 < BottomLimit.position.y)
                pos = BottomLimit.position.y + size/2;

            GameObject newObject = Instantiate(diskPrefab, new Vector2(TopLimit.position.x+(size/2), pos), Quaternion.identity);
            Debug.Log(endPoint.position);
            newObject.GetComponent<DiskMovement>().setEndPoint(endPoint.position);
            newObject.transform.localScale = new Vector2(size, size); // change its local scale in x y z format
        }
    }

}
