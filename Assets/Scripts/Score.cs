using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public TMP_Text scoreText;
    private int currentScore = 0;
    private bool speedUp = false;
    private int speedUpScore = 10;
    private int speedUpComp = 10;

    public GameObject player;
    public GameObject background;
    public GameObject diskSpawner;
    private BackgroundScroller bgScroller;
    private DiskSpawner diskSpawnerScript;
    private DiskPool diskPoolScript;
    private PlayerController playerController;
    

    // Start is called before the first frame update
    void Start()
    {
        bgScroller = background.GetComponent<BackgroundScroller>();
        diskSpawnerScript = diskSpawner.GetComponent<DiskSpawner>();
        diskPoolScript = diskSpawner.GetComponent<DiskPool>();
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(speedUp){
            speedUp = false;
            speedUpScore += speedUpComp;
            playerController.SpeedUp();
            diskPoolScript.SpeedUp();
            bgScroller.SpeedUp();
            diskSpawnerScript.SpeedUp();
        }
    }

    public void UpdateScore(int score){
        currentScore += score;
        scoreText.text = currentScore.ToString();
        if(currentScore - speedUpScore >= 0)
            speedUp = true;
             
    }
}
