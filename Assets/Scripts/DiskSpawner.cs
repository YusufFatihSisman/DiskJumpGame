using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskSpawner : MonoBehaviour
{

    private const float SPEEDUPCOEF = 1.5f;
    private float waitTime = 5f;
    public GameObject player;
    public Transform endPoint;
    public GameObject diskPrefab;
    public Transform TopLimit;
    public Transform BottomLimit;
    private float bottomSize = 2f;
    private float topSize = 4f;

    private float playerXSize;
    private float scrollSpeed = 1f;

    public GameObject scoreText;
    private Score scoreScript;


    // Start is called before the first frame update
    void Start()
    {
        playerXSize = player.GetComponent<PlayerController>().GetXSize();
        scoreScript = scoreText.GetComponent<Score>();
        StartCoroutine(CreateDisk());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator CreateDisk()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            GameObject newDisk = DiskPool.SharedInstance.GetPooledObject();
            if(newDisk != null){
                DiskMovement dM = newDisk.GetComponent<DiskMovement>();
                dM.setScoreSpcript(scoreScript);
                float size = Random.Range(bottomSize, topSize);
                float pos = Random.Range(BottomLimit.position.y, TopLimit.position.y);
                if(pos + size/2 > TopLimit.position.y)
                    pos = TopLimit.position.y - size/2 - playerXSize;     
                else if(pos - size/2 < BottomLimit.position.y)
                    pos = BottomLimit.position.y + size/2 + playerXSize;
                else
                    if((TopLimit.position.y - pos) > (pos - BottomLimit.position.y))
                        pos += playerXSize;
                    else
                        pos -= playerXSize;
                int direction = Random.Range(0, 2);
                if(direction == 0)
                    dM.SetRotation(false);
                else
                    dM.SetRotation(true);
                newDisk.transform.position = new Vector3(TopLimit.position.x+(size/2), pos, 0);
                newDisk.tag = "Infected";
                dM.SetEndPoint(endPoint.position);
                dM.SetScrollSpeed(scrollSpeed);
                newDisk.transform.localScale = new Vector2(size, size); // change its local scale in x y z format
                newDisk.SetActive(true);
            }else{
                Destroy(newDisk);
                yield return null;
            }
            
        }
    }


    public void SpeedUp(){
        scrollSpeed *= SPEEDUPCOEF;
        waitTime /= SPEEDUPCOEF;
    }
}
