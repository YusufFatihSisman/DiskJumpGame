using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public TMP_Text scoreText;
    private int currentScore = 0;
    private int currentScorePenalty = 0;
    private bool speedUp = false;
    private int speedUpScore = 10;
    private int speedUpLimit = 10;

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
            speedUpScore += speedUpLimit;
            speedUpLimit *= 2;
            playerController.SpeedUp();
            diskPoolScript.SpeedUp();
            bgScroller.SpeedUp();
            diskSpawnerScript.SpeedUp();
        }
    }

    public void UpdateScore(int score){
        currentScore += score;
        currentScorePenalty += score;
        scoreText.text = currentScore.ToString();
        if(currentScorePenalty - speedUpScore >= 0)
            speedUp = true;
             
    }

    public void UpdateScorePenalty(int score){
        currentScorePenalty += score;
        if(currentScorePenalty - speedUpScore >= 0)
            speedUp = true;
    }

    public void UpdateHighScore(){
        if(MainMenuController.highScore < currentScore){
            MainMenuController.highScore = currentScore;
            PlayerPrefs.SetInt ("highScore", currentScore);
        }
    }
}
