using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveScore : MonoBehaviour
{
    public int score;
    public static int highscore;
    public Text scoreBar;
    public Text recordBar;

    void Start()
    {
        score = PlayerPrefManager.GetScore();
        highscore = PlayerPrefManager.GetHighscore();
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefManager.SetHighscore(highscore);
        }
        scoreBar.GetComponent<Text>().text = "Score " + score.ToString();
        recordBar.GetComponent<Text>().text = "Record " + highscore.ToString();
    }

    void Update()
    {
       
    }
}
