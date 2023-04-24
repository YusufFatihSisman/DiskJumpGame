using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskSpawner : MonoBehaviour
{

    public GameObject player;
    public Transform endPoint;
    public GameObject diskPrefab;
    public Transform TopLimit;
    public Transform BottomLimit;
    private float bottomSize = 1f;
    private float topSize = 4f;

    private float playerXSize;


    // Start is called before the first frame update
    void Start()
    {
        playerXSize = player.GetComponent<PlayerController>().GetXSize();
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
            GameObject newDisk = DiskPool.SharedInstance.GetPooledObject();
            if(newDisk != null){
                float size = Random.Range(bottomSize, topSize);
                float pos = Random.Range(BottomLimit.position.y, TopLimit.position.y);
                if(pos + size/2 > TopLimit.position.y)
                    pos = TopLimit.position.y - size/2 - playerXSize;     
                if(pos - size/2 < BottomLimit.position.y)
                    pos = BottomLimit.position.y + size/2 + playerXSize;

                newDisk.transform.position = new Vector3(TopLimit.position.x+(size/2), pos, 0);
                //GameObject newObject = Instantiate(diskPrefab, new Vector2(TopLimit.position.x+(size/2), pos), Quaternion.identity);
                //Debug.Log(endPoint.position);
                newDisk.GetComponent<DiskMovement>().setEndPoint(endPoint.position);
                newDisk.transform.localScale = new Vector2(size, size); // change its local scale in x y z format
                newDisk.SetActive(true);
                
            }else{
                Destroy(newDisk);
                yield return null;
            }
            
        }
    }

}
