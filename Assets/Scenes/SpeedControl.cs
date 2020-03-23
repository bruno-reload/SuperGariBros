using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedControl : MonoBehaviour
{
    static public float speed = 5.0f;
    public float difficulty;

    private UnityEngine.UI.Text speedUI;
    private float difficultyControll;
    private float count = 0.0f;
    public float Difficulty
    {
        set { difficultyControll = 1.0f / value; }
        get { return difficultyControll; }
    }

    private void Start()
    {
        speedUI = GameObject.Find("Canvas/KmBar/value").GetComponent<UnityEngine.UI.Text>();
    }
    private void Update()
    {
        count += Time.deltaTime;
        Difficulty = difficulty;
        if (count > difficultyControll)
        {
            count = 0.0f;
            speed += 0.01f;
            speedUI.text = (speed - 5).ToString("F");
        }

    }
}
