using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedControl : MonoBehaviour
{
    static public float speed = 5.0f;
    public float difficulty;

    private float difficultyControll;
    private float count = 0.0f;
    public float Difficulty
    {
        set { difficultyControll = 1.0f / value; }
        get { return difficultyControll; }
    }

    private void Start()
    {
    }
    private void Update()
    {
        count += Time.deltaTime;
        Difficulty = difficulty;
        if (count > difficultyControll)
        {
            count = 0.0f;
            speed += 0.01f;
        }

    }
}
