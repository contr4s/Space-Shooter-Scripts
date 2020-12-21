using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Save_score : MonoBehaviour
{
    public int score;
    public static int record;
    public Text scoreBar;
    public Text recordBar;

    void Start()
    {
        score = SaveSystem.GetInt("Score");
        record = SaveSystem.GetInt("Record");
        if (score > record)
            record = score;
        scoreBar.GetComponent<Text>().text = "Score " + score.ToString();
        recordBar.GetComponent<Text>().text = "Record " + record.ToString();
    }

    void Update()
    {
       
    }
    void OnApplicationQuit()
    {
        SaveSystem.SetInt("Record", record);
        SaveSystem.SaveToDisk();
    }
}
