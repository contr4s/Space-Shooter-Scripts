using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public double score;
    public float complexity = 0.4f;

    void Start()
    {
        
    }
    void Update()
    {
        complexity = 0.4f + Time.time / 2000;
        score += complexity * Time.deltaTime * 5;
    }
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 100), "Score: " + ((int)score).ToString());
    }

    void OnApplicationQuit()
    {
        SaveSystem.SetInt("Score", (int)score);
    }
}
