using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    
    public static int highScore = 0;
    public TMP_Text highScoreText;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", highScore);
        highScoreText.text = highScore.ToString();
    }

    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame(){
        Application.Quit();
    }

}
